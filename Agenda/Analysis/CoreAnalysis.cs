using AgendaBDDManager;
using AgendaCore;
using AgendaCore.DataAnalysis;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Agenda.Analysis
{
    public class CoreAnalysis
    {

        private static List<RendezVous> all = RdvManager.getAll();

        private static string[] Months = new string[13] { "", "Janvier", "Février", "Mars", "Avril", "Mai", "Juin", "Juillet",
                                                    "Août", "Septembre", "Octobre", "Novembre", "Décembre"};
        /// <summary>
        /// Permet de charger les données relatif aux rendez-vous.
        /// </summary>
        public static void LoadRDVAnalysis()
        {
            RDVAnalysis.byYear = new List<KeyValuePair<string, int>>();
            RDVAnalysis.byMonth = new List<KeyValuePair<string, KeyValuePair<string, int>>>();
            RDVAnalysis.byWeek = new List<KeyValuePair<string, int>>();
            RDVAnalysis.byDay = new List<KeyValuePair<string, int>>();
            RDVAnalysis.byHour = new List<KeyValuePair<string, int>>();

            all.Select(r => r.Date.Year.ToString()).Distinct().ToList().ForEach(year =>
            {
                RDVAnalysis.byYear.Add(new KeyValuePair<string, int>(
                    year,
                    all.Count(r => r.Date.Year.ToString() == year)));



                Months.ToList().ForEach(month =>
                {
                    RDVAnalysis.byMonth.Add(new KeyValuePair<string, KeyValuePair<string, int>>( 
                        year, new KeyValuePair<string, int>(
                        month,
                        all.Count(r => r.Date.Year.ToString() == year && 
                                        r.Date.Month == Months.ToList().IndexOf(month)))));
                });
            });
            
            var weeks = all.Select(r => getWeekOfTheYear(r.Date)).Distinct().ToList();
            weeks.Sort();
            foreach (string week in weeks)
            {
                RDVAnalysis.byWeek.Add(new KeyValuePair<string, int>(
                    week,
                    all.Count(r => //r.pDate.Year == DateTime.Now.Year &&
                                    getWeekOfTheYear(r.Date) == week)));
            }
            foreach (string day in all.Select(r => r.Date.ToLocalTime().DayOfWeek.ToString()).Distinct())
            {
                RDVAnalysis.byDay.Add(new KeyValuePair<string, int>(
                    day,
                    all.Count(r => //r.pDate.Year == DateTime.Now.Year && 
                                    r.Date.DayOfWeek.ToString() == day)));
            }
            foreach (string hour in all.Select(r => r.Date.Hour.ToString()).Distinct())
            {
                RDVAnalysis.byHour.Add(new KeyValuePair<string, int>(
                    hour,
                    all.Count(r => //r.pDate.Year == DateTime.Now.Year &&
                                    r.Date.Hour.ToString() == hour)));
            }
        }

        public static void LoadClientAnalysis()
        {
            try
            {
                ClientAnalysis.byClients = new List<KeyValuePair<string, int>>();
                ClientAnalysis.byVehicules = new List<KeyValuePair<string, int>>();

                all.Select(r => r.Client).Distinct().ToList().ForEach(client =>
                {
                    ClientAnalysis.byClients.Add(new KeyValuePair<string, int>(
                        client.ToString(),
                        all.Count(r => r.Client == client)));
                });

                all.Select(r => r.Vehicule.Marque).Distinct().ToList().ForEach(marque =>
                {
                    ClientAnalysis.byVehicules.Add(new KeyValuePair<string, int>(
                        marque,
                        all.Count(r => r.Vehicule.Marque == marque)));
                });
            }
            catch (Exception e )
            {
                Console.WriteLine(e);
            }
        }

        /// <summary>
        /// Permet de récupérer le numéro de semaine de l'année.
        /// </summary>
        /// <param name="d">La date</param>
        /// <returns></returns>
        private static string getWeekOfTheYear(DateTime d)
        {
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(d, CalendarWeekRule.FirstFourDayWeek, 
                DayOfWeek.Monday).ToString("00");
        }
    }
}
