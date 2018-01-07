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
        private int id;

        /// <summary>
        /// Le client à qui appartient le véhicule.
        /// </summary>
        private Client client;

        /// <summary>
        /// L'immatriculation du véhicule.
        /// </summary>
        private string immatriculation;

        /// <summary>
        /// La marque du véhicule.
        /// </summary>
        private string marque;

        /// <summary>
        /// Le modèle du véhicule.
        /// </summary>
        private string modele;

        /// <summary>
        /// Le kilométrage du véhicule.
        /// </summary>
        private int kilometrage;

        /// <summary>
        /// L'année du véhicule.
        /// </summary>
        private string mAnnee;

        /// <summary>
        /// Les rendez-vous du client.
        /// </summary>
        private List<RendezVous> rendezVous = new List<RendezVous>();

        #endregion

        #region Properties

        /// <summary>
        /// L'identifiant du Vehicule.
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
        /// Le client à qui appartient le véhicule
        /// </summary>
        public Client Client
        {
            get
            {
                return client;
            }
            set
            {
                client = value;
            }
        }

        /// <summary>
        /// L'immatriculation du véhicule.
        /// </summary>
        public string Immatriculation
        {
            get
            {
                return immatriculation != null ? immatriculation.Trim() : immatriculation;
            }
            set
            {
                immatriculation = value;
            }
        }

        /// <summary>
        /// La marque du véhicule
        /// </summary>
        public string Marque
        {
            get
            {
                return marque != null ? marque.Trim() : marque;
            }
            set
            {
                marque = value;
            }
        }

        /// <summary>
        /// Le modèle du véhicule
        /// </summary>
        public string Modele
        {
            get
            {
                return modele != null ? modele.Trim() : modele;
            }
            set
            {
                modele = value;
            }
        }

        /// <summary>
        /// Kilometrage du véhicule
        /// </summary>
        public int Kilometrage
        {
            get
            {
                return kilometrage;
            }
            set
            {
                kilometrage = value;
            }
        }

        /// <summary>
        /// L'année du véhicule
        /// </summary>
        public string Annee
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
        public IEnumerable<RendezVous> RendezVous
        {
            get
            {
                return rendezVous;
            }
            set
            {
                rendezVous = value.ToList();
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
            this.id = id;
            Immatriculation = CleanImmat(immat);
            this.marque = marque;
            this.modele = modele;
            Annee = annee;
            Kilometrage = km;
            this.client = client;
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
            Id = -1;
            Immatriculation = CleanImmat(immat);
            Marque = marque;
            Modele = modele;
            Annee = annee;
            Kilometrage = km;
            Client = client;
        }

        public Vehicule()
        {
            Id = -1;
            Immatriculation = string.Empty;
            Marque = string.Empty;
            Modele = string.Empty;
            Annee = string.Empty;
            Kilometrage = 0;
            Client = new Client();
        }

        #endregion

        #region Methods

        public override string ToString()
        {
            return string.Format("{0} {1} [{2}]", marque, modele, immatriculation);
        }

        public void AddRendezVous(RendezVous rdv)
        {
            if (rdv != null)
            {
                rendezVous.Add(rdv);
            }
        }

        #endregion

        #region Operations
        public override int GetHashCode()
        {
            return Id;
        }

        public override bool Equals(object obj)
        {
            return obj is Vehicule && (obj as Vehicule).Id == this.Id;
        }
        #endregion
    }
}
