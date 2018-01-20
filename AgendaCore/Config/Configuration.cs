using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Globalization;

namespace Agenda.Config
{
    public class Configuration
    {
        public static string ApplicationPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);

        public static string ModeleFactureFileName = "modele_facture.xlsx";

        public static string ModeleDevisFileName = "modele_devis.xlsx";

        public readonly static string[] Hours = new string[] { "8h", "9h", "10h", "11h", "12h", "13h", "14h", "15h", "16h", "17h", "18h" };

        /// <summary>
        /// Les mois de l'année.
        /// </summary>
        public readonly static string[] Months =  new string[] {"", "Janvier", "Février", "Mars", "Avril", "Mai", "Juin", "Juillet", "Août",
            "Septembre", "Octobre", "Novembre", "Décembre" };
        
        /// <summary>
        /// Ce dictionnaire permettra de traduire les jours de la semaine (Anglais => Français)
        /// </summary>
        public static Dictionary<string, string> Days = new Dictionary<string, string>();
        public static GregorianCalendar Calendrier = new GregorianCalendar(GregorianCalendarTypes.MiddleEastFrench);


        /// <summary>
        /// Permet d'obtenir l'index d'un jour. (de 0 à 6)
        /// </summary>
        /// <param name="day">Le jour de la semaine en anglais</param>
        /// <returns>un chiffre de 0 à 6</returns>
        public static int GetIndexOfDaysOfWeeks(string day)
        {
            return Days.Keys.ToList().IndexOf(day);
        }
        


        /// <summary>
        /// Permet d'initialiser le dictionnaire des jours de la semaine.
        /// </summary>
        public static void InitializeDays()
        {
            Days.Clear();
            Days.Add("Monday", "Lundi");
            Days.Add("Tuesday", "Mardi");
            Days.Add("Wednesday", "Mercredi");
            Days.Add("Thursday", "Jeudi");
            Days.Add("Friday", "Vendredi");
            Days.Add("Saturday", "Samedi");
            Days.Add("Sunday", "Dimanche");
        }
    }
}
