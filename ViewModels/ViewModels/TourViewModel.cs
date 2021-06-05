using BusinessLogic;
using Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using ViewModels.Commands;
using ViewModels.Enums;

namespace ViewModels.ViewModels
{
    public class TourViewModel : ViewModelBase, IFilterable, IStatusDisplay
    {
        private readonly Tour tour;
        public Tour Tour { get => tour; }

        #region Tour Properties
        public string Name
        {
            get => tour.Name;
            set => SetValue(tour, value, nameof(Name));
        }

        public string Description
        {
            get => tour.Description;
            set => SetValue(tour, value, nameof(Description));
        }

        public double Distance
        {
            get => tour.Distance;
            set => SetValue(tour, value, nameof(Distance));
        }

        public string StartingArea
        {
            get => tour.StartingArea;
            set => SetValue(tour, value, nameof(StartingArea));
        }

        public string TargetArea
        {
            get => tour.TargetArea;
            set => SetValue(tour, value, nameof(TargetArea));
        }

        public string Image
        {
            get => tour.Image;
            set => SetValue(tour, value, nameof(Image));
        }

        public ICommand SearchCommand { get; }

        public ObservableCollection<TourLog> Logs { get; private set; }

        private Status status = Status.Empty;
        private string statusMessage = string.Empty;
        public Status Status { get => status; set => SetValue(ref status, value, nameof(Status)); }
        public string StatusMessage { get => statusMessage; set => SetValue(ref statusMessage, value, nameof(StatusMessage)); }

        public string Operation => throw new NotImplementedException();

        #endregion

        public TourViewModel(Tour tour) : base("Tour", tour.Name, Application.GetLogger())
        {
            this.tour = tour;
            Logs = new ObservableCollection<TourLog>(tour.Logs);
            SearchCommand = CommandFactory.CreateCommand<SearchCommand>(this);

            Manager.DataChanged += (sender, args) =>
            {
                // known bug:
                // DataChanged event will be triggered whenever something changes in the persistence layer (tour crud / log crud)
                // when a tour is deleted, a cached tour viewModel will try to load the logs for this tour
                // since the tour does not exist anymore, an exception will be thrown
                // actual solution: make seperate events
                // for now this will work
                try
                {
                    tour.Logs = Manager.GetLogs(tour.Name); // actual data
                    Logs.Clear(); // data to be displayed

                    tour.Logs.ForEach(log => Logs.Add(log));
                }
                catch(Exception)
                {

                }

            };
        }

        public void Filter(string filter)
        {
            Logs.Clear();
            tour.Logs.Where(log => log.Report.Contains(filter)).ToList().ForEach(item => Logs.Add(item));
            Logger.Debug($"filtering tour logs of tour ({tour.Name}) with search string {filter}");
        }
    }
}
