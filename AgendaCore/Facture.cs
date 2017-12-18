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

        public enum Reglements
        {
            CB,
            Cheque,
            Especes,
            NA
        };

        private int id;
        
        private float totalPieceHT;

        private Reglements reglement;

        private List<float> mainOeuvres;

        private float totalHT;

        private RendezVous rendezVous;
        
        #endregion

        #region Properties

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
        /// Le total HT des pièces
        /// </summary>
        public float TotalPieceHT
        {
            get
            {
                return totalPieceHT;
            }
            set
            {
                totalPieceHT = value;
            }
        }

        /// <summary>
        /// Le mode de réglement
        /// </summary>
        public Reglements Reglement
        {
            get
            {
                return reglement;
            }
            set
            {
                reglement = value;
            }
        }

        /// <summary>
        /// Le total HT des pièces
        /// </summary>
        public float TotalHT
        {
            get
            {
                return totalHT;
            }
            set
            {
                totalHT = value;
            }
        }

        /// <summary>
        /// Le total HT des pièces
        /// </summary>
        public List<float> MainOeuvres
        {
            get
            {
                return mainOeuvres;
            }
            set
            {
                mainOeuvres = value;
            }
        }

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

        #endregion

        #region constructors

        public Facture(int id, float totP, Reglements reg, List<float> mo, float tot, RendezVous rdv)
        {
            Id = id;
            TotalPieceHT = totP;
            Reglement = reg;
            MainOeuvres = mo;
            TotalHT = tot;
            RendezVous = rdv;
        }

        public Facture(float totP, Reglements reg, List<float> mo, float tot, RendezVous rdv)
        {
            TotalPieceHT = totP;
            Reglement = reg;
            MainOeuvres = mo;
            TotalHT = tot;
            RendezVous = rdv;
        }

        #endregion

        #region Methods

        private bool Equals(Facture bill)
        {
            return Id == bill.Id;
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

        public static string getReglementFromEnum(Reglements reg)
        {
            string reglement = "";

            switch (reg)
            {
                case Reglements.Especes:
                    reglement = "Espèces";
                    break;
                case Reglements.CB:
                    reglement = "Carte bancaire";
                    break;
                case Reglements.Cheque:
                    reglement = "Chèque";
                    break;
                default:
                    reglement = "Non spécifié";
                    break;
            }

            return reglement;
        }

        public static Reglements getReglementFromString(string reg)
        {
            Reglements reglement;
            switch (reg)
            {
                case "Espèces":
                    reglement = Reglements.Especes;
                    break;
                case "Carte bancaire":
                    reglement = Reglements.CB;
                    break;
                case "Chèque":
                    reglement = Reglements.Cheque;
                    break;
                default:
                    reglement = Reglements.NA;
                    break;
            }

            return reglement;
        }

        public override string ToString()
        {
            return Id.ToString();
        }


        #endregion

        public override int GetHashCode()
        {
            return Id;
        }
    }
}
