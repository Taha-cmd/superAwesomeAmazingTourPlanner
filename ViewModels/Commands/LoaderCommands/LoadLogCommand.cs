using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using ViewModels.ViewModels;

namespace ViewModels.Commands
{
    public class LoadLogCommand : LoaderCommandBase, ICommand
    {
        public LoadLogCommand(object parameter) : base(parameter) { }
        public void Execute(object parameter) => LoadViewModel(new TourLogViewModel( (TourLog)parameter) );
    }
}
