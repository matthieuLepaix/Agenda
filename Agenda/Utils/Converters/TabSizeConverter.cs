using System;
using System.Windows.Data;
using System.Windows.Controls;

namespace Agenda.Utils
{
    public class TabSizeConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            TabControl tabControl = values[0] as TabControl;
            double width = (tabControl.ActualWidth - 1.5)/ tabControl.Items.Count;
            //Subtract 1, otherwise we could overflow to two rows.
            return (width <= 1) ? 0 : (width-1);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter,
            System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
