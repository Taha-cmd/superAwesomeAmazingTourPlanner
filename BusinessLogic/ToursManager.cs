using Models;
using System;
using System.Collections.Generic;
using System.Text;
using Extensions;
using BusinessLogic.CustomEventArgs;
using System.Linq;

namespace BusinessLogic
{
    public class ToursManager
    {
        private readonly ToursRepository toursRepo = new ToursRepository();
        public ToursManager()
        {
            Console.WriteLine(Config.Instance.DataBaseConnectionString);
        }


        #region Tour CRUD Methods
        public void CreateTour(Tour tour)
        {
            if (!ValidateTour(tour))
                throw new Exception("invalid tour!");

            Console.WriteLine("manager creating tour");
            toursRepo.SaveTour(tour);
            TriggerTourAddedEvent(tour);
        }
        public Tour GetTour(string name)
        {
            throw new NotImplementedException();
        }

        public List<Tour> GetTours(int? limit = null)
        {
            var tours = (List<Tour>)(toursRepo.GetTours());
            tours.ForEach(tour => tour.Logs = (List<TourLog>)toursRepo.GetLogs(tour.Name));
            return tours;
        }

        public void DeleteTour(string name)
        {

        }

        public Tour UpdateTour(string currentName, Tour tour)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region probably useless stuff
        public event EventHandler<TourAddedEventArgs> TourAdded;
        public event EventHandler<TourDeletedEventArgs> TourDeleted;
        public event EventHandler<TourUpdatedEventArgs> TourUpdated;

        public void TriggerTourUpdatedEvent(string oldName, Tour tour)
        {
            TourUpdated?.Invoke(this, new TourUpdatedEventArgs(oldName, tour));
        }

        public void TriggerTourDeletedEvent(Tour tour) => TourDeleted?.Invoke(this, new TourDeletedEventArgs(tour));
        public void TriggerTourAddedEvent(Tour tour) => TourAdded?.Invoke(this, new TourAddedEventArgs(tour));
        #endregion
        #region TourLog Crud Methods
        public void CreateTourLog(string tourName, TourLog log)
        {
            if (!ValidateTourLog(log))
                throw new Exception("invalid tour log!");

            Console.WriteLine("saving tour log for tour " + tourName);

        }
        #endregion

        public bool ValidateTour(Tour tour)
        {
            return
                !tour.Name.IsEmptyOrWhiteSpace() &&
                !tour.Description.IsEmptyOrWhiteSpace() &&
                !tour.StartingArea.IsEmptyOrWhiteSpace() &&
                !tour.TargetArea.IsEmptyOrWhiteSpace();
        }

        public bool ValidateTourLog(TourLog log)
        {
            return true;
        }
    }
}
