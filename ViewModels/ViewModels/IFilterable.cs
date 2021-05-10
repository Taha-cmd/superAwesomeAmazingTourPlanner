using System.Windows.Input;

namespace ViewModels.ViewModels
{
    interface IFilterable
    {
        void Filter(string filterText);
        ICommand SearchCommand { get; }
    }
}
