using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using ViewModels.ViewModels;

namespace ViewModels.Commands
{
    public class LoadUpdateTourFormCommand : LoaderCommandBase, ICommand
    {
        public LoadUpdateTourFormCommand(object parameter) : base(parameter) { }
        public void Execute(object parameter) => LoadViewModel(new CreateOrUpdateTourViewModel(((TourViewModel)parameter).Tour));
    }
}
