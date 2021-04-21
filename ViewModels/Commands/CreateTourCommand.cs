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
    class CreateTourCommand : CommandBase, ICommand
    {
        private CreateOrUpdateTourViewModel createTourViewModel;
        public CreateTourCommand(object tourViewModel)
        {
            createTourViewModel = (CreateOrUpdateTourViewModel)tourViewModel;
            RegisterAllProperties(createTourViewModel);
        }

        public override bool CanExecute(object parameter) => createTourViewModel.Manager.ValidateTour(createTourViewModel.Tour);
        public async void Execute(object parameter)
        {
            try
            {
                createTourViewModel.StatusMessage = string.Empty;
                createTourViewModel.Status = Status.Pending;
                await createTourViewModel.Manager.CreateTour(createTourViewModel.Tour);
                createTourViewModel.Status = Status.Success;
                // after a successfull creation, clear the input fields
                createTourViewModel.Clear();
            }
            catch(Exception ex)
            {
                createTourViewModel.StatusMessage = ex.Message;
                createTourViewModel.Status = Status.Failure;
            }
        }
    }
}
