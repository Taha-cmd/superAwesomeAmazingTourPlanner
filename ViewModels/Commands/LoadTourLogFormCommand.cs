using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using ViewModels.ViewModels;

namespace ViewModels.Commands
{
    public class LoadTourLogFormCommand : LoaderCommandBase, ICommand
    {
        public LoadTourLogFormCommand(object viewModel) : base(viewModel) { }
        public void Execute(object parameter) => LoadViewModel(new CreateTourLogViewModel(((TourViewModel)parameter).Name));
    }
}
