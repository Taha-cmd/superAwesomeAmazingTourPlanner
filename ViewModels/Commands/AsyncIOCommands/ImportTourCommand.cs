using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using ViewModels.ViewModels;

namespace ViewModels.Commands
{
    public class ImportTourCommand : AsyncOperationWithStatusCommandBase, ICommand
    {
        private readonly CreateOrUpdateTourViewModel viewModel;
        public ImportTourCommand(object param) => viewModel = (CreateOrUpdateTourViewModel)param;
        public async void Execute(object parameter) => await AsyncOperationWrapper(viewModel, () => viewModel.Manager.Import((string)parameter));
    }
}
