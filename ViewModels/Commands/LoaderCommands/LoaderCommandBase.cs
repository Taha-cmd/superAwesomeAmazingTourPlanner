using System;
using System.Collections.Generic;
using System.Text;
using ViewModels.ViewModels;

namespace ViewModels.Commands
{
    public class LoaderCommandBase : CommandBase
    {
        private readonly MainViewModel mainViewModel;
        public LoaderCommandBase(object param) => mainViewModel = (MainViewModel)param;
        public void LoadViewModel(object vm)
        {
            mainViewModel.CurrentViewModel = (ViewModelBase)vm;
            mainViewModel.Logger.Debug($"loading viewmodel of type {vm.GetType()} with hash {vm.GetHashCode()}");
        }
    }
}
