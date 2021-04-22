using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using ViewModels.Enums;
using ViewModels.ViewModels;

namespace ViewModels.Commands
{
    class UpdateTourCommand : CommandBase, ICommand
    {
        private CreateOrUpdateTourViewModel viewModel;
        public UpdateTourCommand(object tourViewModel)
        {
            viewModel = (CreateOrUpdateTourViewModel)tourViewModel;
            RegisterAllProperties(viewModel);
        }
        public override bool CanExecute(object parameter) => viewModel.Manager.ValidateTour(viewModel.Tour);
        public async void Execute(object parameter)
        {
            await CreateOrUpdate(viewModel, () => viewModel.Manager.UpdateTour(viewModel.OldName, viewModel.OldImage, viewModel.Tour));
        }
    }
}
