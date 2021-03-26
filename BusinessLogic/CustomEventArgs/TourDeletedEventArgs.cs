using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.CustomEventArgs
{
    public class TourDeletedEventArgs : TourEventArgsBase
    {
        public TourDeletedEventArgs(Tour tour) : base(tour) { }
    }
}
