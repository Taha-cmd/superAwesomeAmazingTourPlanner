using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using ViewModels.Enums;
using ViewModels.ViewModels;

namespace ViewModels.Commands
{
    public class UpdateTourCommand : AsyncOperationWithStatusCommandBase, ICommand
    {
        private readonly TourFormViewModel viewModel;
        public UpdateTourCommand(object tourViewModel)
        {
            viewModel = (TourFormViewModel)tourViewModel;
            RegisterAllProperties(viewModel);
        }
        public override bool CanExecute(object parameter) => manager.ValidateTour(viewModel.Tour);
        public async void Execute(object parameter)
        {
            await AsyncOperationWrapper(viewModel, () => manager.UpdateTour(viewModel.OldName, viewModel.OldImage, viewModel.Tour));
        }
    }
}
