using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Extensions;
using ViewModels.ViewModels;
using System.Linq;
using Models;
using ViewModels.Enums;

namespace ViewModels.Commands
{
    public class CreateTourCommand : AsyncOperationWithStatusCommandBase, ICommand
    {
        private readonly TourFormViewModel viewModel;
        public CreateTourCommand(object tourViewModel)
        {
            viewModel = (TourFormViewModel)tourViewModel;
            RegisterAllProperties(viewModel);
        }

        public override bool CanExecute(object parameter) => manager.ValidateTour(viewModel.Tour);
        public async void Execute(object parameter) => await AsyncOperationWrapper(viewModel, () => manager.CreateTour(viewModel.Tour));
    }
}
