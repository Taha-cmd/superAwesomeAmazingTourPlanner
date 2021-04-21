using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace superAwesomeAmazingTourPlanner.Converters
{
    public class PathToBitMapImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var image = new BitmapImage();


            // need to read this file this way
            // otherwise the image will be used by the process and prevent other processes from deleting it
            // https://stackoverflow.com/questions/13262548/delete-a-file-being-used-by-another-process
            using (var stream = File.OpenRead(Path.GetFullPath((string)value)))
            {
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = stream;
                image.EndInit();
            }

            return image;
        }   

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
