using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ViewModels.Enums;
using ViewModels.ViewModels;

namespace ViewModels.Commands
{
    public class AsyncOperationWithStatusCommandBase : CommandBase
    {
        protected async Task AsyncOperationWrapper(IStatusDisplay form, Func<Task> action)
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
                Debug.WriteLine(ex);
                form.StatusMessage = ex.Message;
                form.Status = Status.Failure;
                Logger.Error(ex.Message);
            }
        }
    }
}
