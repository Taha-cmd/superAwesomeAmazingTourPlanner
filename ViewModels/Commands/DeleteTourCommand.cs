using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using ViewModels.ViewModels;

namespace ViewModels.Commands
{
    public class DeleteTourCommand : CommandBase, ICommand
    {
        private MainViewModel mainViewModel;
        public DeleteTourCommand(object parameter) => this.mainViewModel = (MainViewModel)parameter;
        public async void Execute(object parameter) =>  await mainViewModel.Manager.DeleteTour((Tour)parameter);
    }
}
