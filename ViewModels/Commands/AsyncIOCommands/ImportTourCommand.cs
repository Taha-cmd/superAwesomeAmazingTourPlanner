using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using ViewModels.ViewModels;

namespace ViewModels.Commands
{
    public class ImportTourCommand : AsyncOperationWithStatusCommandBase, ICommand
    {
        private readonly TourFormViewModel viewModel;
        public ImportTourCommand(object param) => viewModel = (TourFormViewModel)param;
        public async void Execute(object parameter) => await AsyncOperationWrapper(viewModel, () => manager.Import((string)parameter));
    }
}
