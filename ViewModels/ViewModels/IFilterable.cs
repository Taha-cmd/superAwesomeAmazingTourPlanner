using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ViewModels.ViewModels
{
    interface IFilterable
    {
        void Filter(string filterText);
        ICommand SearchCommand { get; }
    }
}
