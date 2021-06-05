using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using ViewModels.ViewModels;

namespace ViewModels.Commands
{
    public class GeneratePdfReportCommand : AsyncOperationWithStatusCommandBase, ICommand
    {
        private MainViewModel mainViewModel;
        public GeneratePdfReportCommand(object vm) => mainViewModel = (MainViewModel)vm;
        public async void Execute(object parameter)
        {
            await AsyncOperationWrapper((IStatusDisplay)mainViewModel.CurrentViewModel, () => manager.GenerateReport((Tour)parameter));
        }
    }
}
