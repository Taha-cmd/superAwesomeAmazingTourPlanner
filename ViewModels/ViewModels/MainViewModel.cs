using BusinessLogic;
using System.Collections.Generic;
using System.Windows.Input;
using ViewModels.Commands;

namespace ViewModels.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        // https://rachel53461.wordpress.com/2011/12/18/navigation-with-mvvm-2/

        public MainViewModel() : base("MainWindow", "Main View Model", Application.GetLogger())
        {

            ChangePageCommand           = CommandFactory.CreateCommand<ChangePageCommand>(this);
            LoadTourCommand             = CommandFactory.CreateCommand<LoadTourCommand>(this);
            LoadTourLogFormCommand      = CommandFactory.CreateCommand<LoadTourLogFormCommand>(this);
            LoadLogCommand              = CommandFactory.CreateCommand<LoadLogCommand>(this);
            DeleteTourCommand           = CommandFactory.CreateCommand<DeleteTourCommand>(this);
            LoadUpdateTourFormCommand   = CommandFactory.CreateCommand<LoadUpdateTourFormCommand>(this);
            ExportTourCommand           = CommandFactory.CreateCommand<ExportTourCommand>(this);
            CopyTourCommand             = CommandFactory.CreateCommand<CopyTourCommand>(this);
            GeneratePdfReportCommand    = CommandFactory.CreateCommand<GeneratePdfReportCommand>(this);
            DeleteTourLogCommand        = CommandFactory.CreateCommand<DeleteTourLogCommand>(this);



            ViewModels = new List<ViewModelBase>()
            {
                new HomeViewModel(),
                new ToursViewModel(),
                new TourFormViewModel()
            };

            CurrentViewModel = ViewModels[0];
        }

        #region fields
        private ViewModelBase currentViewModel;
        #endregion

        #region properties
        public ICommand ChangePageCommand { get; }
        public ICommand LoadTourCommand { get; }
        public ICommand LoadTourLogFormCommand { get; }
        public ICommand LoadLogCommand { get; }
        public ICommand DeleteTourCommand { get; }
        public ICommand LoadUpdateTourFormCommand { get; }
        public ICommand ExportTourCommand { get; }
        public ICommand CopyTourCommand { get; }
        public ICommand GeneratePdfReportCommand { get; }
        public ICommand DeleteTourLogCommand { get; }
        public List<ViewModelBase> ViewModels { get; }

        public ViewModelBase CurrentViewModel
        {
            get => currentViewModel;
            set => SetValue(ref currentViewModel, value, nameof(CurrentViewModel));
        }
        #endregion
    }
}
