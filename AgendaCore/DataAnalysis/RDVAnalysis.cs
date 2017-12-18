using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgendaCore.DataAnalysis
{
    public class RDVAnalysis
    {
        /// <summary>
        /// Le nombre de rendez-vous par semaine.
        /// </summary>
        public static List<KeyValuePair<string, int>> byWeek;

        /// <summary>
        /// Le nombre de rendez-vous par mois.
        /// </summary>
        public static List<KeyValuePair<string, KeyValuePair<string, int>>> byMonth;

        /// <summary>
        /// Le nombre de rendez-vous par année.
        /// </summary>
        public static List<KeyValuePair<string, int>> byYear;

        /// <summary>
        /// Le nombre moyen de rendez-vous par jour de la semaine.
        /// </summary>
        public static List<KeyValuePair<string, int>> byDay;

        /// <summary>
        /// Le nombre moyen de rendez-vous par tranche horaire.
        /// </summary>
        public static List<KeyValuePair<string, int>> byHour;
    }
}
