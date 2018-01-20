using AgendaCore;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgendaBDDManager
{
    public class JoursFeriesManager
    {
        private static string maxID_request = "SELECT MAX(id) FROM JOURS_FERIES";
        public static List<JoursFeries> JOURS_FERIES = new List<JoursFeries>();


        public static void initialize()
        {
            JOURS_FERIES.Clear();
            JOURS_FERIES.AddRange(getAll());
        }

        public static List<JoursFeries> getAll()
        {
            List<JoursFeries> liste = new List<JoursFeries>();
            string requete = string.Format("SELECT id, jour FROM JOURS_FERIES");
            Connexion bdd = new Connexion();
            bdd.OpenConnection();
            OracleDataReader odr = bdd.ExecuteSelect(requete);
            while (odr.Read())
            {
                liste.Add(new JoursFeries(int.Parse(odr.GetDecimal(0).ToString()), odr.GetDateTime(1)));
            }
            bdd.CloseConnection();

            return liste;
        }

        public static void Add(JoursFeries jourFeries)
        {

            Connexion bdd = new Connexion();
            string requete = string.Format(@"INSERT INTO JOURS_FERIES(jour) 
                                            VALUES('{0}')", jourFeries.Jour.ToShortDateString());
            bdd.OpenConnection();
            bdd.ExecuteNonQuery(requete);
            OracleDataReader odr = bdd.ExecuteSelect(maxID_request);
            int maxID = 0;
            if (odr.Read())
            {
                maxID = int.Parse(odr.GetDecimal(0).ToString());
            }
            bdd.CloseConnection();
            jourFeries.Id = maxID;
        }
        public static void Delete(JoursFeries jourFeries)
        {
            string requete = string.Format("DELETE JOURS_FERIES WHERE id = {0} ", jourFeries.Id);
            Connexion bdd = new Connexion();
            bdd.OpenConnection();
            bdd.ExecuteNonQuery(requete);
            bdd.CloseConnection();
        }
    }
}
