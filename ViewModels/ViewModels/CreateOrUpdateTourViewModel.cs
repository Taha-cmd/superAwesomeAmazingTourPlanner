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

            Name = tour.Name;
            StartingArea = tour.StartingArea;
            TargetArea = tour.TargetArea;
            Description = tour.Description;
            Distnace = tour.Distance;

            Operation = Title;
            oldName = Name;
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

        #endregion fields

        #region properties
        public string Name 
        { 
            get => name; 
            set 
            { 
                name = value; 
                TriggerPropertyChangedEvent(nameof(Name)); 
            } 
        }
        public string StartingArea 
        { 
            get => startingArea;
            set
            {
                startingArea = value;
                TriggerPropertyChangedEvent(nameof(StartingArea));
            }
        }
        public string TargetArea
        { 
            get => targetArea;
            set
            {
                targetArea = value;
                TriggerPropertyChangedEvent(nameof(TargetArea));
            }
        }
        public string Description 
        { 
            get => description;
            set
            {
                description = value;
                TriggerPropertyChangedEvent(nameof(Description));
            }
        }
        public double Distnace 
        { 
            get => distnace;
            set
            {
                distnace = value;
                TriggerPropertyChangedEvent(nameof(Distnace));
            }
        }

        public Status Status
        {
            get => status;
            set
            {
                status = value;
                TriggerPropertyChangedEvent(nameof(Status));
            }
        }

        public string StatusMessage
        {
            get => statusMessage;
            set { statusMessage = value; TriggerPropertyChangedEvent(nameof(StatusMessage)); }
        }
        public string OldName { get => oldName; }

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

        public string Operation { get; }
        #endregion

        public void Clear()
        {
            Name = string.Empty;
            StartingArea = string.Empty;
            TargetArea = string.Empty;
            Description = string.Empty;
            distnace = 0;
            StatusMessage = string.Empty;
            //Status = Status.Empty;
        }
    }
}
