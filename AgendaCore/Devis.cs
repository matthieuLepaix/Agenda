using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgendaCore
{
    public class Devis
    {
        #region Properties

        /// <summary>
        /// Le véhicule qui représente le client.
        /// </summary>
        public Vehicule Vehicule { get; set; }

        /// <summary>
        /// La liste des réparations à effectuer.
        /// </summary>
        public List<ReparationRDV> Reparations = new List<ReparationRDV>();

        /// <summary>
        /// Les forfaits main d'oeuvre
        /// </summary>
        public List<float> Maindoeuvres = new List<float>();

        /// <summary>
        /// Prix total HT
        /// </summary>
        public float PrixTotalHT;


        /// <summary>
        /// Prix total HT
        /// </summary>
        public float PrixTotalPieces;

        /// <summary>
        /// Type de réglement
        /// </summary>
        public Facture.Reglements Reglement;

        /// <summary>
        /// Prix total TTC
        /// </summary>
        public float PrixTotalTTC;

        #endregion

        #region Constructors

        public Devis(Vehicule v, List<ReparationRDV> r, List<float> mos, Facture.Reglements reg)
        {
            Vehicule = v;
            Reparations.AddRange(r);
            Maindoeuvres.AddRange(mos);
            Reglement = reg;
            SetPrix();
        }

        /// <summary>
        /// Permet de calculer les prix HT et TTC à partir des réparations et forfaits main d'oeuvre du devis.
        /// </summary>
        private void SetPrix()
        {
            PrixTotalPieces = 0;
            foreach(var r in Reparations)
            {
                PrixTotalPieces += r.PrixU * r.Quantite;
            }
            PrixTotalHT = PrixTotalPieces;
            foreach(var mo in Maindoeuvres)
            {
                PrixTotalHT += mo;
            }

            PrixTotalTTC = PrixTotalHT*(1+Facture.TVA);
        }

        #endregion
    }
}
