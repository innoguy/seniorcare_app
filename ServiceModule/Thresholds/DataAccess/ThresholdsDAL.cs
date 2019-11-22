using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ServiceModule.Settings;
using ServiceModule.StorageService;
using ServiceModule.Thresholds.DataAccess.Entities;

namespace ServiceModule.Thresholds.DataAccess
{
    public class ThresholdsDAL : IThresholdsDAL
    {
        private readonly ISettingsService _settingsService;
        private readonly ICrossStorageService _crossStorageService;

        static readonly HttpClient _client = new HttpClient();
        private static string _baseURL;

        public ThresholdsDAL(ISettingsService settingsService, ICrossStorageService crossStorageService)
        {
            _settingsService = settingsService ?? throw new ArgumentNullException(nameof(settingsService));
            _crossStorageService = crossStorageService ?? throw new ArgumentNullException(nameof(crossStorageService));
        }

        public async Task<SeniorCareJsonObject> GetThresholds(string deviceId, string md5Hash)
        {
            var seniorCareJsonObject = new SeniorCareJsonObject();

            try
            {
                _baseURL = $"{_settingsService.Protocol}://{_settingsService.IpAddress}:{_settingsService.Port}/apiv1";
                var response = await _client.GetAsync($"{_baseURL}/config/patterns/download/{deviceId}/{md5Hash}");
                response.EnsureSuccessStatusCode();
                if (response.StatusCode == HttpStatusCode.NotModified)
                {
                    return null;
                }
                if (response.IsSuccessStatusCode)
                {
                    var resp = await response.Content.ReadAsStringAsync();
                    await _crossStorageService.WriteTextAsync("jsonFile", resp);
                    seniorCareJsonObject = JsonConvert.DeserializeObject<SeniorCareJsonObject>(resp);
                }

                return seniorCareJsonObject;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return seniorCareJsonObject;
            }
        }

        public async void UpdateThresholds(string deviceId, DataService.Models.Thresholds thresholds)
        {
            var json = SetThresholds(thresholds);
            try
            {
                _baseURL = $"{_settingsService.Protocol}://{_settingsService.IpAddress}:{_settingsService.Port}/apiv1";
                var response = await _client.PostAsJsonAsync($"{_baseURL}/config/patterns/upload/{deviceId}", json);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private JObject SetThresholds(DataService.Models.Thresholds thresholds)
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
                                {
                                    'P012':
                                    {
                                        'value':
                                        {
                                            'less': 10
                                        }
                                    }
                                },
                                'time':
                                {
                                    'period':
                                    {
                                       'start': '" + thresholds.TelevisionFromTime + @"',
                                       'end': '" + thresholds.TelevisionToTime + @"'
                                    },
                                    'frequency': 600
                                },
                                'messages':
                                {
                                    'rule': 'The TV must be OFF between the specified period'
                                }
                            },

                            {
                                'id': 2,
                                'sensors':
                                {
                                    'P018':
                                        {
                                            'value':
                                            {
                                                'more': 400
                                            },
                                            'duration':
                                            {
                                                'more' : " + (thresholds.PowerDeviceTime * 60) + @"
                                            },
                                            'frequency':
                                            {
                                                'less': 1
                                            }
                                        }
                                },
                                'time':
                                {
                                    'frequency': 600
                                },
                                'messages':
                                {
                                    'rule': 'High power device must not be on for more than one hour'
                                }
                            },

                            {
                                'id': 3,
                                'sensors':
                                {
                                    'M004':
                                    {
                                        'value':
                                        {
                                            'equal': 'On'
                                         },
                                        'cluster_separation': 300,
                                        'duration':
                                            {
                                                'more' : 60
                                            },
                                        'frequency':
                                        {
                                            'less': " + thresholds.BathroomGoingTimes + @"
                                        }
                                    }
                                },
                                'time':
                                {
                                    'period':
                                    {
                                       'start': '" + thresholds.BathroomFromTime + @"',
                                       'end': '" + thresholds.BathroomToTime + @"'
                                    }
                                },
                                'messages':
                                {
                                    'rule': 'Going to the bathroom during the night'
                                }
                            },

                            {
                                'id': 4,
                                'sensors':
                                {
                                    'M003':
                                    {
                                        'value':
                                        {
                                            'equal': 'On'
                                        },
                                        'frequency':
                                        {
                                            'more': 0
                                        }
                                    }
                                },
                                'time':
                                {
                                    'period':
                                    {
                                       'start': '" + thresholds.PersonNotInBedFrom + @"',
                                       'end': '" + thresholds.PersonNotInBedTo + @"'
                                    },
                                    'frequency': 7200
                                },
                                'messages':
                                {
                                    'rule': 'Person has to be in bed during the night'
                                }
                            },

                            {
                                'id': 5,
                                'sensors':
                                {
                                    'T001':
                                    {
                                        'value':
                                        {
                                            'more': 17,
                                            'less':30
                                        }
                                    }
                                },
                                'time':
                                {
                                    'frequency': 3600
                                },
                                'messages':
                                {
                                    'rule': 'Temperature must be inside the specified range'
                                }
                            },

                            {
                                'id': 6,
                                'sensors':
                                {
                                    'D004':
                                    {
                                        'value':
                                        {
                                            'equal': 'On'
                                        },
                                        'frequency':
                                        {
                                            'less': 5
                                        }
                                    }
                                },
                                'notified':
                                {
                                    'duration' : 15
                                },
                                'messages':
                                {
                                    'rule': 'SOS activity'
                                }
                            }
                        ]
                    }");

            JObject json = JObject.Parse(sb.ToString());
            return json;
        }
    }
}
