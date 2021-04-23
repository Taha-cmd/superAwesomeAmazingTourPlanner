using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using ViewModels.ViewModels;

namespace ViewModels.Commands
{
    public class ImportTourCommand : CommandBase, ICommand
    {
        private readonly CreateOrUpdateTourViewModel viewModel;
        public ImportTourCommand(object param) => viewModel = (CreateOrUpdateTourViewModel)param;
        public async void Execute(object parameter) => await CreateOrUpdate(viewModel, () => viewModel.Manager.Import((string)parameter));
    }
}
