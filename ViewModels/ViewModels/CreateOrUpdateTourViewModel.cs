using Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using ViewModels.Commands;
using ViewModels.Enums;

namespace ViewModels.ViewModels
{
    public class CreateOrUpdateTourViewModel : ViewModelBase, IForm
    {
        public CreateOrUpdateTourViewModel() : base("CreateTour", "Create New Tour")
        {
            CreateOrUpdateTourCommand = CommandFactory.CreateCommand<CreateTourCommand>(this);
            Operation = Title;
        } 

        public CreateOrUpdateTourViewModel(Tour tour) : base("CreateTour", $"Update Tour {tour.Name}")
        {
            CreateOrUpdateTourCommand = CommandFactory.CreateCommand<UpdateTourCommand>(this);

            name = tour.Name;
            StartingArea = tour.StartingArea;
            TargetArea = tour.TargetArea;
            Description = tour.Description;
            Distnace = tour.Distance;

            Operation = Title;
            oldName = Name;
            oldImage = tour.Image;

        }
        public ICommand CreateOrUpdateTourCommand { get; protected set; }


        #region fields

        private string name;
        private string startingArea;
        private string targetArea;
        private string description;
        private double distnace;

        private Status status = Status.Empty;
        private string statusMessage = string.Empty;
        private string oldName = string.Empty;
        private string oldImage = string.Empty;

        #endregion fields

        #region properties
        public string Name 
        { 
            get => name;
            set => SetValue(ref name, value, nameof(Name));
        }
        public string StartingArea 
        { 
            get => startingArea;
            set => SetValue(ref startingArea, value, nameof(StartingArea));
        }
        public string TargetArea
        { 
            get => targetArea;
            set => SetValue(ref targetArea, value, nameof(TargetArea));
        }
        public string Description 
        { 
            get => description;
            set => SetValue(ref description, value, nameof(Description));
        }
        public double Distnace 
        { 
            get => distnace;
            set => SetValue(ref distnace, value, nameof(Distnace));
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

        public Tour Tour
        {
            get
            {
                return new Tour()
                {
                    Description = this.Description,
                    Name = this.Name,
                    StartingArea = this.StartingArea,
                    TargetArea = this.TargetArea
                };
            }
        }

        public string OldName { get => oldName; }
        public string OldImage { get => oldImage; }
        public string Operation { get; }
        #endregion

        public void Clear()
        {
            Name = string.Empty;
            StartingArea = string.Empty;
            TargetArea = string.Empty;
            Description = string.Empty;
            distnace = 0;
        }

        public override void Reset()
        {
            Clear();
            StatusMessage = string.Empty;
            Status = Status.Empty;
        }
    }
}
