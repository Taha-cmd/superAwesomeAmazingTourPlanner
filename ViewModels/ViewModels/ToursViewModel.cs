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
using BusinessLogic.CustomEventArgs;

namespace ViewModels.ViewModels
{
    public class ToursViewModel : ViewModelBase, IFilterable
    {
        public ObservableCollection<Tour> Data { get; } // data to be displayed
        private List<Tour> items; // actual data
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
        }

        private void LoadFakeTours()
        {
            items = Manager.GetTours();
            items.ForEach(item => Data.Add(item));
        }
        
        public void Filter(string filter)
        {
            Data.Clear();
            items.Where(item => item.Name.Contains(filter)).ToList().ForEach(item => Data.Add(item));
        }
    }
}
