using Models;
using System;

namespace ViewModels.ViewModels
{
    public class TourLogViewModel : ViewModelBase
    {
        public TourLog Log { get; }
        public TourLogViewModel(TourLog log) : base("TourLog", $"tour log for {log.TourName}")
        {
            this.Log = log;
        }

        public string TourName => Log.TourName;
        public DateTime DateTime
        {
            get => Log.DateTime;
            set => SetValue(Log, value, nameof(DateTime));
        }
        public string Report
        {
            get => Log.Report;
            set => SetValue(Log, value, nameof(Report));
        }
        public double Distance
        {
            get => Log.Distance;
            set => SetValue(Log, value, nameof(Distance));
        }
        public double TotalTime
        {
            get => Log.TotalTime;
            set => SetValue(Log, value, nameof(TotalTime));
        }
        public int Rating
        {
            get => Log.Rating;
            set => SetValue(Log, value, nameof(Rating));
        }
        public bool HasMcDonalds
        {
            get => Log.HasMcDonalds;
            set => SetValue(Log, value, nameof(HasMcDonalds));
        }
        public bool HasCampingSpots
        {
            get => Log.HasCampingSpots;
            set => SetValue(Log, value, nameof(HasCampingSpots));
        }
        public string Author
        {
            get => Log.Author;
            set => SetValue(Log, value, nameof(Author));
        }
        public string Accomodation
        {
            get => Log.Accomodation;
            set => SetValue(Log, value, nameof(Accomodation));
        }
        public int Members
        {
            get => Log.Members;
            set => SetValue(Log, value, nameof(Members));
        }
    }
}
