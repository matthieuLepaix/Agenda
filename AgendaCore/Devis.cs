using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgendaCore
{
    public class Devis
    {
        #region Attributes

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
        public Facture.Reglement Reglement;

        /// <summary>
        /// Prix total TTC
        /// </summary>
        public float PrixTotalTTC;

        #endregion

        #region Constructors

        public Devis(Vehicule v, List<ReparationRDV> r, List<float> mos, Facture.Reglement reg)
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
                PrixTotalPieces += r.pPrixU * r.pQuantite;
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
