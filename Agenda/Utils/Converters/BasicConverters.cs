using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Agenda.Utils
{
    /// <summary>
    /// Value converter that translates list filling status to false when empty and true when is filled.
    /// </summary>
    public sealed class EmptyBooleanConverter : IValueConverter
    {
        /// Convert
        public object Convert(object value, Type targetType, object parameter, CultureInfo language)
        {
            var result = false;
            if (value is ObservableCollection<string>)
            {
                var list = value as ObservableCollection<string>;
                result = (list.Count > 0)? true : false;
            }
            return result;
        }

        /// Convert back
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo language)
        {
            throw new NotImplementedException();
        }
    }
    /// <summary>
    /// Value converter that translates true to false and false to true.
    /// </summary>
    public sealed class ReverseBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value is bool && (bool)value) ? false : true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Value converter that translates true to <see cref="Visibility.Visible"/> and false to
    /// <see cref="Visibility.Collapsed"/>.
    /// </summary>
    public sealed class BooleanToVisibilityConverter : IValueConverter
    {
        /// Convert
        public object Convert(object value, Type targetType, object parameter, CultureInfo language)
        {
            var visible = (value is bool && (bool)value) ? Visibility.Visible : Visibility.Collapsed;
            return visible;
        }

        /// Convert back
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo language)
        {
            return value is Visibility && (Visibility)value == Visibility.Visible;
        }
    }

    /// <summary>
    /// Value converter that translates true to <see cref="Visibility.Collapsed"/> and false to
    /// <see cref="Visibility.Visible"/>.
    /// </summary>
    public sealed class ReverseBooleanToVisibilityConverter : IValueConverter
    {
        /// Convert
        public object Convert(object value, Type targetType, object parameter, CultureInfo language)
        {
            var visible = (value is bool && (bool)value) ? Visibility.Collapsed : Visibility.Visible;
            return visible;
        }

        /// Convert back
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo language)
        {
            return value is Visibility && (Visibility)value == Visibility.Collapsed;
        }
    }

    /// <summary>
    /// Value converter that translates true/null to <see cref="Visibility.Collapsed"/> and false to
    /// <see cref="Visibility.Visible"/>.
    /// </summary>
    public sealed class ReverseBooleanNullableToVisibilityConverter : IValueConverter
    {
        /// Convert
        public object Convert(object value, Type targetType, object parameter, CultureInfo language)
        {
            bool? resValue = (bool?)value;

            if (resValue == null)
            {
                return Visibility.Collapsed;
            }
            else if (!(bool)resValue)
            {
                return Visibility.Visible;
            }
            else
            {
                return Visibility.Collapsed;
            }
        }

        /// Convert back
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo language)
        {
            throw new NotImplementedException();
        }
    }
    
    
    /// <summary>
    /// Simple date to string converter
    /// </summary>
    public sealed class DateToStringConverter : IValueConverter
    {
        /// Convert
        public object Convert(object value, Type targetType, object parameter, CultureInfo language)
        {
            DateTime date = (DateTime)value;
            var res = "-";
            if (date != new DateTime())
            {
                res = String.Format("{0:d}", date);
            }
            return res;
        }

        /// Convert back
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo language)
        {
            throw new NotImplementedException();
        }
    }
    /// <summary>
    /// Simple hour to string converter
    /// </summary>
    public sealed class HourToStringConverter : IValueConverter
    {
        /// Convert
        public object Convert(object value, Type targetType, object parameter, CultureInfo language)
        {
            TimeSpan hour = (TimeSpan)value;
            return hour.ToString(@"hh\:mm");
        }

        /// Convert back
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo language)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Simple hour to string converter
    /// </summary>
    public sealed class TimeToStringConverter : IValueConverter
    {
        /// Convert
        public object Convert(object value, Type targetType, object parameter, CultureInfo language)
        {
            DateTime date = (DateTime)value;
            return String.Format("{0:g}", date);
        }

        /// Convert back
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo language)
        {
            throw new NotImplementedException();
        }
    }
    
    
}
