using Models;
using System;
using System.Collections.Generic;
using System.Text;
using Extensions;
using BusinessLogic.CustomEventArgs;
using System.Linq;
using DataAccess;
using DataAccess.Maps;
using System.Diagnostics;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class ToursManager
    {
        private readonly IToursRepository toursRepo;
        private readonly IMapsApiClient mapsClient;
        public ToursManager(IToursRepository toursRepo, IMapsApiClient mapsClient)
        {
            this.toursRepo = toursRepo;
            this.mapsClient = mapsClient;
        }

        public event EventHandler DataChanged;
        private void TriggerDataChangedEvent() => DataChanged?.Invoke(this, EventArgs.Empty);

        #region Tour CRUD Methods
        public async Task CreateTour(Tour tour)
        {
            if (!ValidateTour(tour))
                throw new Exception("invalid tour! all fields must have a value!");

            if (toursRepo.TourExists(tour.Name))
                throw new Exception($"tour {tour.Name} allready exists. Names must be unique");

            var routeInfo = await mapsClient.GetRouteInformation(tour.StartingArea, tour.TargetArea);

            if (!routeInfo.RouteExists)
                throw new Exception($"no route found between {tour.StartingArea} and {tour.TargetArea}");

            tour.Distance = routeInfo.Distance;
            tour.Image = routeInfo.ImagePath;

            toursRepo.Create(tour);
            TriggerDataChangedEvent();
        }
        public Tour GetTour(string name) => toursRepo.TourExists(name) ? toursRepo.GetTour(name) : throw new Exception($"tour {name} does not exist");
        public List<Tour> GetTours(int? limit = null) => toursRepo.GetTours(limit).ToList();
        public async Task DeleteTour(Tour tour)
        {
            await Task.Run(() =>
              {
                  toursRepo.Delete(tour);
              });

            // throws an exception when triggered from the task thread
            TriggerDataChangedEvent();
        }

        public async Task UpdateTour(string currentName, string currentImage, Tour tour)
        {
            if (!toursRepo.TourExists(currentName))
                throw new Exception($"Tour {currentName} does not exist");

            if (!ValidateTour(tour))
                throw new Exception($"Invalid Tour!");

            if (toursRepo.TourExists(tour.Name) && currentName != tour.Name)
                throw new Exception($"Tour Name {tour.Name} is allready taken, names must be unique");

            var routeInfo = await mapsClient.GetRouteInformation(tour.StartingArea, tour.TargetArea);

            if (!routeInfo.RouteExists)
                throw new Exception($"no route found between {tour.StartingArea} and {tour.TargetArea}");

            tour.Distance = routeInfo.Distance;
            tour.Image = routeInfo.ImagePath;

            toursRepo.Update(currentName, currentImage, tour);
            TriggerDataChangedEvent();
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
        public async Task CreateTourLog(string tourName, TourLog log)
        {
            await Task.Run(() =>
            {
                if (!ValidateTourLog(log))
                    throw new Exception("invalid tour log!");

                if (!toursRepo.TourExists(tourName))
                    throw new Exception($"Tour {tourName} does not exist");

                toursRepo.AddLog(tourName, log);
            });
        }
        #endregion

        #region validation methods
        public bool ValidateTour(Tour tour)
        {
            return new List<string>() { tour.Name, tour.Description, tour.StartingArea, tour.TargetArea }.All(el => el.HasValue());
        }

        public bool ValidateTourLog(TourLog log)
        {
            return log.Rating >= 0 && log.Rating <= 10 && log.Report.HasValue() && log.TotalTime > 0;
        }
        #endregion
    }
}
