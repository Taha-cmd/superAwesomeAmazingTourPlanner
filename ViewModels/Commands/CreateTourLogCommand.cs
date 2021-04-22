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
        // validate that the input is not empty
        public override bool CanExecute(object parameter) => viewModel.Report.HasValue() && viewModel.TotalTime != 0;   
        public async void Execute(object parameter) => await CreateOrUpdate(viewModel, () => viewModel.Manager.CreateTourLog(viewModel.TourName, viewModel.Log));
    }
}
