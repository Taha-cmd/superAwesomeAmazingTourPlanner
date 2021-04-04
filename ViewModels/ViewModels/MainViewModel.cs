using BusinessLogic;
using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using ViewModels.Commands;

namespace ViewModels.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        // https://rachel53461.wordpress.com/2011/12/18/navigation-with-mvvm-2/

        public MainViewModel()
        {
            changePageCommand = CommandFactory.CreateCommand<ChangePageCommand>(this);
            loadTourCommand = CommandFactory.CreateCommand<LoadTourCommand>(this);
            loadTourLogFormCommand = CommandFactory.CreateCommand<LoadTourLogFormCommand>(this);
            loadLogCommand = CommandFactory.CreateCommand<LoadLogCommand>(this);

            viewModels = new List<ViewModelBase>() 
            {
                new HomeViewModel(),
                new ToursViewModel(),
                new CreateTourViewModel()
            };

            CurrentViewModel = viewModels[0];
        }

        #region fields
        private ICommand changePageCommand;
        private ICommand loadTourCommand;
        private ICommand loadTourLogFormCommand;
        private ICommand loadLogCommand;
        private List<ViewModelBase> viewModels;
        private ViewModelBase currentViewModel;
        private object parameter;
        #endregion

        #region properties
        public ICommand ChangePageCommand { get => changePageCommand; }
        public ICommand LoadTourCommand { get => loadTourCommand; }
        public ICommand LoadTourLogFormCommand { get => loadTourLogFormCommand; }
        public ICommand LoadLogCommand { get => loadLogCommand; }
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
        public void LoadTour(Tour tour)
        {
            Console.WriteLine($"MainViewModel with id ({GetHashCode()}) loading tour: " + tour.Name);
            CurrentViewModel = new TourViewModel(tour);
        }

        public void LoadTour(string tourName)
        {
            var tour = Manager.GetTour(tourName);
            Console.WriteLine("loading tour by name : " + tourName);
            CurrentViewModel = new TourViewModel(tour);
        }

        public void LoadTourLogForm(string tourName)
        {
            CurrentViewModel = new CreateTourLogViewModel(tourName);
        }

        #endregion
    }
}
