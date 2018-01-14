using AgendaCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace Agenda.Utils
{
    public class ClientsFilterConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var filter = parameter != null ? ((string)parameter).ToUpper() : string.Empty;
            return string.IsNullOrEmpty(filter) || string.IsNullOrWhiteSpace(filter) ?
                value
                : ((ObservableCollection<Client>)value).Where(c => c.Nom.ToUpper().StartsWith(filter)).ToList();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
