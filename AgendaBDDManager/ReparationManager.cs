using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AgendaCore;
using Oracle.DataAccess.Client;

namespace AgendaBDDManager
{
    public class ReparationManager
    {
        #region Attributes

        public static List<Reparation> REPARATIONS = new List<Reparation>();

        public static string insertReparation = @"INSERT INTO reparation(id,nom) VALUES({0},'{1}');";


        private static string maxID_request = "SELECT MAX(id) FROM reparation";

        #endregion

        #region Methods

        public static void initialize()
        {
            REPARATIONS.Clear();
            REPARATIONS.AddRange(getAll());
        }

        public static Reparation getReparationById(int id)
        {
            Reparation rep = null;
            string requete = string.Format("SELECT id, nom FROM reparation WHERE id={0}", id.ToString());
            Connexion bdd = new Connexion();
            bdd.OpenConnection();
            OracleDataReader odr = bdd.ExecuteSelect(requete);
            if (odr.Read())
            {
                rep = new Reparation(int.Parse(odr.GetDecimal(0).ToString()), odr.GetString(1));
            }
            bdd.CloseConnection();

            return rep;
        }

        public static List<Reparation> getAll()
        {
            List<Reparation> liste = new List<Reparation>();
            string requete = string.Format("SELECT id, nom FROM reparation ORDER BY nom");
            Connexion bdd = new Connexion();
            bdd.OpenConnection();
            OracleDataReader odr = bdd.ExecuteSelect(requete);
            while (odr.Read())
            {
                liste.Add(new Reparation(int.Parse(odr.GetDecimal(0).ToString()), odr.GetString(1)));
            }
            bdd.CloseConnection();

            return liste;
        }

        public static void AddReparation(string nom)
        {
            Reparation r = new Reparation(nom);
            AddReparation(r);
        }

        public static void AddReparation(Reparation rep)
        {
            
            Connexion bdd = new Connexion();
            string requete = string.Format("INSERT INTO reparation(nom) VALUES('{0}')",
                                                    bdd.DeleteInjectionSQL(rep.pNom));
            bdd.OpenConnection();
            bdd.ExecuteNonQuery(requete);
            OracleDataReader odr = bdd.ExecuteSelect(maxID_request);
            int maxID = 0;
            if (odr.Read())
            {
                maxID = int.Parse(odr.GetDecimal(0).ToString());
            }
            bdd.CloseConnection();
            rep.pId = maxID;
            REPARATIONS.Add(rep);
        }
        

        public static void UpdateReparation(Reparation r)
        {
            Connexion bdd = new Connexion();
            string requete = string.Format(@"UPDATE reparation 
                                            SET 
                                                nom = '{0}'
                                            WHERE 
                                                id = {8}",
                                                    bdd.DeleteInjectionSQL(r.pNom),
                                                    r.pId.ToString());
            bdd.OpenConnection();
            bdd.ExecuteNonQuery(requete);
            bdd.CloseConnection();
            REPARATIONS.Remove(REPARATIONS.Find(c => c.pId == r.pId));
            REPARATIONS.Add(r);
            
        }

        public static void DeleteReparation(Reparation r)
        {
            string requete = string.Format("DELETE reparation WHERE id = {0} ", r.pId);
            Connexion bdd = new Connexion();
            bdd.OpenConnection();
            bdd.ExecuteNonQuery(requete);
            bdd.CloseConnection();
            REPARATIONS.Remove(REPARATIONS.Find(c => c.pId == r.pId));
        }

        #endregion

        internal static void Sauvegarde(System.IO.StreamWriter sw)
        {
            foreach (Reparation r in getAll())
            {
                sw.WriteLine(insertReparation, r.pId, r.pNom);
            }
        }


    }
}
