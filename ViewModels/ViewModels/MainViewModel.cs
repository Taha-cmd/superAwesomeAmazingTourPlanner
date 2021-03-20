using BusinessLogic;
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

        public void LoadTourByName(string name)
        {
            CurrentViewModel = new TourViewModel(new Models.Tour() { Name = name });           
        }
    }
}
