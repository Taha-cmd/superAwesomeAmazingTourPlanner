using System;
using System.Collections.Generic;
using System.Text;
using ViewModels.ViewModels;
using System.Linq;
using System.Threading.Tasks;
using ViewModels.Enums;
using System.Threading;
using BusinessLogic;
using log4net;

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

        //default behavior for canexecute
        virtual public bool CanExecute(object parameter) => true;
        protected ILog Logger { get; } = Application.GetLogger();
        protected ToursManager manager = Application.GetToursManager();

        public event EventHandler CanExecuteChanged;
    }
}
