using Models;
using System;
using System.Collections.Generic;
using System.Text;
using Extensions;
using BusinessLogic.CustomEventArgs;
using System.Linq;
using DataAccess;

namespace BusinessLogic
{
    public class ToursManager
    {
        private readonly IToursRepository toursRepo;
        public ToursManager(IToursRepository toursRepo)
        {
            this.toursRepo = toursRepo;
        }

        public event EventHandler DataChanged;
        private void TriggerDataChangedEvent() => DataChanged?.Invoke(this, EventArgs.Empty);

        #region Tour CRUD Methods
        public void CreateTour(Tour tour)
        {
            if (!ValidateTour(tour))
                throw new Exception("invalid tour!");

            toursRepo.Create(tour);
            TriggerDataChangedEvent();

        }
        public Tour GetTour(string name) => toursRepo.TourExists(name) ? toursRepo.GetTour(name) : throw new Exception($"tour {name} does not exist");
        public List<Tour> GetTours(int? limit = null) => toursRepo.GetTours(limit).ToList();
        public void DeleteTour(string name)
        {
            toursRepo.Delete(name);
            TriggerDataChangedEvent();
        }

        public void DeleteTour(Tour tour) => DeleteTour(tour.Name);

        public Tour UpdateTour(string currentName, Tour tour)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region probably useless stuff
        public event EventHandler<TourAddedEventArgs> TourAdded;
        public event EventHandler<TourDeletedEventArgs> TourDeleted;
        public event EventHandler<TourUpdatedEventArgs> TourUpdated;

        public void TriggerTourUpdatedEvent(string oldName, Tour tour) => TourUpdated?.Invoke(this, new TourUpdatedEventArgs(oldName, tour));
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

        #region validation methods
        public bool ValidateTour(Tour tour)
        {
            return new List<string>() { tour.Name, tour.Description, tour.StartingArea, tour.TargetArea }.All(el => el.HasValue()) && !toursRepo.TourExists(tour.Name);
        }

        public bool ValidateTourLog(TourLog log)
        {
            return log.Rating >= 0 && log.Rating <= 10 && log.Report.HasValue() && log.TotalTime > 0;
        }
        #endregion
    }
}
