using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using ViewModels.ViewModels;

namespace ViewModels.Commands
{
    public class ExportTourCommand : AsyncOperationWithStatusCommandBase, ICommand
    {
        private readonly MainViewModel viewModel;
        public ExportTourCommand(object param) => viewModel = (MainViewModel)param;
        public async void Execute(object parameter) => await AsyncOperationWrapper((IStatusDisplay)viewModel.CurrentViewModel, () => viewModel.Manager.Export((Tour)parameter));
    }
}
