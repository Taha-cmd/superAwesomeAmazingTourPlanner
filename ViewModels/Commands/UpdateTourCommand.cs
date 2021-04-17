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
        private CreateOrUpdateTourViewModel createTourViewModel;
        public UpdateTourCommand(object tourViewModel)
        {
            createTourViewModel = (CreateOrUpdateTourViewModel)tourViewModel;
            RegisterAllProperties(createTourViewModel);
        }
        public override bool CanExecute(object parameter) => createTourViewModel.Manager.ValidateTour(createTourViewModel.Tour);
        public void Execute(object parameter)
        {
            
            try
            {
                createTourViewModel.Manager.UpdateTour(createTourViewModel.OldName, createTourViewModel.Tour);
                createTourViewModel.Status = Status.Success;
                // after a successfull creation, clear the input fields
                createTourViewModel.Clear();
            }
            catch (Exception ex)
            {
                createTourViewModel.StatusMessage = ex.Message;
                createTourViewModel.Status = Status.Failure;
            }
        }
    }
}
