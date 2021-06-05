using Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using ViewModels.Commands;
using ViewModels.Enums;

namespace ViewModels.ViewModels
{
    public class ToursViewModel : ViewModelBase, IFilterable, IStatusDisplay
    {
        public ObservableCollection<Tour> Data { get; } // data to be displayed
        private List<Tour> items; // actual data
        public ICommand SearchCommand { get; }
        public ICommand ExportTourCommand { get; }

        private Status status = Status.Empty;
        private string statusMessage = string.Empty;
        public Status Status { get => status; set => SetValue(ref status, value, nameof(Status)); }
        public string StatusMessage { get => statusMessage; set => SetValue(ref statusMessage, value, nameof(StatusMessage)); }

        public string Operation => throw new NotImplementedException();

        public ToursViewModel() : base("Tours", "Tours")
        {
            Data = new ObservableCollection<Tour>();
            SearchCommand = CommandFactory.CreateCommand<SearchCommand>(this);
            //ExportTourCommand = CommandFactory.CreateCommand<ExportTourCommand>(this);
            LoadTours();

            Manager.DataChanged += (object sender, EventArgs e) =>
            {
                LoadTours();
                Logger.Info($"tours view model loading tours after a change occured");
            };
        }
        private void LoadTours()
        {
            items?.Clear(); 
            Data.Clear();
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
