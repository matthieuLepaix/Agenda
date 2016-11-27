using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgendaCore
{
    public class ReparationRDV
    {

        #region Attributes

        private int mId;

        private RendezVous mRdv;

        private Reparation mReparation;

        private string mReference;

        private int mQuantite;

        private float mPrixU;

        private float mRemise;

        private string mComments;

        #endregion

        #region Properties

        /// <summary>
        /// L'identifiant
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
        /// Le rendez-vous.
        /// </summary>
        public RendezVous pRdv
        {
            get
            {
                return mRdv;
            }
            set
            {
                mRdv = value;
            }
        }

        /// <summary>
        /// La réparation.
        /// </summary>
        public Reparation pReparation
        {
            get
            {
                return mReparation;
            }
            set
            {
                mReparation = value;
            }
        }

        /// <summary>
        /// La réfrence du produit.
        /// </summary>
        public string pReference
        {
            get
            {
                return mReference;
            }
            set
            {
                mReference = value;
            }
        }

        /// <summary>
        /// La quantité.
        /// </summary>
        public int pQuantite
        {
            get
            {
                return mQuantite;
            }
            set
            {
                mQuantite = value;
            }
        }

        /// <summary>
        /// Le prix unitaire hors taxes
        /// </summary>
        public float pPrixU
        {
            get
            {
                return mPrixU;
            }
            set
            {
                mPrixU = value;
            }
        }

        /// <summary>
        /// Le remise au client pour cette réparation
        /// </summary>
        public float pRemise
        {
            get
            {
                return mRemise;
            }
            set
            {
                mRemise = value;
            }
        }

        /// <summary>
        /// L'identifiant du client.
        /// </summary>
        public string pComments
        {
            get
            {
                return mComments;
            }
            set
            {
                mComments = value;
            }
        }

        #endregion

        #region Constructors

        public ReparationRDV(int id, RendezVous rdv, Reparation reparation, string refe, int qte, float pu, float rem, string comments)
        {
            pId = id;
            pRdv = rdv;
            pReparation = reparation;
            pReference = refe;
            pQuantite = qte;
            pPrixU = pu;
            pRemise = rem;
            pComments = comments;
        }

        public ReparationRDV(RendezVous rdv, Reparation reparation, string refe, int qte, float pu, float rem, string comments)
        {
            pRdv = rdv;
            pReparation = reparation;
            pReference = refe;
            pQuantite = qte;
            pPrixU = pu;
            pRemise = rem;
            pComments = comments;
        }

        #endregion

    }
}
