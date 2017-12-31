using Agenda.UserControls;
using Agenda.ViewModels;
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
    public class TravauxToUserControlConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var travaux = (List<ReparationRDV>)value;
            var ucs = new ObservableCollection<UserControlWork>();
            travaux.ToList().ForEach(t => ucs.Add(new UserControlWork(null, t)));
            return ucs;
        }
        

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
