using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using ViewModels.ViewModels;
using Extensions;

namespace ViewModels.Commands
{
    class SearchCommand : CommandBase, ICommand
    {
        private ToursViewModel toursViewModel;
        public SearchCommand(object viewModel)
        {
            toursViewModel = (ToursViewModel)viewModel;
        }
        public bool CanExecute(object parameter) => true;
        public void Execute(object parameter) => toursViewModel.FilterTours((string)parameter);
            
    }
}
