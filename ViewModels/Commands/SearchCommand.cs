using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using ViewModels.ViewModels;
using Extensions;

namespace ViewModels.Commands
{
    class SearchCommand : CommandBase, ICommand
    {
        private IFilterable ViewModel;
        public SearchCommand(object viewModel) => ViewModel = (IFilterable)viewModel;
        public void Execute(object parameter) => ViewModel.Filter((string)parameter);
            
    }
}
