using Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using ViewModels.ViewModels;

namespace ViewModels.Commands
{
    public class UpdateTourLogCommand : AsyncOperationWithStatusCommandBase, ICommand
    {
        private readonly TourLogFormViewModel viewModel;
        public UpdateTourLogCommand(object vm) => viewModel = (TourLogFormViewModel)vm;
        
        public async void Execute(object parameter)
        {
            await AsyncOperationWrapper(viewModel, () => viewModel.Manager.UpdateTourLog(viewModel.Log));
        }
    }
}
