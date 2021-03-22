﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Models;
using ViewModels.Commands;

namespace ViewModels.ViewModels
{
    public class TourViewModel : ViewModelBase
    {
        private readonly Tour tour;

        #region Properties
        public string Name
        {
            get => tour.Name;
            set
            {
                tour.Name = value;
                TriggerPropertyChangedEvent(nameof(Name));
            }
        }


        public string Description
        {
            get => tour.Description;
            set
            {
                tour.Description = value;
                TriggerPropertyChangedEvent(nameof(Name));
            }
        }

        public float Distance
        {
            get => tour.Distance;
            set
            {
                tour.Distance = value;
                TriggerPropertyChangedEvent(nameof(Distance));
            }
        }

        #endregion

        public TourViewModel(Tour tour)
        {
            this.tour = tour;
            ViewName = "Tour";
        }

        public TourViewModel()
        {
            ViewName = "Tour";
        }
    }
}
