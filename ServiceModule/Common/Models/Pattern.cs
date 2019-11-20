using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceModule.Common.Models
{
    public class Pattern
    {
        public int Id { get; set; }
        public List<Sensor> Sensors { get; set; }
        public Time Time { get; set; }
        public Messages Messages { get; set; }
    }
}
