using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using ViewModels.ViewModels;

namespace ViewModels.Commands
{
    public class LoadTourCommand : LoaderCommandBase, ICommand
    {
        public LoadTourCommand(object parameter) : base(parameter) { }
        public void Execute(object parameter) => LoadViewModel(new TourViewModel((Tour)parameter));
    }
}