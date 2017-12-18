using System.Collections.Generic;

namespace AgendaCore.DataAnalysis
{
    public class ClientAnalysis
    {
        /// <summary>
        /// Le nombre de rendez-vous par clients.
        /// </summary>
        public static List<KeyValuePair<string, int>> byClients;

        /// <summary>
        /// Le nombre de rendez-vous par marque de véhicule.
        /// </summary>
        public static List<KeyValuePair<string, int>> byVehicules;
    }
}
