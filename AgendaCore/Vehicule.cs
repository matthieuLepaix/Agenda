using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgendaCore
{
    /// <summary>
    /// Cette classe permet de représenter un véhicule
    /// </summary>
    public class Vehicule
    {
        #region Attributes

        /// <summary>
        /// L'identifiant en base de données
        /// </summary>
        private int mId;

        /// <summary>
        /// Le client à qui appartient le véhicule.
        /// </summary>
        private Client mClient;

        /// <summary>
        /// L'immatriculation du véhicule.
        /// </summary>
        private string mImmatriculation;

        /// <summary>
        /// La marque du véhicule.
        /// </summary>
        private string mMarque;

        /// <summary>
        /// Le modèle du véhicule.
        /// </summary>
        private string mModele;

        /// <summary>
        /// Le kilométrage du véhicule.
        /// </summary>
        private int mKilometrage;

        /// <summary>
        /// L'année du véhicule.
        /// </summary>
        private string mAnnee;

        /// <summary>
        /// Les rendez-vous du client.
        /// </summary>
        private List<RendezVous> mRendezVous = new List<RendezVous>();

        #endregion

        #region Properties

        /// <summary>
        /// L'identifiant du Vehicule.
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
        /// Le client à qui appartient le véhicule
        /// </summary>
        public Client pClient
        {
            get
            {
                return mClient;
            }
            set
            {
                mClient = value;
            }
        }

        /// <summary>
        /// L'immatriculation du véhicule.
        /// </summary>
        public string pImmatriculation
        {
            get
            {
                return mImmatriculation;
            }
            set
            {
                mImmatriculation = value;
            }
        }

        /// <summary>
        /// La marque du véhicule
        /// </summary>
        public string pMarque
        {
            get
            {
                return mMarque;
            }
            set
            {
                mMarque = value;
            }
        }

        /// <summary>
        /// Le modèle du véhicule
        /// </summary>
        public string pModele
        {
            get
            {
                return mModele;
            }
            set
            {
                mModele = value;
            }
        }

        /// <summary>
        /// Kilometrage du véhicule
        /// </summary>
        public int pKilometrage
        {
            get
            {
                return mKilometrage;
            }
            set
            {
                mKilometrage = value;
            }
        }

        /// <summary>
        /// L'année du véhicule
        /// </summary>
        public string pAnnee
        {
            get
            {
                return mAnnee;
            }
            set
            {
                mAnnee = value;
            }
        }

        /// <summary>
        /// La liste des rendez-vous
        /// </summary>
        public IEnumerable<RendezVous> pRendezVous
        {
            get
            {
                return mRendezVous;
            }
            set
            {
                mRendezVous = value.ToList();
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructeur totalement renseigné (BDD)
        /// </summary>
        /// <param name="immat">L'immatriculation du véhicule</param>
        /// <param name="marque">La marque du véhicule</param>
        /// <param name="modele">Le modèle du véhicule</param>
        public Vehicule(int id, string marque, string modele, string immat, string annee, int km, Client client)
        {
            mId = id;
            mImmatriculation = CleanImmat(immat);
            mMarque = marque;
            mModele = modele;
            pAnnee = annee;
            pKilometrage = km;
            mClient = client;
        }

        private string CleanImmat(string immat)
        {
            return string.IsNullOrEmpty(immat) ? string.Empty : immat.Trim().Replace('-',' ').Replace('_',' ').Replace(" ","").ToUpper();
        }

        /// <summary>
        /// Constructeur totalement renseigné
        /// </summary>
        /// <param name="immat">L'immatriculation du véhicule</param>
        /// <param name="marque">La marque du véhicule</param>
        /// <param name="modele">Le modèle du véhicule</param>
        public Vehicule(string marque, string modele, string immat, string annee, int km, Client client)
        {
            mId = -1;
            mImmatriculation = CleanImmat(immat);
            mMarque = marque;
            mModele = modele;
            pAnnee = annee;
            pKilometrage = km;
            mClient = client;
        }

        #endregion

        #region Methods

        public override string ToString()
        {
            return string.Format("{0} {1} [{2}]", mMarque, mModele, mImmatriculation);
        }

        public void AddRendezVous(RendezVous rdv)
        {
            if (rdv != null)
            {
                mRendezVous.Add(rdv);
            }
        }

        #endregion

        #region Operations

        #endregion
    }
}
