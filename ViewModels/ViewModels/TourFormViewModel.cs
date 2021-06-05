using Models;
using System.Windows.Input;
using ViewModels.Commands;
using ViewModels.Enums;

namespace ViewModels.ViewModels
{
    public class TourFormViewModel : ViewModelBase, IStatusDisplay
    {
        public TourFormViewModel() : base("CreateTour", "Create New Tour")
        {
            OperationCommand = CommandFactory.CreateCommand<CreateTourCommand>(this);
            ImportTourCommand = CommandFactory.CreateCommand<ImportTourCommand>(this);
            Operation = Title;
            tour = new Tour();
        } 

        public TourFormViewModel(Tour tour) : base("CreateTour", $"Update Tour {tour.Name}")
        {
            OperationCommand = CommandFactory.CreateCommand<UpdateTourCommand>(this);
            ImportTourCommand = CommandFactory.CreateCommand<ImportTourCommand>(this);
            this.tour = tour;

            Operation = Title;
            oldName = Name;
            oldImage = tour.Image;

        }
        public ICommand OperationCommand { get; protected set; }
        public ICommand ImportTourCommand { get; }


        #region fields
        private Tour tour;

        private Status status = Status.Empty;
        private string statusMessage = string.Empty;
        private string oldName = string.Empty;
        private string oldImage = string.Empty;

        #endregion fields

        #region properties
        public string Name 
        { 
            get => tour.Name;
            set => SetValue(tour, value, nameof(Name));
        }
        public string StartingArea 
        { 
            get => tour.StartingArea;
            set => SetValue(tour, value, nameof(StartingArea));
        }
        public string TargetArea
        { 
            get => tour.TargetArea;
            set => SetValue(tour, value, nameof(TargetArea));
        }
        public string Description 
        { 
            get => tour.Description;
            set => SetValue(tour, value, nameof(Description));
        }
        public double Distnace 
        { 
            get => tour.Distance;
            set => SetValue(tour, value, nameof(Distnace));
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

        public Tour Tour => tour;

        public string OldName => oldName;
        public string OldImage => oldImage;
        public string Operation { get; }
        #endregion

        public void Clear()
        {
            Name = string.Empty;
            StartingArea = string.Empty;
            TargetArea = string.Empty;
            Description = string.Empty;
            
        }
        public override void Reset()
        {
            Clear();
            StatusMessage = string.Empty;
            Status = Status.Empty;
        }
    }
}
