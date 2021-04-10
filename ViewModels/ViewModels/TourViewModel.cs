using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Models;
using ViewModels.Commands;

namespace ViewModels.ViewModels
{
    public class TourViewModel : ViewModelBase, IFilterable
    {
        private readonly Tour tour;

        #region Tour Properties
        public string Name
        {
            get => tour.Name;
            set
            {
                tour.Name = value;
                TriggerPropertyChangedEvent(nameof(Name));
            }
        }

        public string Description
        {
            get => tour.Description;
            set
            {
                tour.Description = value;
                TriggerPropertyChangedEvent(nameof(Name));
            }
        }

        public double Distance
        {
            get => tour.Distance;
            set
            {
                tour.Distance = value;
                TriggerPropertyChangedEvent(nameof(Distance));
            }
        }

        public string StartingArea
        {
            get => tour.StartingArea;
            set
            {
                tour.StartingArea = value;
                TriggerPropertyChangedEvent(nameof(StartingArea));
            }
        }

        public string TargetArea
        {
            get => tour.TargetArea;
            set
            {
                tour.TargetArea = value;
                TriggerPropertyChangedEvent(nameof(TargetArea));
            }
        }

        public ICommand SearchCommand { get; }

        public ObservableCollection<TourLog> Logs { get; } 

        #endregion

        public TourViewModel(Tour tour)
        {
            this.tour = tour;
            ViewName = "Tour";
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
