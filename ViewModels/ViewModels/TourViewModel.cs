using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Models;
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

        public ObservableCollection<TourLog> Logs { get; }

        private Status status = Status.Empty;
        private string statusMessage = string.Empty;
        public Status Status { get => status; set => SetValue(ref status, value, nameof(Status)); }
        public string StatusMessage { get => statusMessage; set => SetValue(ref statusMessage, value, nameof(StatusMessage)); }

        public string Operation => throw new NotImplementedException();

        #endregion

        public TourViewModel(Tour tour) : base("Tour", tour.Name)
        {
            this.tour = tour;
            Logs = new ObservableCollection<TourLog>(tour.Logs);
            SearchCommand = CommandFactory.CreateCommand<SearchCommand>(this);
        }

        public void Filter(string filter)
        {
            Logs.Clear();
            tour.Logs.Where(log => log.Report.Contains(filter)).ToList().ForEach(item => Logs.Add(item));
        }
    }
}
