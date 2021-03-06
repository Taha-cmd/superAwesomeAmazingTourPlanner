using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using ViewModels.ViewModels;
using Extensions;
using System.Linq;
using ViewModels.Enums;

namespace ViewModels.Commands
{
    public class CreateTourLogCommand : AsyncOperationWithStatusCommandBase, ICommand
    {
        private readonly TourLogFormViewModel viewModel;

        public CreateTourLogCommand(object parameter)
        {
            viewModel = (TourLogFormViewModel)parameter;
            RegisterAllProperties(viewModel);
        }
        // validate that the input is not empty

        public override bool CanExecute(object parameter) => manager.ValidateTourLog(viewModel.Log);   
        public async void Execute(object parameter) => await AsyncOperationWrapper(viewModel, () => manager.CreateTourLog(viewModel.Log));

    }
}
