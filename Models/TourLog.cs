using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class TourLog : ModelBase
    {
        public DateTime DateTime { get; set; }
        public string Report { get; set; }
        public double Distance { get; set; }
        public double TotalTime { get; set; }
        public int Rating { get; set; }

    }
}
