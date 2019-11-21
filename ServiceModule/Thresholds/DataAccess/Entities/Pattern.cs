using System.Collections.Generic;

namespace ServiceModule.Thresholds.DataAccess.Entities
{
    public class Pattern
    {
        public int Id { get; set; }
        public List<Sensor> Sensors { get; set; }
        public Time Time { get; set; }
        public Messages Messages { get; set; }
    }
}
