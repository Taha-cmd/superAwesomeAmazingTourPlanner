using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using ViewModels.Commands;
using ViewModels.Enums;

namespace ViewModels.ViewModels
{
    public class CreateTourLogViewModel : ViewModelBase, IStatusDisplay
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
            set => SetValue(ref dateTime, value, nameof(DateTime));
        }
        public string Report 
        { 
            get => report;
            set => SetValue(ref report, value, nameof(Report));
        }
        public float Distance 
        { 
            get => distance;
            set => SetValue(ref distance, value, nameof(Distance));
        }
        public float TotalTime 
        { 
            get => totalTime;
            set => SetValue(ref totalTime, value, nameof(TotalTime));
        }
        public int Rating 
        { 
            get => rating;
            set => SetValue(ref rating, value, nameof(Rating));
        }

        public Status Status
        {
            get => status;
            set => SetValue(ref status, value, nameof(Status));
        }
        public string StatusMessage 
        {
            get => statusMessage;
            set => SetValue(ref statusMessage, value, nameof(StatusMessage));
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
        public void Clear()
        {
            Report = string.Empty;
            TotalTime = 0;
            Rating = 0;
            DateTime = DateTime.Now;
        }

        public override void Reset()
        {
            Clear();
            StatusMessage = string.Empty;
            Status = Status.Empty;
        }


        #endregion
    }
}
