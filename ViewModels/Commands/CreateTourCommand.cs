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
        private CreateTourViewModel createTourViewModel;
        public CreateTourCommand(object tourViewModel)
        {
            createTourViewModel = (CreateTourViewModel)tourViewModel;

            createTourViewModel
                .GetType()
                .GetProperties()
                .ToList()
                .ForEach(prop => RegisterSubscriptionToPropertyChanged(createTourViewModel, prop.Name));
        }

        public bool CanExecute(object parameter)
        {
            return
                !createTourViewModel.Name.IsEmptyOrWhiteSpace() &&
                !createTourViewModel.StartingArea.IsEmptyOrWhiteSpace() &&
                !createTourViewModel.TargetArea.IsEmptyOrWhiteSpace() &&
                !createTourViewModel.Description.IsEmptyOrWhiteSpace();
        }
        public void Execute(object parameter)
        {
            try
            {
                //throw new Exception();

                createTourViewModel.Manager.CreateTour
                    (
                        new Tour() 
                        { 
                            Description = createTourViewModel.Description, 
                            Name = createTourViewModel.Name,
                            StartingArea = createTourViewModel.StartingArea,
                            TargetArea = createTourViewModel.TargetArea
                        }
                    );

                createTourViewModel.Status = Status.Success;
                // after a successfull creation, clear the input fields
                createTourViewModel.clearProperties();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                createTourViewModel.Status = Status.Failure;
            }
        }
    }
}
