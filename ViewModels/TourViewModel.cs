using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Models;
using ViewModels.Commands;

namespace ViewModels
{
    public class TourViewModel : ViewModelBase
    {
        private Tour tour = new Tour(); 
        public string Name
        {
            get => tour.Name;
            set
            {
                tour.Name = value;
                TriggerPropertyChangedEvent(nameof(Name));
            }
        }

        public ICommand CreateTourCommand { get; set; }

        public TourViewModel()
        {
            CreateTourCommand = new CreateTourCommand(this);
        }
    }
}
