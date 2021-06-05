using System.Threading;
using ViewModels.Enums;

namespace ViewModels.ViewModels
{
    public interface IStatusDisplay
    {
        public void Clear() // default interface implementation. yes this a thing in c#
        {
            Thread.Sleep(2);
            StatusMessage = string.Empty;
            Status = Status.Empty;
        }
        public Status Status { get; set; }
        public string StatusMessage { get; set; }
        public string Operation { get; }
    }
}
