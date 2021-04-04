using System;
using System.Collections.Generic;
using System.Text;
using ViewModels.ViewModels;
using System.Linq;

namespace ViewModels.Commands
{
    public class CommandBase
    {
        protected void RegisterSubscriptionToPropertyChanged(ViewModelBase viewModel, string propertyName)
        {
            viewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == propertyName)
                {
                    CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                }
            };
        }

        protected void RegisterAllProperties(ViewModelBase viewModel)
        {
            viewModel
                .GetType()
                .GetProperties()
                .ToList()
                .ForEach(prop => RegisterSubscriptionToPropertyChanged(viewModel, prop.Name));
        }

        public event EventHandler CanExecuteChanged;
    }
}
