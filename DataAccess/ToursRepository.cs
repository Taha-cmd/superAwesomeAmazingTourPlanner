using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    public class ToursRepository
    {
        public IEnumerable<Tour> GetTours()
        {
            return new List<Tour>()
            {
                new Tour() { Name = "tour1", StartingArea="place1", TargetArea="place2", Description = "very nice", Distance = 10 },
                new Tour() { Name = "tour2", Description = "very very nice", Distance = 20 },
                new Tour() { Name = "tour3", Description = "even more nice", Distance = 30 }
            };
        }

        public void SaveTour(Tour tour)
        {
            Console.WriteLine("saving tour to database");
        }
    }
}
