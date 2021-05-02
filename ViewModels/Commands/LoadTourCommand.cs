using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using ViewModels.ViewModels;

namespace ViewModels.Commands
{
    public class LoadTourCommand : CommandBase, ICommand
    {
        private MainViewModel mainViewModel;
        public LoadTourCommand(object parameter) => this.mainViewModel = (MainViewModel)parameter;
        public void Execute(object parameter)
        {
            mainViewModel.CurrentViewModel = new TourViewModel((Tour)parameter);
            mainViewModel.Logger.Debug($"loading tour {((Tour)parameter).Name} with hash {parameter.GetHashCode()}");
        }
    }
}