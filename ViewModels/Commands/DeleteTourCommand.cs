using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using ViewModels.ViewModels;

namespace ViewModels.Commands
{
    public class DeleteTourCommand : CommandBase, ICommand
    {
        public DeleteTourCommand(object _) { }
        public async void Execute(object parameter) =>  await manager.DeleteTour((Tour)parameter);
    }
}
