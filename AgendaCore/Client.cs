using System;
using System.Collections.Generic;
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
        private List<Vehicule> mVehicules = new List<Vehicule>();

        /// <summary>
        /// L'identifiant en base de données
        /// </summary>
        private int mId;

        /// <summary>
        /// Le nom du client.
        /// </summary>
        private string mNom;

        /// <summary>
        /// Le prénom du client.
        /// </summary>
        private string mPrenom;

        /// <summary>
        /// Le premier téléphone du client.
        /// </summary>
        private string mTelephone1;

        /// <summary>
        /// Le second téléphone du client.
        /// </summary>
        private string mTelephone2;

        /// <summary>
        /// L'adresse email du client.
        /// </summary>
        private string mEmail;

        /// <summary>
        /// L'adresse postale du client.
        /// </summary>
        private string mAdresse;

        /// <summary>
        /// Le code postal du client.
        /// </summary>
        private string mCodePostal;

        /// <summary>
        /// La ville du client.
        /// </summary>
        private string mVille;
        #endregion

        #region Properties

        /// <summary>
        /// Les véhicules du client.
        /// </summary>
        public IEnumerable<Vehicule> pVehicules
        {
            get
            {
                return mVehicules;
            }
            set
            {
                mVehicules = value.ToList();
            }
        }

        /// <summary>
        /// L'identifiant du client.
        /// </summary>
        public int pId
        {
            get
            {
                return mId;
            }
            set
            {
                mId = value;
            }
        }

        /// <summary>
        /// Le nom du client.
        /// </summary>
        public string pNom
        {
            get
            {
                return mNom;
            }
            set
            {
                mNom = value;
            }
        }

        /// <summary>
        /// Le prénom du client.
        /// </summary>
        public string pPrenom
        {
            get
            {
                return mPrenom.Length > 1 ? mPrenom.ElementAt(0).ToString().ToUpper()+mPrenom.Substring(1,mPrenom.Length-1).ToLower() : mPrenom;
            }
            set
            {
                mPrenom = value;
            }
        }

        /// <summary>
        /// Le premier téléphone du client.
        /// </summary>
        public string pTelephone1
        {
            get
            {
                return mTelephone1;
            }
            set
            {
                mTelephone1 = value;
            }
        }

        

        /// <summary>
        /// Le second téléphone du client.
        /// </summary>
        public string pTelephone2
        {
            get
            {
                return mTelephone2;
            }
            set
            {
                mTelephone2 = value;
            }
        }

        /// <summary>
        /// L'adresse email du client.
        /// </summary>
        public string pEmail
        {
            get
            {
                return mEmail;
            }
            set
            {
                mEmail = value;
            }
        }

        /// <summary>
        /// L'adresse du client.
        /// </summary>
        public string pAdresse
        {
            get
            {
                return mAdresse;
            }
            set
            {
                mAdresse = value;
            }
        }

        /// <summary>
        /// L'adresse email du client.
        /// </summary>
        public string pCodePostal
        {
            get
            {
                return mCodePostal;
            }
            set
            {
                mCodePostal = value;
            }
        }

        /// <summary>
        /// La ville du client.
        /// </summary>
        public string pVille
        {
            get
            {
                return mVille;
            }
            set
            {
                mVille = value;
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
            mId = -1;
            mNom = nom;
            mPrenom = prenom;
            mTelephone1 = telephone1;
            mTelephone2 = telephone2;
            mEmail = email;
            mAdresse = adresse;
            mCodePostal = codePostal;
            mVille = ville;
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
            mId = id;
            mNom = nom;
            mPrenom = prenom;
            mTelephone1 = telephone1;
            mTelephone2 = telephone2;
            mEmail = email;
            mAdresse = adresse;
            mCodePostal = codePostal;
            mVille = ville;
        }

        #endregion

        #region Methods

        public override string ToString()
        {
            return string.Format("M. {0} {1}", pNom, pPrenom);
        }

        public void AddVehicule(Vehicule v)
        {
            mVehicules.Add(v);
        }

        public void RemoveVehicule(Vehicule v)
        {
            mVehicules.Remove(v);
        }

        public void AddVehicules(List<Vehicule> v)
        {
            mVehicules.AddRange(v);
        }

        #endregion

        #region Operations

        #endregion
    }
}
