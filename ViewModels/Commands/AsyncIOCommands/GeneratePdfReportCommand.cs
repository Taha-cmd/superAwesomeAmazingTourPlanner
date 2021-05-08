using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using ViewModels.ViewModels;

namespace ViewModels.Commands
{
    public class GeneratePdfReportCommand : AsyncOperationWithStatusCommandBase, ICommand
    {
        private MainViewModel mainViewModel;
        public GeneratePdfReportCommand(object vm) => mainViewModel = (MainViewModel)vm;
        public void Execute(object parameter)
        {
            throw new NotImplementedException();
        }
    }
}
