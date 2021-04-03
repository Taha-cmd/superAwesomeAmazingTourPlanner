using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using ViewModels.Commands;

namespace ViewModels.ViewModels
{
    public class CreateTourLogViewModel : ViewModelBase
    {
        public CreateTourLogViewModel(string tourName)
        {
            ViewName = "CreateTourLog";
            Title = "Create Tour Log for ";
            this.tourName = tourName;
            createLogCommand = CommandFactory.CreateCommand<CreateTourLogCommand>(this);
        }
        private readonly string tourName;

        private DateTime dateTime = DateTime.Now;
        private string report;
        private float distance;
        private float totalTime;
        private string rating;
        private readonly ICommand createLogCommand;

        public ICommand CreateTourLogCommand { get => createLogCommand; }

        public string TourName
        {
            get => tourName;
        }
        public DateTime DateTime 
        { 
            get => dateTime;
            set { dateTime = value; TriggerPropertyChangedEvent(nameof(DateTime)); } 
        }
        public string Report 
        { 
            get => report;
            set { report = value; TriggerPropertyChangedEvent(nameof(Report)); } 
        }
        public float Distance 
        { 
            get => distance; 
            set { distance = value; TriggerPropertyChangedEvent(nameof(Distance)); }
        }
        public float TotalTime 
        { 
            get => totalTime; 
            set { totalTime = value; TriggerPropertyChangedEvent(nameof(Distance)); } 
        }
        public string Rating 
        { 
            get => rating; 
            set { rating = value; TriggerPropertyChangedEvent(nameof(Rating)); }
        }
    }
}
