using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using ViewModels.ViewModels;
using Models;

namespace ViewModels.Commands
{
    public class DeleteTourLogCommand : AsyncOperationWithStatusCommandBase, ICommand
    {
        private readonly MainViewModel viewModel;
        public DeleteTourLogCommand(object vm) => viewModel = (MainViewModel)vm;
        public async void Execute(object parameter) 
        {
            await AsyncOperationWrapper((IStatusDisplay)viewModel.CurrentViewModel, () => manager.DeleteTourLog((TourLog)parameter));
        }
    }
}
