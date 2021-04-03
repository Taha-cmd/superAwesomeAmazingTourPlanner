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

        public void SaveTourLog(string tourName, TourLog log)
        {
            Console.WriteLine("saving tour log to database");
        }

        public IEnumerable<TourLog> GetLogs(string tourName)
        {
            return new List<TourLog>()
            {
                new TourLog(){ DateTime = DateTime.Now, Rating= "very good", Report= "very nice tour", TotalTime =3},
                new TourLog(){ Rating= "sucks", Report="it was rainy", TotalTime=4}
            };
        }
    }
}
