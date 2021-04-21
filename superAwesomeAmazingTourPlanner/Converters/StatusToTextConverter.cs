using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using ViewModels.Enums;

namespace superAwesomeAmazingTourPlanner.Converters
{
    class StatusToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((Status)value)
            {
                case Status.Success: return "Operation was Successfull!";
                case Status.Failure: return "Operation has failed!";
                case Status.Pending: return "Request successfully sent, this might take a minute!";
                case Status.Empty: return "";
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
