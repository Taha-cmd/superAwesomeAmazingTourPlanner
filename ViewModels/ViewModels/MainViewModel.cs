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

        private ICommand changePageCommand;
        private ICommand loadTourCommand;
        private ICommand loadTourLogFormCommand;
        private ICommand loadLogCommand;
        private List<ViewModelBase> viewModels;
        private ViewModelBase currentViewModel;
        private object parameter;

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

        public List<ViewModelBase> ViewModels { get => viewModels; }
        public ICommand ChangePageCommand { get => changePageCommand; }
        public ICommand LoadTourCommand { get => loadTourCommand; }
        public ICommand LoadTourLogFormCommand { get => loadTourLogFormCommand; }
        public ICommand LoadLogCommand { get => loadLogCommand; }
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

        public void LoadTour(Tour tour)
        {
            Console.WriteLine($"MainViewModel with id ({this.GetHashCode()}) loading tour: " + tour.Name);
            CurrentViewModel = new TourViewModel(tour);
        }

        public void LoadTourLogForm(string tourName)
        {
            CurrentViewModel = new CreateTourLogViewModel(tourName);
        }
    }
}
