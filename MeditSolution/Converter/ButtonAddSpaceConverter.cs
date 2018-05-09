using System;
using System.Globalization;
using Xamarin.Forms;

namespace MeditSolution.Converter
{
    public class ButtonAddSpaceConverter : IValueConverter
    {
        
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "     " + ((string)value)+"     ";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((string)value).Trim();
        }
    }
}
