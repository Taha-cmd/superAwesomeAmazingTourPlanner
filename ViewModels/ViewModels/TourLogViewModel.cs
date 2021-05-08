using System;
using System.Collections.Generic;
using System.Text;
using Models;

namespace ViewModels.ViewModels
{
    public class TourLogViewModel : ViewModelBase
    {
        TourLog log { get; }
        public TourLogViewModel(TourLog log) : base("TourLog", $"tour log for {log.TourName}")
        {
            this.log = log;
        }

        public string TourName => log.TourName;
        public DateTime DateTime
        {
            get => log.DateTime;
            set => SetValue(log, value, nameof(DateTime));
        }
        public string Report
        {
            get => log.Report;
            set => SetValue(log, value, nameof(Report));
        }
        public double Distance
        {
            get => log.Distance;
            set => SetValue(log, value, nameof(Distance));
        }
        public double TotalTime
        {
            get => log.TotalTime;
            set => SetValue(log, value, nameof(TotalTime));
        }
        public int Rating
        {
            get => log.Rating;
            set => SetValue(log, value, nameof(Rating));
        }
        public bool HasMcDonalds
        {
            get => log.HasMcDonalds;
            set => SetValue(log, value, nameof(HasMcDonalds));
        }
        public bool HasCampingSpots
        {
            get => log.HasCampingSpots;
            set => SetValue(log, value, nameof(HasCampingSpots));
        }
        public string Author
        {
            get => log.Author;
            set => SetValue(log, value, nameof(Author));
        }
        public string Accomodation
        {
            get => log.Accomodation;
            set => SetValue(log, value, nameof(Accomodation));
        }
        public int Members
        {
            get => log.Members;
            set => SetValue(log, value, nameof(Members));
        }
    }
}
