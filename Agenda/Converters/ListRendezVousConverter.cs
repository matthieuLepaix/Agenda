using Agenda.ViewModels;
using Agenda.UserControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace Agenda.Utils
{
    public class ListRendezVousConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var items = value as ObservableCollection<UserControlRDV>;
            if (parameter != null && items != null)
            {

                var strParameter = parameter.ToString();
                var day = strParameter[0];
                var hour = int.Parse(strParameter.Substring(1));
                var dayToAdd = 0;
                switch (day)
                {
                    case 'M':
                        // Mardi = Lundi + 1
                        dayToAdd = 1;
                        break;
                    case 'm':
                        // Mardi = Lundi + 2
                        dayToAdd = 2;
                        break;
                    case 'J':
                        // Mardi = Lundi + 3
                        dayToAdd = 3;
                        break;
                    case 'V':
                        // Mardi = Lundi + 4
                        dayToAdd = 4;
                        break;
                    default:
                        // Lundi
                        dayToAdd = 0;
                        break;
                }
                return items.Where(r =>
                ((RendezVousViewModel)r.DataContext).RendezVous.Date.Day == AgendaViewModel.SelectedDateForConverter.AddDays(dayToAdd).Day
                && ((RendezVousViewModel)r.DataContext).RendezVous.Date.Hour == hour).ToList();
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
