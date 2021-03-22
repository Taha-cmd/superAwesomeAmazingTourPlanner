using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.CustomEventArgs
{
    public class TourUpdatedEventArgs : TourEventArgsBase
    {
        public string OldName { get; }
        public TourUpdatedEventArgs(string oldName, Tour tour) : base(tour) => OldName = oldName;
 
    }
}
