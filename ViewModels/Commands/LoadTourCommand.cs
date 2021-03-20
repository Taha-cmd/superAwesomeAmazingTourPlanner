using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using ViewModels.ViewModels;

namespace ViewModels.Commands
{
    public class LoadTourCommand : CommandBase, ICommand
    {
        private MainViewModel mainViewModel;
        public LoadTourCommand(object parameter)
        {
            this.mainViewModel = (MainViewModel)parameter;
        }
        public bool CanExecute(object parameter) => true;
        public void Execute(object parameter)
        {
            string tourName = (string)parameter;
            Console.WriteLine(tourName);
            mainViewModel.LoadTourByName(tourName);
        }
    }
}
