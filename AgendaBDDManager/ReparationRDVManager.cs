using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AgendaCore;
using Oracle.ManagedDataAccess.Client;

namespace AgendaBDDManager
{
    public class ReparationRDVManager
    {
        #region Attributes

        public static List<ReparationRDV> REPARATIONRDVS = new List<ReparationRDV>();

        private static string maxID_request = "SELECT MAX(id) FROM rdv_reparation";

        public static string insertReparationRDV = @"INSERT INTO rdv_reparation(id,rdv,reparation, reference,quantite,prixu, remise,comments) VALUES({0},{1},{2},'{3}',{4}, {5},{6},'{7}');";
        
        public static string insertReparationRDV_SANSID = @"INSERT INTO rdv_reparation(rdv,reparation, reference,quantite,prixu, remise,comments) VALUES({0},{1},'{2}',{3},{4},{5},'{6}');";

        #endregion

        #region Methods

        public static void initialize()
        {
            REPARATIONRDVS.Clear();
            REPARATIONRDVS.AddRange(getAll());
        }

        public static List<ReparationRDV> getAll()
        {
            List<ReparationRDV> reps = new List<ReparationRDV>();
            string requete = string.Format("SELECT id, rdv, reparation, reference, quantite, prixu, remise, comments FROM rdv_reparation");
            Connexion bdd = new Connexion();
            bdd.OpenConnection();
            OracleDataReader odr = bdd.ExecuteSelect(requete);
            while (odr.Read())
            {
                RendezVous rdv = RdvManager.RDVS.First(x => x.Id == Connexion.getIntFromOdr(1, odr));
                ReparationRDV rep = new ReparationRDV(Connexion.getIntFromOdr(0, odr),
                                        RdvManager.RDVS.First(x => x.Id == Connexion.getIntFromOdr(1, odr)),
                                        ReparationManager.REPARATIONS.First(x => x.Id == Connexion.getIntFromOdr(2, odr)),
                                        Connexion.getStringFromOdr(3, odr),
                                        Connexion.getIntFromOdr(4, odr),
                                        Connexion.getIntFromOdr(5, odr),
                                        Connexion.getIntFromOdr(6, odr),
                                        Connexion.getStringFromOdr(7, odr));
                rdv.addReparation(rep);
                reps.Add(rep);
            }
            bdd.CloseConnection();
            return reps;
        }

        public static ReparationRDV getReparationRDVById(int id)
        {
            ReparationRDV rep = null;
            string requete = string.Format("SELECT id, rdv, reparation, reference, quantite, prixu, remise, comments FROM rdv_reparation WHERE id = {0}", id.ToString());
            Connexion bdd = new Connexion();
            bdd.OpenConnection();
            OracleDataReader odr = bdd.ExecuteSelect(requete);
            if (odr.Read())
            {
                rep = new ReparationRDV(Connexion.getIntFromOdr(0, odr),
                                        RdvManager.getRDVById(Connexion.getIntFromOdr(1, odr)),
                                        ReparationManager.getReparationById(Connexion.getIntFromOdr(2, odr)),
                                        Connexion.getStringFromOdr(3, odr),
                                        Connexion.getIntFromOdr(4, odr),
                                        Connexion.getIntFromOdr(5, odr),
                                        Connexion.getIntFromOdr(6, odr),
                                        Connexion.getStringFromOdr(7, odr));
            }
            bdd.CloseConnection();

            return rep;
        }

        public static List<ReparationRDV> getReparationRDVByRDV(RendezVous rdv)
        {
            List<ReparationRDV> reps = new List<ReparationRDV>();
            string requete = string.Format("SELECT id, rdv, reparation, reference, quantite, prixu, remise, comments FROM rdv_reparation WHERE rdv = {0}", rdv.Id.ToString());
            Connexion bdd = new Connexion();
            bdd.OpenConnection();
            OracleDataReader odr = bdd.ExecuteSelect(requete);
            while (odr.Read())
            {
                reps.Add(new ReparationRDV(Connexion.getIntFromOdr(0, odr),
                                        rdv,
                                        ReparationManager.getReparationById(Connexion.getIntFromOdr(2, odr)),
                                        Connexion.getStringFromOdr(3, odr),
                                        Connexion.getIntFromOdr(4, odr),
                                        Connexion.getIntFromOdr(5, odr),
                                        Connexion.getIntFromOdr(6, odr),
                                        Connexion.getStringFromOdr(7, odr)));
            }
            bdd.CloseConnection();
            return reps;
        }

        public static void AddReparationRDV(ReparationRDV rep)
        {
            Connexion bdd = new Connexion();
            string requete = string.Format("INSERT INTO rdv_reparation(rdv, reparation, reference, quantite, prixu, remise, comments) VALUES({0}, {1},'{2}',{3},{4},{5},'{6}')",
                                                    rep.RendezVous.Id, rep.Reparation.Id, rep.Reference, rep.Quantite, rep.PrixU, rep.Remise, bdd.DeleteInjectionSQL(rep.Comments));
            bdd.OpenConnection();
            bdd.ExecuteNonQuery(requete);
            bdd.CloseConnection();
        }


        public static void UpdateReparationRDV(ReparationRDV r)
        {
            Connexion bdd = new Connexion();
            string requete = string.Format(@"UPDATE rdv_reparation 
                                            SET 
                                                rdv = {0},
                                                reparation = {1}, 
                                                reference = {2},
                                                quantite = {3},
                                                prixu = {4},   
                                                remise = {5} 
                                                comments = '{6}'
                                            WHERE 
                                                id = {7}",
                                                    r.RendezVous.Id,
                                                    r.Reparation.Id,
                                                    r.Reference,
                                                    r.Quantite,
                                                    r.PrixU,
                                                    r.Remise,
                                                    bdd.DeleteInjectionSQL(r.Comments),
                                                    r.Id.ToString());
            bdd.OpenConnection();
            bdd.ExecuteNonQuery(requete);
            bdd.CloseConnection();
        }

        public static void DeleteReparationRDV(ReparationRDV r)
        {
            string requete = string.Format("DELETE rdv_reparation WHERE id = {0} ", r.Id);
            Connexion bdd = new Connexion();
            bdd.OpenConnection();
            bdd.ExecuteNonQuery(requete);
            bdd.CloseConnection();
        }

        public static void DeleteReparationRDVByRDV(RendezVous r)
        {
            string requete = string.Format("DELETE rdv_reparation WHERE rdv = {0} ", r.Id);
            Connexion bdd = new Connexion();
            bdd.OpenConnection();
            bdd.ExecuteNonQuery(requete);
            bdd.CloseConnection();
            
        }

        #endregion

        internal static void Sauvegarde(System.IO.StreamWriter sw)
        {
            foreach (ReparationRDV rep in getAll())
            {
                sw.WriteLine(insertReparationRDV, rep.Id, rep.RendezVous.Id, rep.Reparation.Id, rep.Reference, rep.Quantite, rep.PrixU, rep.Remise, rep.Comments);
            }
        }

        internal static void SaveReparation(System.IO.StreamWriter sw, ReparationRDV rep)
        {
            sw.WriteLine(insertReparationRDV_SANSID, rep.RendezVous.Id, rep.Reparation.Id, rep.Reference, rep.Quantite, rep.PrixU, rep.Remise, rep.Comments);
        }
    }
}
