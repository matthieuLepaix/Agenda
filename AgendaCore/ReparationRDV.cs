using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgendaCore
{
    public class ReparationRDV
    {

        #region Attributes

        private int id;

        private RendezVous rendezVous;

        private Reparation reparation;

        private string mReference;

        private int quantite;

        private float prixU;

        private float remise;

        private string comments;

        #endregion

        #region Properties

        /// <summary>
        /// L'identifiant
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
        /// Le rendez-vous.
        /// </summary>
        public RendezVous RendezVous
        {
            get
            {
                return rendezVous;
            }
            set
            {
                rendezVous = value;
            }
        }

        /// <summary>
        /// La réparation.
        /// </summary>
        public Reparation Reparation
        {
            get
            {
                return reparation;
            }
            set
            {
                reparation = value;
            }
        }

        /// <summary>
        /// La réfrence du produit.
        /// </summary>
        public string Reference
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
        public int Quantite
        {
            get
            {
                return quantite;
            }
            set
            {
                quantite = value;
            }
        }

        /// <summary>
        /// Le prix unitaire hors taxes
        /// </summary>
        public float PrixU
        {
            get
            {
                return prixU;
            }
            set
            {
                prixU = value;
            }
        }

        /// <summary>
        /// Le remise au client pour cette réparation
        /// </summary>
        public float Remise
        {
            get
            {
                return remise;
            }
            set
            {
                remise = value;
            }
        }

        /// <summary>
        /// L'identifiant du client.
        /// </summary>
        public string Comments
        {
            get
            {
                return comments;
            }
            set
            {
                comments = value;
            }
        }

        #endregion

        #region Constructors

        public ReparationRDV(int id, RendezVous rdv, Reparation reparation, string refe, int qte, float pu, float rem, string comments)
        {
            Id = id;
            RendezVous = rdv;
            Reparation = reparation;
            Reference = refe;
            Quantite = qte;
            PrixU = pu;
            Remise = rem;
            Comments = comments;
        }

        public ReparationRDV(RendezVous rdv, Reparation reparation, string refe, int qte, float pu, float rem, string comments)
        {
            RendezVous = rdv;
            Reparation = reparation;
            Reference = refe;
            Quantite = qte;
            PrixU = pu;
            Remise = rem;
            Comments = comments;
        }

        #endregion

    }
}
