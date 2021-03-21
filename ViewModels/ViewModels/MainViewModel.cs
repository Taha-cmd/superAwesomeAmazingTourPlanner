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
        private List<ViewModelBase> viewModels;
        private ViewModelBase currentViewModel;
        private object parameter;

        public MainViewModel()
        {
            changePageCommand = CommandFactory.CreateCommand<ChangePageCommand>(this);
            loadTourCommand = CommandFactory.CreateCommand<LoadTourCommand>(this);

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
            CurrentViewModel = new TourViewModel(tour);
        }
    }
}
