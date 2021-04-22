using System;
using System.Collections.Generic;
using System.Text;
using ViewModels.ViewModels;
using System.Linq;
using System.Threading.Tasks;
using ViewModels.Enums;

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

        public event EventHandler CanExecuteChanged;

        protected async Task CreateOrUpdate(IForm form, Func<Task> action)
        {
            try
            {
                form.StatusMessage = string.Empty;
                form.Status = Status.Pending;
                await action();
                form.Status = Status.Success;
                // after a successfull creation, clear the input fields
                form.Clear();
            }
            catch (Exception ex)
            {
                form.StatusMessage = ex.Message;
                form.Status = Status.Failure;
            }

        }
    }
}
