using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using ViewModels.ViewModels;
using System.Linq;

namespace ViewModels.Commands
{
    public class ChangePageCommand : CommandBase, ICommand
    {
        private MainViewModel mainViewModel;
        public ChangePageCommand(object viewModel)
        {
            mainViewModel = (MainViewModel)viewModel;
            Console.WriteLine("command created from: " + viewModel.GetType().Name);
            RegisterSubscriptionToPropertyChanged(mainViewModel, nameof(mainViewModel.CurrentViewModel));
        }
        public bool CanExecute(object parameter) => true;
        public void Execute(object parameter)
        {
            ViewModelBase viewModel = (ViewModelBase)parameter;
            
            if (!mainViewModel.ViewModels.Contains(viewModel))
                mainViewModel.ViewModels.Add(viewModel);

            mainViewModel.CurrentViewModel = mainViewModel.ViewModels.FirstOrDefault(vm => vm == viewModel);
            Console.WriteLine("changing page to :" + viewModel.ViewName);
        }
    }
}
