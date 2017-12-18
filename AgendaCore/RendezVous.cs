using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgendaCore
{
    public class RendezVous
    {
        #region Attributes

        /// <summary>
        /// L'identifiant en base de données
        /// </summary>
        private int id;

        /// <summary>
        /// La date du durée.
        /// </summary>
        private DateTime date;

        /// <summary>
        /// La durée des travaux à effectuer.
        /// </summary>
        private DureeType duree;

        /// <summary>
        /// Les travaux à effectuer.
        /// </summary>
        private List<ReparationRDV> travaux = new List<ReparationRDV>();

        private Vehicule vehicule;

        private Client client;

        #endregion

        #region Properties

        /// <summary>
        /// L'identifiant du rendez-vous.
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

        public DateTime Date
        {
            get
            {
                return date;
            }
            set
            {
                date = value;
            }
        }

        public DureeType Duree
        {
            get
            {
                return duree;
            }
            set
            {
                duree = value;
            }
        }

        public IEnumerable<ReparationRDV> Travaux
        {
            get
            {
                return travaux;
            }
            set
            {
                travaux = value.ToList();
            }
        }

        public Vehicule Vehicule
        {
            get
            {
                return vehicule;
            }
            set
            {
                vehicule = value;
            }
        }

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

        #endregion

        #region Constructors

        public RendezVous(int id, DateTime date, DureeType duree, Vehicule vehicule, Client client)
        {
            this.id = id;
            this.date = date;
            this.duree = duree;
            this.vehicule = vehicule;
            this.client = client;
        }

        public RendezVous(DateTime date, DureeType duree, Vehicule vehicule, Client client)
        {
            id = -1;
            this.date = date;
            this.duree = duree;
            this.vehicule = vehicule;
            this.client = client;
        }

        #endregion

        #region Methods

        public override bool Equals(object obj)
        {
            return obj is RendezVous ? Equals(obj as RendezVous) : false;
        }

        public bool Equals(RendezVous obj)
        {
            return Id == obj.Id;
        }


        public override string ToString()
        {
            return string.Format("{0} {1}", vehicule.Marque , vehicule.Modele);
        }

        public void addReparation(ReparationRDV reparation)
        {
            if (reparation != null)
            {
                travaux.Add(reparation);
            }
        }

        public void addReparations(List<ReparationRDV> list)
        {
            foreach (ReparationRDV r in list)
            {
                addReparation(r);
            }
        }

        public void RemoveAllWorks()
        {
            if (travaux != null)
            {
                travaux.RemoveAll(x => true);
            }
        }

        public int getDuree()
        {
            int duree = 9;
            switch (Duree)
            {
                case DureeType.UneDemiHeure:
                    duree = 0;
                    break;
                case DureeType.UneHeure:
                    duree = 1;
                    break;
                case DureeType.DeuxHeures:
                    duree = 2;
                    break;
                case DureeType.TroisHeures:
                    duree = 3;
                    break;
                case DureeType.QuatreHeures:
                    duree = 4;
                    break;
                case DureeType.CinqHeures:
                    duree = 5;
                    break;
                case DureeType.SixHeures:
                    duree = 6;
                    break;
                case DureeType.SeptHeures:
                    duree = 7;
                    break;
                case DureeType.HuitHeures:
                    duree = 8;
                    break;
            }
            return duree;
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
