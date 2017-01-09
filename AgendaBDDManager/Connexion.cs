using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using Microsoft.Win32;
using System.IO;
using System.Windows.Forms;

namespace AgendaBDDManager
{
    public class Connexion
    {
        #region constante

        private const string CLIENT_TYPE = "CLIENT";

        #endregion

        #region Attributes

        /// <summary>
        /// La connexion à la base de données.
        /// </summary>
        private OracleConnection Connection;

        private string IpAdress;

        private string User;

        private string Password;

        private string SID = "xe";

        private string Oradb = @"Data Source=(DESCRIPTION=
                        (ADDRESS=(PROTOCOL=TCP)(HOST={ip})(PORT=1521))
                        (CONNECT_DATA=(SERVICE_NAME={sid})));
                        User Id={user};Password={pwd};";
        #endregion 

        #region Constructors

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        public Connexion()
        {
            IpAdress = getMachineType().Equals(CLIENT_TYPE) ? IpAdressManager.get_server_ip_adress() : "localhost";
            User = getUser();
            Password = getPassword();
            Connection = new OracleConnection(Oradb.Replace("{ip}",IpAdress).Replace("{sid}",SID).Replace("{user}",User).Replace("{pwd}",Password));
        }

        #endregion


        #region Methods

        public void OpenConnection()
        {
            if(Connection.State == System.Data.ConnectionState.Closed)
                Connection.Open();
        }

        public void CloseConnection()
        {
            if(Connection.State == System.Data.ConnectionState.Open)
                Connection.Close();
        }

        /// <summary>
        /// Permet d'éxécuter une requpete de type SELECT
        /// </summary>
        /// <param name="requete">La requête à exécuter</param>
        /// <returns>La liste des résultats</returns>
        public OracleDataReader ExecuteSelect(string requete)
        {
            OracleCommand cmd = new OracleCommand(requete, Connection);
            cmd.CommandType = System.Data.CommandType.Text;
            OracleDataReader odr = cmd.ExecuteReader();
            return odr;
        }

        /// <summary>
        /// Permet d'exécuter une requête de type insert, delete, update...
        /// </summary>
        /// <param name="requete">La requpete à exécuter</param>
        public void ExecuteNonQuery(string requete)
        {
            OracleCommand cmd = new OracleCommand(requete, Connection);
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.ExecuteNonQuery();
        }

        public static void init()
        {
            ClientManager.initialize();
            VehiculeManager.initialize();
            RdvManager.initialize();
            ReparationManager.initialize();
            ReparationRDVManager.initialize();
        }

        /// <summary>
        /// Permet de récupérer le mot de passe de connection à la BDD directement dans le registre HKEY_CURRENT_USER/Softawre/Agenda.
        /// </summary>
        /// <returns>Le mot de passe de connection à la BDD sous forme de chaîne de caractères.</returns>
        private string getPassword()
        {
            RegistryKey key = Registry.CurrentUser;
            string password = string.Empty;
            try
            {
                key = key.OpenSubKey(@"Software\Agenda");
                password = key.GetValue("password").ToString();
            }
            catch (Exception)
            {
            }
            finally
            {
                key.Close();
            }
            return password;
        }

        /// <summary>
        /// Permet de récupérer le nom d'utilisateur de connection à la BDD directement dans le registre HKEY_CURRENT_USER/Softawre/Agenda.
        /// </summary>
        /// <returns>Le nom d'utilisateur de connection à la BDD sous forme de chaîne de caractères.</returns>
        private string getUser()
        {
            RegistryKey key = Registry.CurrentUser;
            string user = string.Empty;
            try
            {
                key = key.OpenSubKey(@"Software\Agenda");
                user = key.GetValue("user").ToString();
            }
            catch (Exception)
            {
            }
            finally
            {
                key.Close();
            }
            return user;
        }

        /// <summary>
        /// Permet de récupérer le type de la machine locale : seveur ou client directement dans le registre HKEY_CURRENT_USER/Softawre/Agenda.
        /// </summary>
        /// <returns>le type de la machine locale sous forme de chaîne de caractères.</returns>
        private string getMachineType()
        {
            RegistryKey key = Registry.CurrentUser;
            string type = string.Empty;
            try
            {
                key = key.OpenSubKey(@"Software\Agenda");
                type = key.GetValue("type").ToString();
            }
            catch (Exception)
            {
            }
            finally
            {
                key.Close();
            }
            return type;
        }

        public string DeleteInjectionSQL(string data)
        {

            return data.Replace('\'',' ');
        }

        public static string getStringFromOdr(int index, OracleDataReader odr)
        {
            return odr.IsDBNull(index) ? "" : odr.GetString(index);
        }

        public static int getIntFromOdr(int index, OracleDataReader odr)
        {
            return odr.IsDBNull(index) ? -1 : int.Parse(odr.GetDecimal(index).ToString());
        }

        #endregion

        public static void Sauvegarde(object obj)
        {
            if (obj is string)
            {
                string path = obj as string;
                DateTime today = DateTime.Now;
                string nomFichier = string.Format("{0:00}-{1:00}-{2}_Sauvegarde_RendezVous.txt", today.Day, today.Month, today.Year);
                FileStream fichier = File.Create(string.Format(@"{0}\{1}", path, nomFichier));
                StreamWriter sw = new StreamWriter(fichier);
                sw.WriteLine("delete from facture;");
                sw.WriteLine("delete from rdv_reparation;");
                sw.WriteLine("delete from rendezvous;");
                sw.WriteLine("delete from vehicule;");
                sw.WriteLine("delete from client;");
                sw.WriteLine("delete from reparation;");
                sw.WriteLine("alter trigger tr_client disable;");
                sw.WriteLine("alter trigger tr_vehicule disable;");
                sw.WriteLine("alter trigger tr_rendezvous disable;");
                sw.WriteLine("alter trigger tr_rdv_reparation disable;");
                sw.WriteLine("alter trigger tr_reparation disable;");
                sw.WriteLine("alter trigger tr_facture disable;");
                ClientManager.Sauvegarde(sw);
                VehiculeManager.Sauvegarde(sw);
                RdvManager.Sauvegarde(sw);
                ReparationManager.Sauvegarde(sw);
                ReparationRDVManager.Sauvegarde(sw);
                FactureManager.Sauvegarde(sw);
                sw.WriteLine("alter trigger tr_rdv_reparation enable;");
                sw.WriteLine("alter trigger tr_reparation enable;");
                sw.WriteLine("alter trigger tr_facture enable;");
                sw.WriteLine("alter trigger tr_client enable;");
                sw.WriteLine("alter trigger tr_vehicule enable;");
                sw.WriteLine("alter trigger tr_rendezvous enable;"); 
                sw.Dispose();
                fichier.Dispose();
                MessageBox.Show("Sauvegarde réussie : " + path +"\\"+nomFichier);
            }
        }
    }
}
