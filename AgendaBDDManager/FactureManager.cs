﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AgendaCore;
using Oracle.ManagedDataAccess.Client;

namespace AgendaBDDManager
{
    public class FactureManager
    {
        #region Attributes

        public static List<Facture> FACTURE = new List<Facture>();

        private static string maxID_request = "SELECT MAX(id) FROM facture";


        private static string insertFacture = @"INSERT INTO FACTURE (id,totalpieceht,reglement,mo1,mo2,mo3,mo4,mo5,totalht,rdv) 
                                        VALUES ({0},{1},'{2}',{3},{4},{5},{6},{7},{8},{9});";

        #endregion

        #region Properties

        #endregion

        #region Constructors

        #endregion

        #region Methods

        public static void initialize()
        {
            FACTURE.AddRange(GetAll());
        }

        public static Facture GetFactureById(int id)
        {
            Facture facture = null;
            string requete = string.Format(@"SELECT id,totalpieceht,reglement,mo1,mo2,mo3,mo4,mo5,totalht,rdv FROM facture f WHERE f.id={0}", id);
            Connexion bdd = new Connexion();
            bdd.OpenConnection();
            OracleDataReader odr = bdd.ExecuteSelect(requete);
            if (odr.Read())
            {
                List<float> mos = new List<float>();
                mos.Add(float.Parse(odr.GetDecimal(3).ToString()));
                mos.Add(float.Parse(odr.GetDecimal(4).ToString()));
                mos.Add(float.Parse(odr.GetDecimal(5).ToString()));
                mos.Add(float.Parse(odr.GetDecimal(6).ToString()));
                mos.Add(float.Parse(odr.GetDecimal(7).ToString()));
                facture = new Facture(int.Parse(odr.GetDecimal(0).ToString()),
                                    float.Parse(odr.GetDecimal(1).ToString()),
                                    odr.IsDBNull(2) ? Facture.Reglements.NA : Facture.getReglementFromString(odr.GetString(2)),
                                    mos,
                                    float.Parse(odr.GetDecimal(8).ToString()),
                                    RdvManager.RDVS.First(x => x.Id == Connexion.getIntFromOdr(9, odr))
                                    );
            }
            bdd.CloseConnection();
            return facture;
        }

        public static Facture GetFactureByRdv(int idRDV)
        {
            Facture facture = null;
            string requete = string.Format(@"SELECT id,totalpieceht,reglement,mo1,mo2,mo3,mo4,mo5,totalht,rdv FROM facture f WHERE rdv={0}", idRDV);
            Connexion bdd = new Connexion();
            bdd.OpenConnection();
            OracleDataReader odr = bdd.ExecuteSelect(requete);
            if (odr.Read())
            {
                List<float> mos = new List<float>();
                mos.Add(float.Parse(odr.GetDecimal(3).ToString()));
                mos.Add(float.Parse(odr.GetDecimal(4).ToString()));
                mos.Add(float.Parse(odr.GetDecimal(5).ToString()));
                mos.Add(float.Parse(odr.GetDecimal(6).ToString()));
                mos.Add(float.Parse(odr.GetDecimal(7).ToString()));
                facture = new Facture(int.Parse(odr.GetDecimal(0).ToString()),
                                    float.Parse(odr.GetDecimal(1).ToString()),
                                    odr.IsDBNull(2) ? Facture.Reglements.NA : Facture.getReglementFromString(odr.GetString(2)),
                                    mos,
                                    float.Parse(odr.GetDecimal(8).ToString()),
                                    RdvManager.RDVS.First(x => x.Id == Connexion.getIntFromOdr(9, odr))
                                    );
            }
            bdd.CloseConnection();
            return facture;
        }

        public static List<Facture> GetAll()
        {
            List<Facture> liste = new List<Facture>();
            string requete = string.Format("SELECT id,totalpieceht,reglement,mo1,mo2,mo3,mo4,mo5,totalht,rdv FROM facture");
            Connexion bdd = new Connexion();
            bdd.OpenConnection();
            OracleDataReader odr = bdd.ExecuteSelect(requete);
            while (odr.Read())
            {
                List<float> mos = new List<float>();
                mos.Add(float.Parse(odr.GetDecimal(3).ToString()));
                mos.Add(float.Parse(odr.GetDecimal(4).ToString()));
                mos.Add(float.Parse(odr.GetDecimal(5).ToString()));
                mos.Add(float.Parse(odr.GetDecimal(6).ToString()));
                mos.Add(float.Parse(odr.GetDecimal(7).ToString()));
                liste.Add(new Facture(int.Parse(odr.GetDecimal(0).ToString()),
                                    float.Parse(odr.GetDecimal(1).ToString()),
                                    odr.IsDBNull(2) ? Facture.Reglements.NA : Facture.getReglementFromString(odr.GetString(2)),
                                    mos,
                                    float.Parse(odr.GetDecimal(8).ToString()),
                                    RdvManager.RDVS.First(x => x.Id == Connexion.getIntFromOdr(9, odr)))
                                    );
            }
            bdd.CloseConnection();

            return liste;
        }

