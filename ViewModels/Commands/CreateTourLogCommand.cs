using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using ViewModels.ViewModels;

namespace ViewModels.Commands
{
    public class CreateTourLogCommand : CommandBase, ICommand
    {
        private readonly CreateTourLogViewModel viewModel;

        public CreateTourLogCommand(object parameter)
        {
            viewModel = (CreateTourLogViewModel)parameter;
        }
        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            var log = new TourLog()
            {
                DateTime = viewModel.DateTime,
                Rating = viewModel.Rating,
                Report = viewModel.Report,
                TotalTime = viewModel.TotalTime
            };

            viewModel.Manager.CreateTourLog(viewModel.TourName, log);

            // what to do after creation?
        }
    }
}
