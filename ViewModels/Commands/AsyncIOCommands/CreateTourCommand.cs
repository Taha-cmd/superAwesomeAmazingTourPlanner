﻿using System;
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
        private TourFormViewModel viewModel;
        public CreateTourCommand(object tourViewModel)
        {
            viewModel = (TourFormViewModel)tourViewModel;
            RegisterAllProperties(viewModel);
        }

        public override bool CanExecute(object parameter) => viewModel.Manager.ValidateTour(viewModel.Tour);
        public async void Execute(object parameter) => await AsyncOperationWrapper(viewModel, () => viewModel.Manager.CreateTour(viewModel.Tour));
    }
}