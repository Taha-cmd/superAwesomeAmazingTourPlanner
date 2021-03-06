using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Tour :  ICloneable<Tour>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string StartingArea { get; set; }
        public string TargetArea { get; set; }
        public double Distance { get; set; } // in km?

        public string Image { get; set; }
        public List<TourLog> Logs { get; set; } = new List<TourLog>();

        public Tour Clone()
        {
            var logs = new List<TourLog>();
            Logs.ForEach(log => logs.Add(log.Clone()));

            Tour tour = (Tour)this.MemberwiseClone();
            tour.Logs = logs;

            return tour;
        }
    }
}
