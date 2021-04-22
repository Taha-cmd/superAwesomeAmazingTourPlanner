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
        public LoadTourLogFormCommand(object viewModel) => this.mainViewModel = (MainViewModel)viewModel;
        public void Execute(object parameter) => mainViewModel.CurrentViewModel =  new CreateTourLogViewModel(((TourViewModel)parameter).Name);
    }
}
