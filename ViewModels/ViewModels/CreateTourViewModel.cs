using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using ViewModels.Commands;
using ViewModels.Enums;

namespace ViewModels.ViewModels
{
    public class CreateTourViewModel : ViewModelBase
    {
        public CreateTourViewModel()
        {
            ViewName = "CreateTour";
            Title = "Create New Tour";
            CreateTourCommand = CommandFactory.CreateCommand<CreateTourCommand>(this);
        }

        public ICommand CreateTourCommand { get; protected set; }


        #region fields and their properties

        private string name;
        private string startingArea;
        private string targetArea;
        private string description;
        private float distnace;

        private Status status = Status.Empty;

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
        public float Distnace 
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

        #endregion

        internal void ClearProperties()
        {
            Name = string.Empty;
            StartingArea = string.Empty;
            TargetArea = string.Empty;
            Description = string.Empty;
            distnace = 0;
            //Status = Status.Empty;
        }
    }
}
