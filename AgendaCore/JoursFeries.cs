using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgendaCore
{
    public class JoursFeries
    {
        #region Attributes
        private int id;

        private DateTime jour;
        #endregion
        #region Properties
        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public DateTime Jour
        {
            get
            {
                return jour;
            }
            set
            {
                jour = value;
            }
        }
        #endregion

        #region constructors
        public JoursFeries(int id, DateTime jr)
        {
            this.id = id;
            jour = jr;
        }

        public JoursFeries(DateTime jr) : this(-1, jr) { }

        #endregion

        public override bool Equals(object obj)
        {
            return obj is JoursFeries ? Equals((JoursFeries)obj) : false;
        }

        public bool Equals(JoursFeries jf)
        {
            return jf.Jour.Day == this.Jour.Day && jf.Jour.Month == this.Jour.Month;
        }

        public override string ToString()
        {
            return string.Format("{0:00}/{1:00}/{2}", Jour.Day, Jour.Month, Jour.Year) ;
        }

        public override int GetHashCode()
        {
            return Jour.GetHashCode();
        }

    }
}
