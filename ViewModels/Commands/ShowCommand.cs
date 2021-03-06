using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ViewModels.Commands
{
    public class ShowCommand : ICommand
    {
        public MainWindowViewModel MainViewModel { get; set; }

        public ShowCommand(MainWindowViewModel viewModel)
        {
            MainViewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) 
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Console.WriteLine("command triggered!");
            MainViewModel.ReverseName();
            
        }
    }
}
