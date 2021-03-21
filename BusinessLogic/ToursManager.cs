using Models;
using System;
using System.Collections.Generic;
using System.Text;
using Extensions;

namespace BusinessLogic
{
    public class ToursManager
    {
        private ToursRepository toursRepo = new ToursRepository();
        public void CreateTour(Tour tour)
        {
            if (!ValidateTour(tour))
                throw new Exception("invalid tour!");

            Console.WriteLine("manager creating tour");
            toursRepo.SaveTour(tour);
        }

        public ToursManager()
        {
            //Console.WriteLine(Config.Instance.DataBaseConnectionString);
        }

        public Tour GetTour(string name)
        {
            throw new NotImplementedException();
        }

        public List<Tour> GetTours(int? limit = null)
        {
            return (List<Tour>)toursRepo.GetTours();
        }

        public void DeleteTour(string name)
        {

        }

        public Tour UpdateTour(string currentName, Tour tour)
        {
            throw new NotImplementedException();
        }



        private bool ValidateTour(Tour tour)
        {
            return
                !tour.Name.IsEmptyOrWhiteSpace() &&
                !tour.Description.IsEmptyOrWhiteSpace() &&
                !tour.Distance.IsNull();

        }
    }
}
