using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AgendaCore;
using Oracle.DataAccess.Client;


namespace AgendaBDDManager
{
    public class ClientManager
    {
        #region Attributes

        public static List<Client> CLIENTS = new List<Client>();

        private static string maxID_request = "SELECT MAX(id) FROM client";


        private static string insertClient = @"INSERT INTO CLIENT (id,nom,prenom,telephone1,telephone2,email,adresse,codepostal,ville) 
                                        VALUES ({0},'{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}');";
        
        #endregion

        #region Properties

        #endregion

        #region Constructors

        #endregion

        #region Methods

        public static void initialize()
        {
            CLIENTS.AddRange(getAll());
        }

        public static Client getClientById(int id)
        {
             //TODO
            return null;
        
        }

        public static List<Client> getAllforOldVersion()
        {
            List<Client> liste = new List<Client>();
            string requete = string.Format("SELECT client_id, nom, prenom, tel1, tel2 FROM client");
            Connexion bdd = new Connexion();
            bdd.OpenConnection();
            OracleDataReader odr = bdd.ExecuteSelect(requete);
            while (odr.Read())
            {
                liste.Add(new Client(int.Parse(odr.GetDecimal(0).ToString()),
                                    Connexion.getStringFromOdr(1, odr),
                                    Connexion.getStringFromOdr(2, odr),
                                    Connexion.getStringFromOdr(3, odr),
                                    Connexion.getStringFromOdr(4,odr), "","","", ""));
            }
            bdd.CloseConnection();

            //foreach (Client c in liste)
            //{
            //    c.AddVehicules(VehiculeManager.GetVehiculesByClient(c));
            //}

            return liste;
        }

        public static List<Client> getAll()
        {
            List<Client> liste = new List<Client>();
            string requete = string.Format("SELECT id, nom, prenom, telephone1, telephone2, email, adresse, codepostal, ville FROM client");
            Connexion bdd = new Connexion();
            bdd.OpenConnection();
            OracleDataReader odr = bdd.ExecuteSelect(requete);
            while (odr.Read())
            {
                liste.Add(new Client(int.Parse(odr.GetDecimal(0).ToString()),
                                    Connexion.getStringFromOdr(1, odr),
                                    Connexion.getStringFromOdr(2, odr),
                                    Connexion.getStringFromOdr(3, odr),
                                    Connexion.getStringFromOdr(4, odr),
                                    Connexion.getStringFromOdr(5, odr),
                                    Connexion.getStringFromOdr(6, odr),
                                    Connexion.getStringFromOdr(7, odr),
                                    Connexion.getStringFromOdr(8,odr)));
            }
            bdd.CloseConnection();

            foreach (Client c in liste)
            {
                c.AddVehicules(VehiculeManager.GetVehiculesByClient(c));
            }

            return liste;
        }

        public static void AddClient(string nom, string prenom, string tel1, string tel2, string email, string adresse, string cp, string ville)
        {
            Client c = new Client(nom, prenom, tel1, tel2, email, adresse, cp, ville);
            AddClient(c);
        }

        public static void AddClient(Client client)
        {
            
            Connexion bdd = new Connexion();
            string requete = string.Format(@"INSERT INTO client(nom, prenom,telephone1,telephone2,email, adresse, codepostal, ville) 
                                            VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')",
                                                    bdd.DeleteInjectionSQL(client.pNom), bdd.DeleteInjectionSQL(client.pPrenom),
                                                    bdd.DeleteInjectionSQL(client.pTelephone1).Substring(0, 30), bdd.DeleteInjectionSQL(client.pTelephone2).Substring(0, 30),
                                                    bdd.DeleteInjectionSQL(client.pEmail), bdd.DeleteInjectionSQL(client.pAdresse),
                                                    bdd.DeleteInjectionSQL(client.pCodePostal), bdd.DeleteInjectionSQL(client.pVille));
            bdd.OpenConnection();
            bdd.ExecuteNonQuery(requete);
            OracleDataReader odr = bdd.ExecuteSelect(maxID_request);
            int maxID = 0;
            if (odr.Read())
            {
                maxID = int.Parse(odr.GetDecimal(0).ToString());
            }
            bdd.CloseConnection();
            client.pId = maxID;
            CLIENTS.Add(client);
        }
        

        public static void UpdateClient(Client client)
        {
            
            Connexion bdd = new Connexion();
            string requete = string.Format(@"UPDATE client 
                                            SET 
                                                nom = '{0}', 
                                                prenom = '{1}', 
                                                telephone1 = '{2}', 
                                                telephone2 = '{3}', 
                                                email = '{4}', 
                                                adresse = '{5}', 
                                                codepostal = '{6}', 
                                                ville = '{7}'
                                            WHERE 
                                                id = {8}",
                                                    bdd.DeleteInjectionSQL(client.pNom), bdd.DeleteInjectionSQL(client.pPrenom), 
                                                    bdd.DeleteInjectionSQL(client.pTelephone1), bdd.DeleteInjectionSQL(client.pTelephone2), 
                                                    bdd.DeleteInjectionSQL(client.pEmail), bdd.DeleteInjectionSQL(client.pAdresse),
                                                    bdd.DeleteInjectionSQL(client.pCodePostal), bdd.DeleteInjectionSQL(client.pVille),
                                                    client.pId.ToString());
            bdd.OpenConnection();
            bdd.ExecuteNonQuery(requete);
            bdd.CloseConnection();
            CLIENTS.Remove(CLIENTS.Find(c => c.pId == client.pId));
            CLIENTS.Add(client);
            
        }

        public static void DeleteClient(Client client)
        {
            string requete = string.Format("DELETE client WHERE id = {0} ", client.pId);
            Connexion bdd = new Connexion();
            bdd.OpenConnection();
            bdd.ExecuteNonQuery(requete);
            bdd.CloseConnection();
            CLIENTS.Remove(CLIENTS.Find(c => c.pId == client.pId));
        }

        #endregion


        public static void Sauvegarde(System.IO.StreamWriter sw)
        {
            foreach (Client c in getAll())
            {
                SaveClient(sw, c);
            }
        }

        public static void SaveClient(System.IO.StreamWriter sw, Client c)
        {
            sw.WriteLine(string.Format(insertClient, c.pId, c.pNom, c.pPrenom, c.pTelephone1, c.pTelephone2, c.pEmail, c.pAdresse, c.pCodePostal, c.pVille));
        }
    }
}
