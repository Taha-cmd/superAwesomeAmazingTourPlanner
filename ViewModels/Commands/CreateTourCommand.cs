using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Extensions;

namespace ViewModels.Commands
{
    class CreateTourCommand : CommandBase, ICommand
    {
        private TourViewModel TourViewModel { get; set; }
        public CreateTourCommand(TourViewModel tourViewModel)
        {
            TourViewModel = tourViewModel;
            RegisterSubscriptionToPropertyChanged(TourViewModel, nameof(TourViewModel.Name));
        }

        public bool CanExecute(object parameter)
        {
            string name = (string)parameter;
            return !name.IsEmptyOrWhiteSpace() && name.Length >= 2;
        }
        public void Execute(object parameter)
        {
            TourViewModel.Name = ((string)parameter).Reverse();
        }
    }
}
