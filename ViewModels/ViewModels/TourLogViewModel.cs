using System;
using System.Collections.Generic;
using System.Text;
using Models;

namespace ViewModels.ViewModels
{
    public class TourLogViewModel : ViewModelBase
    {
        TourLog TourLog { get; }
        public TourLogViewModel(TourLog log) : base("TourLog", "what eva")
        {
            TourLog = log;
        }  
    }
}
