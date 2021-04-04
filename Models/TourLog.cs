using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class TourLog : ModelBase
    {
        public DateTime DateTime { get; set; }
        public string Report { get; set; }
        public float Distance { get; set; }
        public float TotalTime { get; set; }
        public int Rating { get; set; }

    }
}
