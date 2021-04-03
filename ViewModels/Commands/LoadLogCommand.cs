using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using ViewModels.ViewModels;

namespace ViewModels.Commands
{
    public class LoadLogCommand : CommandBase, ICommand
    {
        private MainViewModel mainViewModel;

        public LoadLogCommand(object parameter)
        {
            mainViewModel = (MainViewModel)parameter;
        }
        public bool CanExecute(object parameter) => true;
        
        public void Execute(object parameter)
        {
            Console.WriteLine($"MainViewModel with id ({mainViewModel.GetHashCode()}) loading log: ");
            throw new NotImplementedException();
        }
    }
}
