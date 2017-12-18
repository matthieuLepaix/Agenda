using System;

namespace Agenda.Utils
{
    public class DateTimeHelper : IComparable
    {

        private DateTime dateTime;

        public int Day
        {
            get
            {
                return dateTime.Day;
            }
        }

        public int Month
        {
            get
            {
                return dateTime.Month;
            }
        }

        public DateTimeHelper(DateTime dt)
        {
            dateTime = dt;
        }

        public override int GetHashCode()
        {
            return dateTime.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("{0:00}/{1:00}/{2}", dateTime.Day, dateTime.Month, dateTime.Year);
        }

        public override bool Equals(object obj)
        {
            return obj is DateTimeHelper ? Equals(obj as DateTimeHelper) : false;
        }

        public bool Equals(DateTimeHelper obj)
        {
            return obj.dateTime.Equals(this.dateTime);
        }

        public int CompareTo(object obj)
        {
            return obj is DateTimeHelper ? CompareTo(obj as DateTimeHelper) : -1;
        }

        public int CompareTo(DateTimeHelper obj)
        {
            return obj.dateTime.CompareTo(this.dateTime);
        }
    }
}
