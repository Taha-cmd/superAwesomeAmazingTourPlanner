using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using ViewModels.ViewModels;

namespace ViewModels.Commands
{
    public class CopyTourCommand : AsyncOperationWithStatusCommandBase, ICommand
    {
        private readonly MainViewModel mainViewModel;
        public CopyTourCommand(object param) => mainViewModel = (MainViewModel)param;
        public async void Execute(object parameter)
        {
            await AsyncOperationWrapper((IStatusDisplay)mainViewModel.CurrentViewModel, () => manager.Copy((Tour)parameter));
        }
    }
}
