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
    public class CreateTourLogCommand : CommandBase, ICommand
    {
        private readonly CreateTourLogViewModel viewModel;

        public CreateTourLogCommand(object parameter)
        {
            viewModel = (CreateTourLogViewModel)parameter;
            RegisterAllProperties(viewModel);
        }
        public bool CanExecute(object parameter)
        {
            // validate that the input is not empty
            return viewModel.Report.HasValue() && viewModel.TotalTime != 0;
        }

        public void Execute(object parameter)
        {
            try
            {

                var log = new TourLog()
                {
                    DateTime = viewModel.DateTime,
                    Rating = viewModel.Rating,
                    Report = viewModel.Report,
                    TotalTime = viewModel.TotalTime
                };

                viewModel.Manager.CreateTourLog(viewModel.TourName, log);

                viewModel.Status = Status.Success;
                viewModel.ClearProperties();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                viewModel.Status = Status.Failure;
            }

            // what to do after creation?
        }
    }
}
