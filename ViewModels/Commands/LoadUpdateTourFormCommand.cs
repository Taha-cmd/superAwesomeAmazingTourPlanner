using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using ViewModels.ViewModels;

namespace ViewModels.Commands
{
    public class LoadUpdateTourFormCommand : CommandBase, ICommand
    {
        private MainViewModel mainViewModel;
        public LoadUpdateTourFormCommand(object parameter) => this.mainViewModel = (MainViewModel)parameter;
        public void Execute(object parameter) => mainViewModel.CurrentViewModel = new CreateOrUpdateTourViewModel(((TourViewModel)parameter).Tour);
    }
}
