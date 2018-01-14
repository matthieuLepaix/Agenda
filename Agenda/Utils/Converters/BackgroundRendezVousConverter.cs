using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;

namespace Agenda.Utils
{
    public class BackgroundRendezVousConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var isSelected = value as bool?;
            return isSelected != null && (bool)isSelected ?
                (SolidColorBrush)(new BrushConverter().ConvertFrom("#002860"))
                : (SolidColorBrush)(new BrushConverter().ConvertFrom("#0048ac"));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
