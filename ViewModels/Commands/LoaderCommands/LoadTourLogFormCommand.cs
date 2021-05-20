using Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using ViewModels.ViewModels;

namespace ViewModels.Commands
{
    public class LoadTourLogFormCommand : LoaderCommandBase, ICommand
    {
        public LoadTourLogFormCommand(object viewModel) : base(viewModel) { }
        public void Execute(object parameter)
        {
            switch(parameter.GetType().Name)
            {
                case nameof(TourLog): LoadViewModel(new TourLogFormViewModel((TourLog)parameter)); return;
                case nameof(TourViewModel): LoadViewModel(new TourLogFormViewModel(((TourViewModel)parameter).Name)); return;
            }
        }
    }
}
