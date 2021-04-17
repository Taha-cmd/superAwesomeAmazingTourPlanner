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
            changePageCommand = CommandFactory.CreateCommand<ChangePageCommand>(this);
            loadTourCommand = CommandFactory.CreateCommand<LoadTourCommand>(this);
            loadTourLogFormCommand = CommandFactory.CreateCommand<LoadTourLogFormCommand>(this);
            loadLogCommand = CommandFactory.CreateCommand<LoadLogCommand>(this);
            deleteTourCommand = CommandFactory.CreateCommand<DeleteTourCommand>(this);
            loadUpdateTourFormCommand = CommandFactory.CreateCommand<LoadUpdateTourFormCommand>(this);


            viewModels = new List<ViewModelBase>() 
            {
                new HomeViewModel(),
                new ToursViewModel(),
                new CreateOrUpdateTourViewModel()
            };

            CurrentViewModel = viewModels[0];
        }

        #region fields
        private ICommand changePageCommand;
        private ICommand loadTourCommand;
        private ICommand loadTourLogFormCommand;
        private ICommand loadLogCommand;
        private ICommand loadUpdateTourFormCommand;
        private ICommand deleteTourCommand;
        
        private List<ViewModelBase> viewModels;
        private ViewModelBase currentViewModel;
        private object parameter;
        #endregion

        #region properties
        public ICommand ChangePageCommand { get => changePageCommand; }
        public ICommand LoadTourCommand { get => loadTourCommand; }
        public ICommand LoadTourLogFormCommand { get => loadTourLogFormCommand; }
        public ICommand LoadLogCommand { get => loadLogCommand; }
        public ICommand DeleteTourCommand { get => deleteTourCommand; }
        public ICommand LoadUpdateTourFormCommand { get => loadUpdateTourFormCommand; }
        public List<ViewModelBase> ViewModels { get => viewModels; }
        
        public ViewModelBase CurrentViewModel
        {
            get => currentViewModel;
            set
            {
                currentViewModel = value;
                TriggerPropertyChangedEvent(nameof(CurrentViewModel));
            }
        }

        public object Parameter
        {
            get => parameter;
            set
            {
                parameter = value;
                TriggerPropertyChangedEvent(nameof(Parameter));
            }
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
