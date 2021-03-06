using DataAccess;
using DataAccess.Maps;
using Extensions;
using log4net;
using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class ToursManager
    {
        private readonly IToursRepository toursRepo;
        private readonly IMapsApiClient mapsClient;
        private readonly IPdfGenerator pdfGenerator;
        private readonly ILog logger;
        public ToursManager(IToursRepository toursRepo, IMapsApiClient mapsClient, IPdfGenerator pdfGenerator)
        {
            this.toursRepo = toursRepo;
            this.mapsClient = mapsClient;
            this.pdfGenerator = pdfGenerator;
            this.logger = Application.GetLogger();
        }

 
        public event EventHandler DataChanged;
        private void TriggerDataChangedEvent() => DataChanged?.Invoke(this, EventArgs.Empty);

        public async Task GenerateReport(Tour tour)
        {
            if (!ValidateTour(tour))
                throw new Exception("Invalid tour!");

            var routeInfo = await mapsClient.GetRouteInformation(tour.StartingArea, tour.TargetArea);

            if (!routeInfo.RouteExists)
                throw new Exception($"no route found between {tour.StartingArea} and {tour.TargetArea}");


            await Task.Run(() => pdfGenerator.GenerateReport(tour));
        }
        public async Task Export(Tour tourOriginal)
        {
            if (!ValidateTour(tourOriginal))
                throw new Exception("invalid tour! all fields must have a value!");

            Tour tour = tourOriginal.Clone();

            tour.Image = null;
            string path = Path.Join(Config.Instance.ExportsFolderPath, tour.Name + ".json");
            string json = JsonSerializer.Serialize(tour);

            await File.WriteAllTextAsync(path, json);
            logger.Debug($"exporting tour {tour.Name}");

        }

        public async Task Import(string path)
        {
            if (!File.Exists(path))
                throw new Exception($"file {path} does not exist");

            Tour tour;

            try
            {
                tour = JsonSerializer.Deserialize<Tour>(File.ReadAllText(path));
            }
            catch (Exception)
            {
                throw new Exception("error imprting tour, invalid file format!");
            }

            if (!ValidateTour(tour))
                throw new Exception("invalid tour! all fields must have a value!");

            if (toursRepo.TourExists(tour.Name))
                throw new Exception($"tour {tour.Name} allready exists. Names must be unique");

            var routeInfo = await mapsClient.GetRouteInformation(tour.StartingArea, tour.TargetArea, true);

            if (!routeInfo.RouteExists)
                throw new Exception($"no route found between {tour.StartingArea} and {tour.TargetArea}");

            tour.Image = routeInfo.ImagePath;
            toursRepo.Create(tour);
            logger.Debug($"importing tour {tour.Name}");

            TriggerDataChangedEvent();
        }

        public async Task Copy(Tour tour)
        {
            if (!ValidateTour(tour))
                throw new Exception("invalid tour! all fields must have a value!");

            var newTour = tour.Clone();

            if (tour.Name.Contains("Copy"))
            {
                char prefix = (char)(((int)tour.Name.Last()) + 1);
                newTour.Name = newTour.Name[0..^1] + prefix;
            }
            else
            {
                newTour.Name += "Copy0";
            }

            if (toursRepo.TourExists(newTour.Name))
                throw new Exception($"A copy of the tour {tour.Name} allready exists. A tour can be only copied once. Consider copying the copy");

            string newPath = $"{Config.Instance.ImagesFolderPath}/{Guid.NewGuid()}.jpg";
            newTour.Image = newPath;
            await Task.Run(() => File.Copy(tour.Image, newPath));

            toursRepo.Create(newTour);
            TriggerDataChangedEvent();
        }

        #region Tour CRUD Methods
        public async Task CreateTour(Tour tour)
        {
            if (!ValidateTour(tour))
                throw new Exception("invalid tour! all fields must have a value!");

            if (toursRepo.TourExists(tour.Name))
                throw new Exception($"tour {tour.Name} allready exists. Names must be unique");

            var routeInfo = await mapsClient.GetRouteInformation(tour.StartingArea, tour.TargetArea, true);

            if (!routeInfo.RouteExists)
                throw new Exception($"no route found between {tour.StartingArea} and {tour.TargetArea}");

            tour.Distance = routeInfo.Distance;
            tour.Image = routeInfo.ImagePath;

            toursRepo.Create(tour);
            logger.Debug($"creating tour {tour.Name}");

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

            var routeInfo = await mapsClient.GetRouteInformation(tour.StartingArea, tour.TargetArea, true);

            if (!routeInfo.RouteExists)
                throw new Exception($"no route found between {tour.StartingArea} and {tour.TargetArea}");

            tour.Distance = routeInfo.Distance;
            tour.Image = routeInfo.ImagePath;

            await Task.Run(() => toursRepo.Update(currentName, currentImage, tour));

            logger.Debug($"updating tour {tour.Name}");
            TriggerDataChangedEvent();
        }
        public Tour GetTour(string name)
        {
            return toursRepo.TourExists(name) ? toursRepo.GetTour(name) : throw new Exception($"tour {name} does not exist");
        }

        public List<Tour> GetTours(int? limit = null)
        {
            return toursRepo.GetTours(limit).ToList();
        }

        public async Task DeleteTour(Tour tour)
        {
            if (!toursRepo.TourExists(tour.Name))
                throw new Exception($"Tour {tour.Name} does not exist");

            await Task.Run(() => toursRepo.Delete(tour));
            logger.Debug($"deleting tour {tour.Name}");
          
            // throws an exception when triggered from the task thread
            TriggerDataChangedEvent();
        }

        #endregion

        #region TourLog Crud Methods
        public List<TourLog> GetLogs(string tourName)
        {
            return toursRepo.TourExists(tourName) ? toursRepo.GetLogs(tourName).ToList() : throw new Exception($"tour {tourName} does not exist");
        }

        public async Task CreateTourLog(TourLog log)
        {
            await CreateUpdateLogWrapper(log, () => toursRepo.AddLog(log.TourName, log));
            logger.Debug($"creating tour log for {log.TourName}");
        }

        public async Task UpdateTourLog(TourLog log)
        {
            await CreateUpdateLogWrapper(log, () => toursRepo.UpdateLog(log));
            logger.Debug($"updating tour log {log.Id} for {log.TourName}");
        }

        private async Task CreateUpdateLogWrapper(TourLog log, Action action)
        {
            if (!ValidateTourLog(log))
                throw new Exception("invalid tour log!");

            if (!toursRepo.TourExists(log.TourName))
                throw new Exception($"Tour {log.TourName} does not exist");

            await Task.Run(() => { action(); });

            TriggerDataChangedEvent();
        }
        public async Task DeleteTourLog(TourLog log)
        {
            if (!toursRepo.LogExists(log.Id))
                throw new Exception($"Log with id {log.Id} does not exist");

            await Task.Run(() => toursRepo.DeleteLog(log));
            logger.Info("Deleting log with id " + log.Id);
            TriggerDataChangedEvent();
        }
        #endregion

        #region validation methods
        public bool ValidateTour(Tour tour)
        {
            return new List<string>() { tour.Name, tour.Description, tour.StartingArea, tour.TargetArea }.All(el => el.HasValue());
        }

        public bool ValidateTourLog(TourLog log)
        {
            return log.Rating >= 0 && log.Rating <= 10 && log.Report.HasValue() && log.TotalTime > 0 && log.Accomodation.HasValue() && log.Author.HasValue() && log.Members >= 0 && log.Members <= 100 && log.TourName.HasValue();
        }
        #endregion

        #region unused code (for now (forever?))
        //public event EventHandler<TourAddedEventArgs> TourAdded;
        //public event EventHandler<TourDeletedEventArgs> TourDeleted;
        //public event EventHandler<TourUpdatedEventArgs> TourUpdated;

        //public void TriggerTourUpdatedEvent(string oldName, Tour tour) => TourUpdated?.Invoke(this, new TourUpdatedEventArgs(oldName, tour));
        //public void TriggerTourDeletedEvent(Tour tour) => TourDeleted?.Invoke(this, new TourDeletedEventArgs(tour));
        //public void TriggerTourAddedEvent(Tour tour) => TourAdded?.Invoke(this, new TourAddedEventArgs(tour));

        #endregion
    }
}
