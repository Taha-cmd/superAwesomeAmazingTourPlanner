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
        public override bool CanExecute(object parameter) => manager.ValidateTourLog(viewModel.Log);
        public async void Execute(object parameter)
        {
            await AsyncOperationWrapper(viewModel, () => manager.UpdateTourLog(viewModel.Log));
        }
    }
}
