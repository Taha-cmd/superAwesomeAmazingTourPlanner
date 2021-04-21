using BusinessLogic;
using Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using ViewModels.Commands;

namespace ViewModels.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        // https://rachel53461.wordpress.com/2011/12/18/navigation-with-mvvm-2/

        public MainViewModel() : base("MainWindow", "Main View Model")
        {
            ChangePageCommand = CommandFactory.CreateCommand<ChangePageCommand>(this);
            LoadTourCommand = CommandFactory.CreateCommand<LoadTourCommand>(this);
            LoadTourLogFormCommand = CommandFactory.CreateCommand<LoadTourLogFormCommand>(this);
            LoadLogCommand = CommandFactory.CreateCommand<LoadLogCommand>(this);
            DeleteTourCommand = CommandFactory.CreateCommand<DeleteTourCommand>(this);
            LoadUpdateTourFormCommand = CommandFactory.CreateCommand<LoadUpdateTourFormCommand>(this);


            ViewModels = new List<ViewModelBase>() 
            {
                new HomeViewModel(),
                new ToursViewModel(),
                new CreateOrUpdateTourViewModel()
            };

            CurrentViewModel = ViewModels[0];
        }

        #region fields
        private ViewModelBase currentViewModel;
        #endregion

        #region properties
        public ICommand ChangePageCommand { get; }
        public ICommand LoadTourCommand { get; }
        public ICommand LoadTourLogFormCommand { get; }
        public ICommand LoadLogCommand { get; }
        public ICommand DeleteTourCommand { get; }
        public ICommand LoadUpdateTourFormCommand { get; }
        public List<ViewModelBase> ViewModels { get; }

        public ViewModelBase CurrentViewModel
        {
            get => currentViewModel;
            set => SetValue(ref currentViewModel, value, nameof(CurrentViewModel));
        }
        #endregion

        #region methods
        public void LoadTour(Tour tour) => CurrentViewModel = new TourViewModel(tour);
        public void LoadTour(string tourName) => CurrentViewModel = new TourViewModel(Manager.GetTour(tourName));
        public void LoadTourLogForm(string tourName) => CurrentViewModel = new CreateTourLogViewModel(tourName);
        public void LoadUpdateTourForm(Tour tour) => CurrentViewModel = new CreateOrUpdateTourViewModel(tour);

        #endregion
    }
}
