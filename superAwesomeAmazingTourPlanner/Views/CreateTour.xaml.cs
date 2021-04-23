using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ViewModels.ViewModels;

namespace superAwesomeAmazingTourPlanner.Views
{
    /// <summary>
    /// Interaction logic for CreateTour.xaml
    /// </summary>
    public partial class CreateTour : UserControl
    {
        public CreateTour()
        {
            InitializeComponent();
        }

        public void ShowImportWindow(object sender, EventArgs args)
        {
            var importWindow = new Microsoft.Win32.OpenFileDialog();

            bool? result = importWindow.ShowDialog();

            if ((bool)result)
            {
                string path = importWindow.FileName;
                ((CreateOrUpdateTourViewModel)DataContext).ImportTourCommand.Execute(path);
            }
        }


        public void OnTextChanged(object sender, EventArgs args)
        {

        }

    }
}
