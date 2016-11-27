using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgendaCore
{
    public class Facture
    {
        #region Attributes  

        public static float TVA = 0.20f;

        public enum Reglement{
            Especes,
            CB,
            Cheque
        };

        private int mId;
        
        private float mTotalPieceHT;

        private string mReglement;

        private List<float> mMainOeuvres;

        private float mTotalHT;

        private RendezVous mRdv;
        
        #endregion

        #region Properties

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
        /// Le total HT des pièces
        /// </summary>
        public float pTotalPieceHT
        {
            get
            {
                return mTotalPieceHT;
            }
            set
            {
                mTotalPieceHT = value;
            }
        }

        /// <summary>
        /// Le mode de réglement
        /// </summary>
        public string pReglement
        {
            get
            {
                return mReglement;
            }
            set
            {
                mReglement = value;
            }
        }

        /// <summary>
        /// Le total HT des pièces
        /// </summary>
        public float pTotalHT
        {
            get
            {
                return mTotalHT;
            }
            set
            {
                mTotalHT = value;
            }
        }

        /// <summary>
        /// Le total HT des pièces
        /// </summary>
        public List<float> pMainOeuvres
        {
            get
            {
                return mMainOeuvres;
            }
            set
            {
                mMainOeuvres = value;
            }
        }

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

        #endregion

        #region constructors

        public Facture(int id, float totP, string reg, List<float> mo, float tot, RendezVous rdv)
        {
            pId = id;
            pTotalPieceHT = totP;
            pReglement = reg;
            pMainOeuvres = mo;
            pTotalHT = tot;
            pRdv = rdv;
        }

        public Facture(float totP, string reg, List<float> mo, float tot, RendezVous rdv)
        {
            pTotalPieceHT = totP;
            pReglement = reg;
            pMainOeuvres = mo;
            pTotalHT = tot;
            pRdv = rdv;
        }

        #endregion

        #region Methods

        private bool Equals(Facture bill)
        {
            return pId == bill.pId;
        }

        public override bool Equals(object obj)
        {
            bool ok = false;
            if (obj is Facture)
            {
                ok = Equals(obj as Facture);
            }
            return ok;
        }

        public string getReglementFromEnum(Reglement reg)
        {
            string reglement = "";

            switch (reg)
            {
                case Reglement.Especes:
                    reglement = "Espèces";
                    break;
                case Reglement.CB:
                    reglement = "Carte bancaire";
                    break;
                case Reglement.Cheque:
                    reglement = "Chèque";
                    break;
                default:
                    reglement = "Non spécifié";
                    break;
            }

            return reglement;
        }

        public override string ToString()
        {
            return pId.ToString();
        }


        #endregion
    }
}
