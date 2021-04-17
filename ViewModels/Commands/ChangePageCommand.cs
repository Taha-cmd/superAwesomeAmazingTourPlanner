using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using ViewModels.ViewModels;
using System.Linq;
using Models;
using System.Diagnostics;

namespace ViewModels.Commands
{
    public class ChangePageCommand : CommandBase, ICommand
    {
        private MainViewModel mainViewModel;
        public ChangePageCommand(object viewModel)
        {
            mainViewModel = (MainViewModel)viewModel;
            RegisterSubscriptionToPropertyChanged(mainViewModel, nameof(mainViewModel.CurrentViewModel));
        }
        public void Execute(object parameter)
        {
            ViewModelBase viewModel = (ViewModelBase)parameter;
            
            if (!mainViewModel.ViewModels.Contains(viewModel))
                mainViewModel.ViewModels.Add(viewModel);

            mainViewModel.CurrentViewModel = mainViewModel.ViewModels.FirstOrDefault(vm => vm == viewModel);
            mainViewModel.CurrentViewModel.Reset();
            Debug.WriteLine($"MainViewModel with id ({mainViewModel.GetHashCode()}) changing page to :" + viewModel.ViewName + $" id: {viewModel.GetHashCode()}");
        }
    }
}
