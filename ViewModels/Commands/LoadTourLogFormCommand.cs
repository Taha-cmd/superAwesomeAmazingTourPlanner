using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using ViewModels.ViewModels;

namespace ViewModels.Commands
{
    public class LoadTourLogFormCommand : CommandBase, ICommand
    {
        private MainViewModel mainViewModel;

        public LoadTourLogFormCommand(object viewModel)
        {
            this.mainViewModel = (MainViewModel)viewModel;
        }

        public bool CanExecute(object parameter) => true;
        
        public void Execute(object parameter)
        {
            mainViewModel.LoadTourLogForm(((TourViewModel)parameter).Name);
        }
    }
}
