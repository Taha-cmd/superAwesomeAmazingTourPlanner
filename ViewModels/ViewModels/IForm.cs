using System;
using System.Collections.Generic;
using System.Text;
using ViewModels.Enums;

namespace ViewModels.ViewModels
{
    public interface IForm
    {
        public void Clear();
        public Status Status { get; set; }
        public string StatusMessage { get; set; }
        public string Operation { get; }
    }
}
