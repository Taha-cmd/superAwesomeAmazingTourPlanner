using Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using ViewModels.Commands;
using System.Linq;
using Extensions;
using BusinessLogic;

namespace ViewModels.ViewModels
{
    public class ToursViewModel : ViewModelBase
    {
        public ObservableCollection<Tour> Data { get; } // data to be displayed
        private List<Tour> items; // actual data
        private Tour selectedItem;

        public Tour SelectedItem
        {
            get => selectedItem;
            set
            {
                selectedItem = value;
                TriggerPropertyChangedEvent(nameof(SelectedItem));
            }
        }

        public ICommand SearchCommand { get; }


        public ToursViewModel()
        {
            ViewName = "Tours";
            Data = new ObservableCollection<Tour>();
            SearchCommand = CommandFactory.CreateCommand<SearchCommand>(this);

             
            LoadFakeTours();
        }

        public void AddTour(Tour tour)
        {
            items.Add(tour);
            Data.Add(tour);
            Manager.CreateTour(tour);
        }

        private void LoadFakeTours()
        {
            items = Manager.GetTours();
            items.ForEach(item => Data.Add(item));
            SelectedItem = items[0];
        }
        
        public void FilterTours(string filter)
        {
            Data.Clear();
            items.Where(item => item.Name.Contains(filter)).ToList().ForEach(item => Data.Add(item));
        }
    }
}
