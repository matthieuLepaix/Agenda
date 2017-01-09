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
        private int mId;

        /// <summary>
        /// La date du durée.
        /// </summary>
        private DateTime mDate;

        /// <summary>
        /// La durée des travaux à effectuer.
        /// </summary>
        private DureeType mDuree;

        /// <summary>
        /// Les travaux à effectuer.
        /// </summary>
        private List<ReparationRDV> mTravaux = new List<ReparationRDV>();

        private Vehicule mVehicule;

        private Client mClient;

        #endregion

        #region Properties

        /// <summary>
        /// L'identifiant du rendez-vous.
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

        public DateTime pDate
        {
            get
            {
                return mDate;
            }
            set
            {
                mDate = value;
            }
        }

        public DureeType pDuree
        {
            get
            {
                return mDuree;
            }
            set
            {
                mDuree = value;
            }
        }

        public IEnumerable<ReparationRDV> pTravaux
        {
            get
            {
                return mTravaux;
            }
            set
            {
                mTravaux = value.ToList();
            }
        }

        public Vehicule pVehicule
        {
            get
            {
                return mVehicule;
            }
            set
            {
                mVehicule = value;
            }
        }

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

        #endregion

        #region Constructors

        public RendezVous(int id, DateTime date, DureeType duree, Vehicule vehicule, Client client)
        {
            mId = id;
            mDate = date;
            mDuree = duree;
            mVehicule = vehicule;
            mClient = client;
        }

        public RendezVous(DateTime date, DureeType duree, Vehicule vehicule, Client client)
        {
            mId = -1;
            mDate = date;
            mDuree = duree;
            mVehicule = vehicule;
            mClient = client;
        }

        #endregion

        #region Methods

        public override bool Equals(object obj)
        {
            return obj is RendezVous ? Equals(obj as RendezVous) : false;
        }

        public bool Equals(RendezVous obj)
        {
            return pId == obj.pId;
        }


        public override string ToString()
        {
            return string.Format("{0} {1}", mVehicule.pMarque , mVehicule.pModele);
        }

        public void addReparation(ReparationRDV reparation)
        {
            if (reparation != null)
            {
                mTravaux.Add(reparation);
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
            if (mTravaux != null)
            {
                mTravaux.RemoveAll(x => true);
            }
        }

        public int getDuree()
        {
            int duree = 9;
            switch (pDuree)
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
            return pId;
        }
        #endregion



    }
}
