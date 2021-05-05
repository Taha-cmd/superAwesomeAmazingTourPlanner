using Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using ViewModels.Commands;
using ViewModels.Enums;

namespace ViewModels.ViewModels
{
    public class CreateTourLogViewModel : ViewModelBase, IStatusDisplay
    {
        private TourLog log = new TourLog();
        public TourLog Log => log;
        public CreateTourLogViewModel(string tourName) : base("CreateTourLog", $"Create Tour Log for {tourName}")
        {
            log.TourName = tourName;
            createLogCommand = CommandFactory.CreateCommand<CreateTourLogCommand>(this);
            Operation = Title;
        }

        #region private fields

        private string statusMessage = string.Empty;
        private Status status = Status.Empty;
        private readonly ICommand createLogCommand;

        #endregion

        #region properties
        public ICommand CreateTourLogCommand { get => createLogCommand; }

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
            set
            {
                SetValue(log, value, nameof(HasMcDonalds));
                Debug.WriteLine(HasMcDonalds);
            }
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


        // options for the comboboxes

        public ObservableCollection<bool> HasMcDonaldsOptions { get; } = new ObservableCollection<bool>() { true, false };
        public ObservableCollection<bool> HasCampingSpotsOptions { get; } = new ObservableCollection<bool>() { true, false };
        public ObservableCollection<string> AccomodationOptions { get; } = new ObservableCollection<string>() { "hotel", "camping", "appartment" };
        public ObservableCollection<int> RatingOptions { get; } = new ObservableCollection<int>() { 0,1,2,3,4,5,6,7,8,9,10 };


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
