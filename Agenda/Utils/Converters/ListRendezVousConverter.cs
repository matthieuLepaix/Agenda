using Agenda.ViewModels;
using Agenda.UserControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;
using AgendaCore;
using System.Windows.Media;

namespace Agenda.Utils
{
    public class ListRendezVousConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var items = value as ObservableCollection<UserControlRendezVous>;
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
                ((UserControlRendezVousViewModel)r.DataContext).RendezVous.Date.Day == AgendaViewModel.SelectedDateForConverter.AddDays(dayToAdd).Day
                && ((UserControlRendezVousViewModel)r.DataContext).RendezVous.Date.Hour == hour).ToList();
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


    public class DayOffToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var items = value as ObservableCollection<JoursFeries>;
            if (parameter != null)
            {
                var strParameter = parameter.ToString();
                var day = strParameter[0];
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
                return !items.Any(r => r.Jour.ToShortDateString() == AgendaViewModel.SelectedDateForConverter.
                                                                        AddDays(dayToAdd).ToShortDateString());
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }


    public class DayOffToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var items = value as ObservableCollection<JoursFeries>;
            if (parameter != null)
            {
                var strParameter = parameter.ToString();
                var day = strParameter[0];
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
                return items.Any(r => r.Jour.ToShortDateString() == AgendaViewModel.SelectedDateForConverter.
                                                                        AddDays(dayToAdd).ToShortDateString()) 
                                                                        ? new SolidColorBrush(Color.FromRgb(255,73,61)) : Brushes.Transparent;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}