        public static void AddFacture(float totP, Facture.Reglements reg, List<float> mo, float tot, RendezVous rdv)
        {
            Facture c = new Facture(totP, reg, mo, tot, rdv);
            AddFacture(c);
        }

        public static void AddFacture(Facture facture)
        {

            Connexion bdd = new Connexion();
            string requete = string.Format(@"INSERT INTO facture(totalpieceht,reglement,mo1,mo2,mo3,mo4,mo5,totalht,rdv) 
                                            VALUES({0},'{1}',{2},{3},{4},{5},{6},{7},{8})",
                                                    facture.TotalPieceHT, bdd.DeleteInjectionSQL(Facture.getReglementFromEnum(facture.Reglement)),
                                                    facture.MainOeuvres.Count > 0 ? facture.MainOeuvres[0].ToString() : "0",
                                                    facture.MainOeuvres.Count > 1 ? facture.MainOeuvres[1].ToString() : "0",
                                                    facture.MainOeuvres.Count > 2 ? facture.MainOeuvres[2].ToString() : "0",
                                                    facture.MainOeuvres.Count > 3 ? facture.MainOeuvres[3].ToString() : "0",
                                                    facture.MainOeuvres.Count > 4 ? facture.MainOeuvres[4].ToString() : "0", 
                                                    facture.TotalHT, facture.RendezVous.Id);
            bdd.OpenConnection();
            bdd.ExecuteNonQuery(requete);
            OracleDataReader odr = bdd.ExecuteSelect(maxID_request);
            int maxID = 0;
            if (odr.Read())
            {
                maxID = int.Parse(odr.GetDecimal(0).ToString());
            }
            bdd.CloseConnection();
            facture.Id = maxID;
            FACTURE.Add(facture);
        }


        public static void UpdateFacture(Facture facture)
        {

            Connexion bdd = new Connexion();
            string requete = string.Format(@"UPDATE facture 
                                            SET 
                                                totalpieceht = {0},
                                                reglement = '{1}',
                                                mo1 = {2},
                                                mo2 = {3},
                                                mo3 = {4},
                                                mo4 = {5},
                                                mo5 = {6},
                                                totalht = {7},
                                                rdv = {8}
                                            WHERE 
                                                id = {9}",
                                                    facture.TotalPieceHT, bdd.DeleteInjectionSQL(Facture.getReglementFromEnum(facture.Reglement)),
                                                    facture.MainOeuvres.Count > 0 ? facture.MainOeuvres[0].ToString() : "0",
                                                    facture.MainOeuvres.Count > 1 ? facture.MainOeuvres[1].ToString() : "0",
                                                    facture.MainOeuvres.Count > 2 ? facture.MainOeuvres[2].ToString() : "0",
                                                    facture.MainOeuvres.Count > 3 ? facture.MainOeuvres[3].ToString() : "0",
                                                    facture.MainOeuvres.Count > 4 ? facture.MainOeuvres[4].ToString() : "0", 
                                                    facture.TotalHT, facture.RendezVous.Id,
                                                    facture.Id.ToString());
            bdd.OpenConnection();
            bdd.ExecuteNonQuery(requete);
            bdd.CloseConnection();
            FACTURE.Remove(FACTURE.Find(c => c.Id == facture.Id));
            FACTURE.Add(facture);

        }

        public static void DeleteFacture(Facture facture)
        {
            string requete = string.Format("DELETE facture WHERE id = {0} ", facture.Id);
            Connexion bdd = new Connexion();
            bdd.OpenConnection();
            bdd.ExecuteNonQuery(requete);
            bdd.CloseConnection();
            FACTURE.Remove(FACTURE.Find(c => c.Id == facture.Id));
        }

        public static void DeleteFactureByRDV(RendezVous rdv)
        {
            string requete = string.Format("DELETE facture WHERE rdv = {0} ", rdv.Id);
            Connexion bdd = new Connexion();
            bdd.OpenConnection();
            bdd.ExecuteNonQuery(requete);
            bdd.CloseConnection();
            FACTURE.Remove(FACTURE.Find(c => c.RendezVous == rdv));
        }

        #endregion

        public static void Sauvegarde(System.IO.StreamWriter sw)
        {
            foreach (Facture f in GetAll())
            {
                SaveFacture(sw, f);
            }
        }

        public static void SaveFacture(System.IO.StreamWriter sw, Facture facture)
        {
            sw.WriteLine(string.Format(insertFacture, facture.Id, facture.TotalPieceHT, Facture.getReglementFromEnum(facture.Reglement),
                                                    facture.MainOeuvres.Count > 0 ? facture.MainOeuvres[0].ToString() : "0",
                                                    facture.MainOeuvres.Count > 1 ? facture.MainOeuvres[1].ToString() : "0",
                                                    facture.MainOeuvres.Count > 2 ? facture.MainOeuvres[2].ToString() : "0",
                                                    facture.MainOeuvres.Count > 3 ? facture.MainOeuvres[3].ToString() : "0",
                                                    facture.MainOeuvres.Count > 4 ? facture.MainOeuvres[4].ToString() : "0",
                                                    facture.TotalHT, facture.RendezVous.Id));
        }
    }
}
