using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.CustomEventArgs
{
    public class TourAddedEventArgs : TourEventArgsBase
    {
        public TourAddedEventArgs(Tour tour) : base(tour) { }
    }
}
