using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class TourLog : ModelBase, ICloneable<TourLog>
    {
        public string TourName { get; set; } // the name references the tour (names are unique)
        public DateTime DateTime { get; set; } = DateTime.Now;
        public string Report { get; set; }
        public double Distance { get; set; } // am i using this?
        public double TotalTime { get; set; }
        public int Rating { get; set; }

        public string Author { get; set; }
        public bool HasMcDonalds { get; set; }
        public bool HasCampingSpots { get; set; }
        public string Accomodation { get; set; }
        public int Members { get; set; }


        public TourLog Clone() => (TourLog)this.MemberwiseClone();
    }
}
