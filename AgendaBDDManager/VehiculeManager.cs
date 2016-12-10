using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AgendaCore;
using Oracle.DataAccess.Client;


namespace AgendaBDDManager
{
    public class VehiculeManager
    {
        #region Attributes

        public static List<Vehicule> VEHICULES = new List<Vehicule>();

        private static string maxID_request = "SELECT MAX(id) FROM vehicule";

        private static string insertVehicule = @"INSERT INTO VEHICULE (id,marque,modele,immatriculation,annee,kilometrage,client) VALUES ({0},'{1}','{2}','{3}','{4}','{5}',{6});";

        #endregion

        #region Properties

        #endregion

        #region Constructors

        #endregion

        #region Methods

        public static void initialize()
        {
            VEHICULES.AddRange(getAll());
        }

        public static List<Vehicule> getAllForOldVersion()
        {
            List<Vehicule> liste = new List<Vehicule>();

            string requete = string.Format(@"SELECT v.vehicule_id, v.marque, v.modele, v.immatriculation, v.client_id
                                            FROM vehicule v");
            Connexion bdd = new Connexion();
            bdd.OpenConnection();
            OracleDataReader odr = bdd.ExecuteSelect(requete);
            while (odr.Read())
            {
                liste.Add(new Vehicule(Connexion.getIntFromOdr(0,odr),
                                        Connexion.getStringFromOdr(1, odr),
                                        Connexion.getStringFromOdr(2, odr),
                                        Connexion.getStringFromOdr(3, odr),
                                        "année",
                                        0,
                                        new Client(Connexion.getIntFromOdr(4,odr),"","","","","","","","")));
            }
            bdd.CloseConnection();

            return liste;
        }

        private static List<Vehicule> getAll()
        {
            List<Vehicule> liste = new List<Vehicule>();

            string requete = string.Format(@"SELECT v.id, v.marque, v.modele, v.immatriculation, v.annee, v.kilometrage, v.client
                                            FROM vehicule v");
            Connexion bdd = new Connexion();
            bdd.OpenConnection();
            OracleDataReader odr = bdd.ExecuteSelect(requete);
            while (odr.Read())
            {
                liste.Add(new Vehicule(Connexion.getIntFromOdr(0,odr), 
                                        Connexion.getStringFromOdr(1, odr),
                                        Connexion.getStringFromOdr(2, odr),
                                        Connexion.getStringFromOdr(3, odr),
                                        Connexion.getStringFromOdr(4, odr),
                                        int.Parse(Connexion.getStringFromOdr(5, odr)), 
                                        ClientManager.CLIENTS.Find(c => c.pId == Connexion.getIntFromOdr(6, odr))));
            }
            bdd.CloseConnection();

            return liste;
        }

        public static Vehicule getVehiculeById(int id)
        {
            //TODO
            return null;
        }

        /// <summary>
        /// Cette fonction permet de récupérer tous les véhicules ayant une immat donnée.
        /// Si la liste contient plusieurs véhicule (impossible normalement) alors il y a une erreur en BDD.
        /// </summary>
        /// <param name="immat"></param>
        /// <returns></returns>
        public static List<Vehicule> GetVehiculesByImmatriculation(string immat)
        {
            List<Vehicule> liste = new List<Vehicule>();
            string requete = string.Format("SELECT id, marque, modele, immatriculation, annee, kilometrage, client FROM vehicule WHERE UPPER(REPLACE(REPLACE(immatriculation,'-',''),' ','')) = UPPER(REPLACE(REPLACE('{0}','-',''),' ',''))", immat);
            Connexion bdd = new Connexion();
            bdd.OpenConnection();
            OracleDataReader odr = bdd.ExecuteSelect(requete);
            while (odr.Read())
            {
                int id_client = Connexion.getIntFromOdr(6,odr);
                liste.Add(new Vehicule(Connexion.getIntFromOdr(0, odr),
                                        Connexion.getStringFromOdr(1, odr),
                                        Connexion.getStringFromOdr(2, odr),
                                        Connexion.getStringFromOdr(3, odr),
                                        Connexion.getStringFromOdr(4, odr),
                                        int.Parse(Connexion.getStringFromOdr(5, odr)), 
                                        ClientManager.getClientById(id_client)));
            }
            bdd.CloseConnection();
            return liste;
        }
        

        public static List<Vehicule> GetVehiculesByClient(Client c)
        {
            List<Vehicule> liste = new List<Vehicule>();
            string requete = string.Format("SELECT id, marque, modele, immatriculation, annee, kilometrage FROM vehicule WHERE client = {0}", c.pId);
            Connexion bdd = new Connexion(); 
            bdd.OpenConnection();
            OracleDataReader odr = bdd.ExecuteSelect(requete);
            while (odr.Read())
            {
                liste.Add(new Vehicule(Connexion.getIntFromOdr(0, odr), 
                                        Connexion.getStringFromOdr(1, odr),
                                        Connexion.getStringFromOdr(2, odr),
                                        Connexion.getStringFromOdr(3, odr),
                                        Connexion.getStringFromOdr(4, odr),
                                        int.Parse(Connexion.getStringFromOdr(5, odr)), 
                                        c));
            }
            bdd.CloseConnection();
            return liste;
        }

        public static void AddVehicule(string marque, string modele, string immat, string annee, int km, Client c)
        {
            Vehicule v = new Vehicule(marque, modele, immat, annee, km, c);
            AddVehicule(v);
        }

        public static void AddVehicule(Vehicule vehicule)
        {
            Connexion bdd = new Connexion();
            OracleDataReader odr;
            bdd.OpenConnection();
            int idClient = -1;
            if (vehicule.pClient.pId == -1)
            // récupérer l'id du client s'il est null
            {
                string requeteClient = string.Format("SELECT id FROM client WHERE nom = '{0}' AND telephone1 = '{1}'", 
                    bdd.DeleteInjectionSQL(vehicule.pClient.pNom), bdd.DeleteInjectionSQL(vehicule.pClient.pTelephone1));

                odr = bdd.ExecuteSelect(requeteClient);


                if (odr.Read())
                {
                    idClient = int.Parse(odr.GetDecimal(0).ToString());
                    
                    string requete = string.Format("INSERT INTO vehicule(marque, modele, immatriculation, annee, kilometrage, client) VALUES('{0}','{1}','{2}','{3}','{4}',{5})",
                                                    bdd.DeleteInjectionSQL(vehicule.pMarque), bdd.DeleteInjectionSQL(vehicule.pModele), bdd.DeleteInjectionSQL(vehicule.pImmatriculation), bdd.DeleteInjectionSQL(vehicule.pAnnee), vehicule.pKilometrage, idClient);
                    bdd.ExecuteNonQuery(requete);
                }
                //Sinon Erreur !
            }
            else
            {
                idClient = vehicule.pClient.pId;
                string requete = string.Format("INSERT INTO vehicule(marque, modele, immatriculation, annee, kilometrage, client) VALUES('{0}','{1}','{2}','{3}','{4}',{5})",
                                                       bdd.DeleteInjectionSQL(vehicule.pMarque), bdd.DeleteInjectionSQL(vehicule.pModele), bdd.DeleteInjectionSQL(vehicule.pImmatriculation), bdd.DeleteInjectionSQL(vehicule.pAnnee), vehicule.pKilometrage, vehicule.pClient.pId);
                bdd.ExecuteNonQuery(requete);
            }

            odr = bdd.ExecuteSelect(maxID_request);
            int maxID = 0;
            if (odr.Read())
            {
                maxID = int.Parse(odr.GetDecimal(0).ToString());
            }
            vehicule.pClient = ClientManager.CLIENTS.Find(c => c.pId == idClient);
            vehicule.pId = maxID;
            VEHICULES.Add(vehicule);
            vehicule.pClient.AddVehicule(vehicule);
            bdd.CloseConnection();

        }

        public static void AddVehicules(params Vehicule[] vehicules)
        {
            foreach (Vehicule v in vehicules)
            {
                AddVehicule(v);
            }
        }


        public static void UpdateVehicule(Vehicule vehicule)
        {
            Connexion bdd = new Connexion();


            string requete = string.Format(@"UPDATE vehicule 
                                            SET 
                                                marque = '{0}', 
                                                modele = '{1}', 
                                                immatriculation = '{2}', 
                                                annee = '{3}',
                                                kilometrage= '{4}',
                                                client = {5} 
                                            WHERE id = {6}",
                                                       bdd.DeleteInjectionSQL(vehicule.pMarque), 
                                                       bdd.DeleteInjectionSQL(vehicule.pModele),
                                                       bdd.DeleteInjectionSQL(vehicule.pImmatriculation),
                                                       bdd.DeleteInjectionSQL(vehicule.pAnnee),
                                                       vehicule.pKilometrage,
                                                       vehicule.pClient.pId,
                                                       vehicule.pId.ToString());
            bdd.OpenConnection();
            bdd.ExecuteNonQuery(requete);
            bdd.CloseConnection();
            VEHICULES.Remove(VEHICULES.Find(v => v.pId == vehicule.pId));
            VEHICULES.Add(vehicule);
        }

        public static void DesaffecterClient(Vehicule vehicule)
        {
            Connexion bdd = new Connexion();
            string requete = string.Format(@"UPDATE vehicule 
                                            SET 
                                                marque = '{0}', 
                                                modele = '{1}', 
                                                immatriculation = '{2}', 
                                                annee = '{3}',
                                                kilometrage= '{4}',
                                                client = {5} 
                                            WHERE id = {6}",
                                                       bdd.DeleteInjectionSQL(vehicule.pMarque),
                                                       bdd.DeleteInjectionSQL(vehicule.pModele),
                                                       bdd.DeleteInjectionSQL(vehicule.pImmatriculation),
                                                       bdd.DeleteInjectionSQL(vehicule.pAnnee),
                                                       vehicule.pKilometrage,
                                                       "null",
                                                       vehicule.pId.ToString());
            bdd.OpenConnection();
            bdd.ExecuteNonQuery(requete);
            bdd.CloseConnection();
            vehicule.pClient = null;
            VEHICULES.Remove(VEHICULES.Find(v => v.pId == vehicule.pId));
            VEHICULES.Add(vehicule);
        }

        public static void DeleteVehicule(int id)
        {
            string requete = string.Format("DELETE vehicule WHERE id = {0} ", id);
            Connexion bdd = new Connexion();
            bdd.OpenConnection();
            bdd.ExecuteNonQuery(requete);
            bdd.CloseConnection();
            VEHICULES.Remove(VEHICULES.Find(v => v.pId == id));
        }

        #endregion


        public static void Sauvegarde(System.IO.StreamWriter sw)
        {
            foreach (Vehicule v in getAll())
            {
                sw.WriteLine(insertVehicule, v.pId, v.pMarque, v.pModele, v.pImmatriculation, v.pAnnee, v.pKilometrage, v.pClient.pId);
            }
        }

        internal static void saveVehicule(System.IO.StreamWriter sw, List<Vehicule> vehicules)
        {
            foreach(Vehicule v in vehicules) {
                sw.WriteLine(string.Format(insertVehicule, v.pId, v.pMarque, v.pModele, v.pImmatriculation, v.pAnnee, v.pKilometrage, v.pClient.pId));
            }
        }
    }
}
