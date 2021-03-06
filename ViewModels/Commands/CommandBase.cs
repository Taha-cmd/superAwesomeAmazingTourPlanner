using System;
using System.Collections.Generic;
using System.Text;

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

        public event EventHandler CanExecuteChanged;
    }
}
