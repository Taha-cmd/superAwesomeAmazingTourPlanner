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
            if (parameter.GetType().Name == nameof(String))
                mainViewModel.LoadTour((string)parameter);
            else
                mainViewModel.LoadTour((Tour)parameter);
        }
    }
}