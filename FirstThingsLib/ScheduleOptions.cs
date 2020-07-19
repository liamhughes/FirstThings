using System;

namespace FirstThingsLib
{
    public class ScheduleOptions
    {
        public ScheduleOptions()
        {
        }

        public DateTime? EndDate { get; set; }

        public string List { get; set; }

        public DateTime StartDate { get; set; }
    }
}