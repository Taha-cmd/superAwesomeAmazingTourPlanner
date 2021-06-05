using System.Windows.Input;
using ViewModels.ViewModels;

namespace ViewModels.Commands
{
    class SearchCommand : CommandBase, ICommand
    {
        private readonly IFilterable viewModel;
        public SearchCommand(object viewModel) => this.viewModel = (IFilterable)viewModel;
        public void Execute(object parameter) => viewModel.Filter((string)parameter);
            
    }
}
