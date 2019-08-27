using System;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json.Linq;

namespace ServiceModule.Thresholds
{
    public class ThresholdsDataservice : IThresholdsDataservice
    {
        static readonly HttpClient _client = new HttpClient();
        private static string _baseURL = "http://5.2.158.223:45678/apiv1";

        public async void UpdateThresholds(string deviceId, Models.Thresholds thresholds)
        {
            var json = SetThresholds(thresholds);
            try
            {
                var response = await _client.PostAsJsonAsync($"{_baseURL}/config/patterns/upload/{deviceId}", json);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }     
        }

        private JObject SetThresholds(Models.Thresholds thresholds)
        {
            var sb = new StringBuilder();
            sb.Append(@"
                    {'patterns':
                        [
                            {
                                'id': 1,
                                'sensors':
                                {
                                    'P001':
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
                                    'frequency': 300
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
                                    'P002':
                                        {
                                            'value':
                                            {
                                                'more': 10
                                            },
                                            'duration':
                                            {
                                                'more' : " + thresholds.PowerDeviceTime + @"
                                            },
                                            'frequency':
                                            {
                                                'less': 1
                                            }
                                        }
                                },
                                'time':
                                {
                                    'frequency': 300
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
                                    'M001':
                                    {
                                        'value':
                                        {
                                            'equal': 'On'
                                         },
                                        'cluster_separation': 200,
                                        'duration':
                                            {
                                                'more' : 300
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
                                    'M001':
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
                                            'more': 20,
                                            'less':32
                                        }
                                    }
                                },
                                'time':
                                {
                                    'frequency': 300
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
                                    'B001':
                                    {
                                        'value':
                                        {
                                            'equal': 'On'
                                        },
                                        'frequency':
                                        {
                                            'less': 4
                                        }
                                    }
                                },
                                'notified':
                                {
                                    'duration' : 10
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
