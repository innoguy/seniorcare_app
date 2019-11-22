using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using ServiceModule.HashGenerator;
using ServiceModule.StorageService;
using ServiceModule.Thresholds.DataAccess;

namespace ServiceModule.Thresholds.DataService
{
    public class ThresholdsDataService : IThresholdsDataService
    {
        private readonly IThresholdsDAL _thresholdsDal;
        private readonly ICrossStorageService _crossStorageService;


        public ThresholdsDataService(IThresholdsDAL thresholdsDal, ICrossStorageService crossStorageService)
        {
            _thresholdsDal = thresholdsDal ?? throw new ArgumentNullException(nameof(thresholdsDal));
            _crossStorageService = crossStorageService ?? throw new ArgumentNullException(nameof(crossStorageService));
        }

        public async Task<Models.Thresholds> GetThresholds(string deviceId, Models.Thresholds currentThresholds)
        {
            try
            {
                string md5Hash = "";
                if (await _crossStorageService.FileExistAsync("jsonFile", ".txt"))
                {
                    var jsonFile = await _crossStorageService.GetFileAsync("jsonFile");
                    var content = jsonFile.ReadTextAsync();
                    md5Hash = MD5HashGenerator.GenerateKey(content).ToLower();
                }
                else
                {
                    var thresholdsJsonObject = GetThresholdsJsonObject(currentThresholds);
                    md5Hash = MD5HashGenerator.GenerateKey(thresholdsJsonObject, true).ToLower();
                }

                var jsonObject = await _thresholdsDal.GetThresholds(deviceId, md5Hash);

                if (jsonObject != null)
                {
                    var televisionPattern = jsonObject.Patterns.Find(p => p.Id == 1);
                    var powerDevicePattern = jsonObject.Patterns.Find(p => p.Id == 2);
                    var bathroomPattern = jsonObject.Patterns.Find(p => p.Id == 3);
                    var personBedSchedulePattern = jsonObject.Patterns.Find(p => p.Id == 4);

                    var updatedThresholds = new Models.Thresholds
                    {
                        BathroomFromTime = TimeSpan.Parse(bathroomPattern.Time.Period.Start.Replace(" ", "")),
                        BathroomToTime = TimeSpan.Parse(bathroomPattern.Time.Period.End.Replace(" ", "")),
                        BathroomGoingTimes = bathroomPattern.Sensors.Find(s => s.Name == "M001").Frequency.Less,
                        PersonNotInBedFrom = TimeSpan.Parse(personBedSchedulePattern.Time.Period.Start.Replace(" ", "")),
                        PersonNotInBedTo = TimeSpan.Parse(personBedSchedulePattern.Time.Period.End.Replace(" ", "")),
                        PowerDeviceTime = powerDevicePattern.Sensors.Find(s => s.Name == "P002").Duration.More,
                        TelevisionFromTime = TimeSpan.Parse(televisionPattern.Time.Period.Start.Replace(" ", "")),
                        TelevisionToTime = TimeSpan.Parse(televisionPattern.Time.Period.End.Replace(" ", ""))
                    };

                    return updatedThresholds;
                }

                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }

        private JObject GetThresholdsJsonObject(DataService.Models.Thresholds thresholds)
        {
            var sb = new StringBuilder();
            sb.Append(@"
                    {
                    'version': 1,
                    'last-modified': 1564741796235,
                    'patterns':
                        [
                            {
                                'id': 1,
                                'sensors':
                                [
                                    {
                                        'name': 'P001',
                                        'value': {
                                            'less': 10
                                        }
                                    }
                                ],
                                'time': {
                                    'period': {
                                       'start': '" + thresholds.TelevisionFromTime + @"',
                                       'end': '" + thresholds.TelevisionToTime + @"'
                                    },
                                    'frequency': 300
                                },
                                'messages': {
                                    'rule': 'The TV must be OFF between the specified period'
                                }
                            },

                            {
                                'id': 2,
                                'sensors':
                                [
                                   {
                                        'name': 'P002',
                                        'value': {
                                            'more': 10
                                        },
                                        'duration': {
                                            'more' : " + thresholds.PowerDeviceTime + @"
                                        },
                                        'frequency': {
                                            'less': 1
                                        }
                                   }
                                ],
                                'time': {
                                    'frequency': 300
                                },
                                'messages': {
                                    'rule': 'High power device must not be on for more than one hour'
                                }
                            },

                            {
                                'id': 3,
                                'sensors':
                                [
                                    {
                                        'name': 'M001',
                                        'value': {
                                            'equal': 'On'
                                         },
                                        'cluster_separation': 200,
                                        'duration': {
                                             'more' : 300
                                         },
                                        'frequency': {
                                            'less': " + thresholds.BathroomGoingTimes + @"
                                        }
                                    }
                                ],
                                'time': {
                                    'period': {
                                       'start': '" + thresholds.BathroomFromTime + @"',
                                       'end': '" + thresholds.BathroomToTime + @"'
                                    }
                                },
                                'messages': {
                                    'rule': 'Going to the bathroom during the night'
                                }
                            },

                            {
                                'id': 4,
                                'sensors':
                                [
                                    {
                                        'name': 'M001',
                                        'value': {
                                            'equal': 'On'
                                        },
                                        'frequency': {
                                            'more': 0
                                        }
                                    }
                                ],
                                'time': {
                                    'period': {
                                       'start': '" + thresholds.PersonNotInBedFrom + @"',
                                       'end': '" + thresholds.PersonNotInBedTo + @"'
                                    },
                                    'frequency': 7200
                                },
                                'messages': {
                                    'rule': 'Person has to be in bed during the night'
                                }
                            },

                            {
                                'id': 5,
                                'sensors':
                                [  
                                    {
                                        'name': 'T001',
                                        'value': {
                                            'more': 20,
                                            'less': 32
                                        }
                                    }
                                ],
                                'time': {
                                    'frequency': 300
                                },
                                'messages': {
                                    'rule': 'Temperature must be inside the specified range'
                                }
                            },

                            {
                                'id': 6,
                                'sensors':
                                [
                                    {
                                        'name': 'B001',
                                        'value': {
                                            'equal': 'On'
                                        },
                                        'frequency': {
                                            'less': 4
                                        }
                                    }
                                ],
                                'notified': {
                                    'duration' : 10
                                },
                                'messages': {
                                    'rule': 'SOS activity'
                                }
                            }
                        ]
                    }");

            try
            {
                JObject json = JObject.Parse(sb.ToString());
                return json;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }
    }
}