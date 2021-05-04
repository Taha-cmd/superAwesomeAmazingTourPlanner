using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using ViewModels;

namespace ViewModels.Commands
{
    class CommandFactory
    {
        public static ICommand CreateCommand<TCommand>(object viewModel) where TCommand : ICommand
        {
            return typeof(TCommand).Name switch
            {
                nameof(CreateTourCommand)           => new CreateTourCommand(viewModel),
                nameof(ChangePageCommand)           => new ChangePageCommand(viewModel),
                nameof(SearchCommand)               => new SearchCommand(viewModel),
                nameof(LoadTourCommand)             => new LoadTourCommand(viewModel),
                nameof(LoadTourLogFormCommand)      => new LoadTourLogFormCommand(viewModel),
                nameof(LoadLogCommand)              => new LoadLogCommand(viewModel),
                nameof(CreateTourLogCommand)        => new CreateTourLogCommand(viewModel),
                nameof(DeleteTourCommand)           => new DeleteTourCommand(viewModel),
                nameof(LoadUpdateTourFormCommand)   => new LoadUpdateTourFormCommand(viewModel),
                nameof(UpdateTourCommand)           => new UpdateTourCommand(viewModel),
                nameof(ExportTourCommand)           => new ExportTourCommand(viewModel),
                nameof(ImportTourCommand)           => new ImportTourCommand(viewModel),
                nameof(CopyTourCommand)             => new CopyTourCommand(viewModel),
                _ => throw new Exception($"command type [{typeof(TCommand).Name}] unknown"),
            };
        }
    }
}