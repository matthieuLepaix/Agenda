using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace AgendaCore
{
    public class Client
    {
        #region Attributes

        /// <summary>
        /// Les véhicules du client.
        /// </summary>
        private List<Vehicule> vehicules = new List<Vehicule>();

        /// <summary>
        /// L'identifiant en base de données
        /// </summary>
        private int id;

        /// <summary>
        /// Le nom du client.
        /// </summary>
        private string nom;

        /// <summary>
        /// Le prénom du client.
        /// </summary>
        private string prenom;

        /// <summary>
        /// Le premier téléphone du client.
        /// </summary>
        private string telephone1;

        /// <summary>
        /// Le second téléphone du client.
        /// </summary>
        private string telephone2;

        /// <summary>
        /// L'adresse email du client.
        /// </summary>
        private string email;

        /// <summary>
        /// L'adresse postale du client.
        /// </summary>
        private string adresse;

        /// <summary>
        /// Le code postal du client.
        /// </summary>
        private string codePostal;

        /// <summary>
        /// La ville du client.
        /// </summary>
        private string ville;
        #endregion

        #region Properties

        /// <summary>
        /// Les véhicules du client.
        /// </summary>
        public List<Vehicule> Vehicules
        {
            get
            {
                return vehicules;
            }
            set
            {
                vehicules = value;
            }
        }

        /// <summary>
        /// L'identifiant du client.
        /// </summary>
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

        /// <summary>
        /// Le nom du client.
        /// </summary>
        public string Nom
        {
            get
            {
                return nom;
            }
            set
            {
                nom = value;
            }
        }

        /// <summary>
        /// Le prénom du client.
        /// </summary>
        public string Prenom
        {
            get
            {
                return prenom.Length > 1 ? prenom.ElementAt(0).ToString().ToUpper()+prenom.Substring(1,prenom.Length-1).ToLower() : prenom;
            }
            set
            {
                prenom = value;
            }
        }

        /// <summary>
        /// Le premier téléphone du client.
        /// </summary>
        public string Telephone1
        {
            get
            {
                return telephone1;
            }
            set
            {
                telephone1 = value;
            }
        }

        

        /// <summary>
        /// Le second téléphone du client.
        /// </summary>
        public string Telephone2
        {
            get
            {
                return telephone2;
            }
            set
            {
                telephone2 = value;
            }
        }

        /// <summary>
        /// L'adresse email du client.
        /// </summary>
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
            }
        }

        /// <summary>
        /// L'adresse du client.
        /// </summary>
        public string Adresse
        {
            get
            {
                return adresse;
            }
            set
            {
                adresse = value;
            }
        }

        /// <summary>
        /// L'adresse email du client.
        /// </summary>
        public string CodePostal
        {
            get
            {
                return codePostal;
            }
            set
            {
                codePostal = value;
            }
        }

        /// <summary>
        /// La ville du client.
        /// </summary>
        public string Ville
        {
            get
            {
                return ville;
            }
            set
            {
                ville = value;
            }
        }

        //public static List<Client> clients = new List<Client>();


        #endregion

        #region Constructors

        /// <summary>
        /// Constructeur totalement renseigné
        /// </summary>
        /// <param name="nom">Le nom du client</param>
        /// <param name="prenom">Le prénom du client</param>
        /// <param name="telephone1">Le premier téléphone</param>
        /// <param name="telephone2">Le second téléphone</param>
        public Client(string nom, string prenom, string telephone1, string telephone2, string email, string adresse, string codePostal, string ville)
        {
            Id = -1;
            Nom = nom;
            Prenom = prenom;
            Telephone1 = telephone1;
            Telephone2 = telephone2;
            Email = email;
            Adresse = adresse;
            CodePostal = codePostal;
            Ville = ville;
        }

        /// <summary>
        /// Constructeur totalement renseigné pour les clients en bdd (avec id)
        /// </summary>
        /// <param name="nom">Le nom du client</param>
        /// <param name="prenom">Le prénom du client</param>
        /// <param name="telephone1">Le premier téléphone</param>
        /// <param name="telephone2">Le second téléphone</param>
        public Client(int id, string nom, string prenom, string telephone1, string telephone2, string email, string adresse, string codePostal, string ville)
        {
            Id = id;
            Nom = nom;
            Prenom = prenom;
            Telephone1 = telephone1;
            Telephone2 = telephone2;
            Email = email;
            Adresse = adresse;
            CodePostal = codePostal;
            Ville = ville;
        }

        public Client()
        {
            Id = -1;
            Nom = string.Empty;
            Prenom = string.Empty;
            Telephone1 = string.Empty;
            Telephone2 = string.Empty;
            Email = string.Empty;
            Adresse = string.Empty;
            CodePostal = string.Empty;
            Ville = string.Empty;
        }

        #endregion

        #region Methods

        public override string ToString()
        {
            return string.Format("M. ou Mme {0} {1}", string.IsNullOrEmpty(Nom) ? string.Empty : Nom.ToUpper(), string.IsNullOrEmpty(Prenom) ? string.Empty : string.Format("{0}{1}", Prenom.Substring(0, 1), Prenom.Substring(1, Prenom.Length - 1)));
        }

        public void AddVehicule(Vehicule v)
        {
            vehicules.Add(v);
        }

        public void RemoveVehicule(Vehicule v)
        {
            vehicules.Remove(v);
        }

        public void AddVehicules(List<Vehicule> vehicules)
        {
            vehicules.ForEach(v => Vehicules.Add(v));
        }

        #endregion

        #region Operations
        public override int GetHashCode()
        {
            return Id;
        }

        #endregion
    }
}
