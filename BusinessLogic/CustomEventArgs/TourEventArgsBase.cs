using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.CustomEventArgs
{
    public class TourEventArgsBase : EventArgs
    {
        public Tour Tour { get; protected set; }
        public TourEventArgsBase(Tour tour) => Tour = tour; 
    }
}
