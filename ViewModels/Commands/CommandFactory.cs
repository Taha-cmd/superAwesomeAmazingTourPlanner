using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using ViewModels;

namespace ViewModels.Commands
{
    class CommandFactory
    {
        public static ICommand CreateCommand<CommandType>(object viewModel) where CommandType : ICommand
        {
            switch (typeof(CommandType).Name)
            {
                case nameof(CreateTourCommand): return new CreateTourCommand(viewModel);
                case nameof(ChangePageCommand): return new ChangePageCommand(viewModel);
                case nameof(SearchCommand):     return new SearchCommand(viewModel);
                case nameof(LoadTourCommand): return new LoadTourCommand(viewModel);
                case nameof(LoadTourLogFormCommand): return new LoadTourLogFormCommand(viewModel);
                case nameof(LoadLogCommand): return new LoadLogCommand(viewModel);
                case nameof(CreateTourLogCommand): return new CreateTourLogCommand(viewModel);
                    /*case nameof(UpdateTourCommand): return new UpdateTourCommand(viewModel);
                    case nameof(DeleteTourCommand): return new DeleteTourCommand(viewModel);
                    case nameof(ImportTourCommand): return new ImportTourCommand(viewModel);
                    case nameof(ExportTourCommand): return new ExportTourCommand(viewModel);
                    case nameof(CopyTourCommand): return new CopyTourCommand(viewModel); */
            }

            throw new Exception($"command type [{typeof(CommandType).Name}] unknown");
        }
    }
}