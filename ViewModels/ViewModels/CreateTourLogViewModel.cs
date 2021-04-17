using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using ViewModels.Commands;
using ViewModels.Enums;

namespace ViewModels.ViewModels
{
    public class CreateTourLogViewModel : ViewModelBase, IForm
    {
        public CreateTourLogViewModel(string tourName) : base("CreateTourLog", $"Create Tour Log for {tourName}")
        {
            this.tourName = tourName;
            createLogCommand = CommandFactory.CreateCommand<CreateTourLogCommand>(this);
            Operation = Title;
        }

        #region private fields

        private readonly string tourName;
        private string statusMessage = string.Empty;
        private Status status = Status.Empty;
        private DateTime dateTime = DateTime.Now;
        private string report;
        private float distance;
        private float totalTime;
        private int rating;
        private readonly ICommand createLogCommand;

        #endregion

        #region properties
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

        public void Clear()
        {
            Report = string.Empty;
            TotalTime = 0;
            Rating = 0;
            DateTime = DateTime.Now;
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
        public int Rating 
        { 
            get => rating; 
            set { rating = value; TriggerPropertyChangedEvent(nameof(Rating)); }
        }

        public Status Status
        {
            get => status;
            set { status = value; TriggerPropertyChangedEvent(nameof(Status)); }
        }
        public string StatusMessage 
        {
            get => statusMessage;
            set { statusMessage = value; TriggerPropertyChangedEvent(nameof(StatusMessage)); }
        }

        public string Operation { get; }

        public TourLog Log
        {
            get
            {
                return new TourLog()
                {
                     DateTime = this.DateTime,
                     Distance = this.Distance,
                     Rating = this.Rating,
                     Report = this.Report,
                     TotalTime = this.TotalTime
                };
            }
        }


        #endregion
    }
}
