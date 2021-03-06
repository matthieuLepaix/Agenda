﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AgendaCore;
using Oracle.ManagedDataAccess.Client;


namespace AgendaBDDManager
{
    public class RdvManager
    {
        #region Attributes

        public static List<RendezVous> RDVS = new List<RendezVous>();

        private static string SELECT_MAXID_RDV = "SELECT MAX(id) FROM rendezvous";

        private static string SELECT_MAXID_REPS = "SELECT MAX(id) FROM rdv_reparation";

        private static string INSERTRDV = @"INSERT INTO rendezvous (id,date_rdv,duree,client,vehicule) VALUES ({0},to_date('{1} {2}','DD/MM/RR hh24:mi:ss'),{3},{4},{5})";

        private static string INSERTRDV_OLD = @"INSERT INTO rendezvous (id,date_rdv,duree,vehicule) VALUES ({0},to_date('{1} {2}','DD/MM/RR hh24:mi:ss'),{3},{4})";


        #endregion

        #region Properties

        #endregion

        #region Constructors

        #endregion

        #region Methods

        public static void Initialize()
        {
            RDVS.Clear();
            RDVS.AddRange(GetAll());
        }

        public static List<ReparationRDV> GetAllForOldVersion()
        {
            List<ReparationRDV> liste = new List<ReparationRDV>();

            string requete = string.Format(@"SELECT r.rendez_vous_id, r.date_rdv, r.duree, r.travaux,
                                            c.client_id, c.nom, c.prenom, c.tel1, c.tel2, 
                                            v.vehicule_id, v.marque, v.modele, v.immatriculation
                                            FROM rendez_vous r, client c, vehicule v
                                            WHERE r.vehicule_id = v.vehicule_id AND v.client_id = c.client_id");
            Connexion bdd = new Connexion();
            bdd.OpenConnection();
            OracleDataReader odr = bdd.ExecuteSelect(requete);
            while (odr.Read())
            {
                Client c = new Client(int.Parse(odr.GetDecimal(4).ToString()), odr.GetString(5), odr.IsDBNull(6) ? "" : odr.GetString(6), odr.GetString(7), odr.IsDBNull(8) ? "" : odr.GetString(8),"","","","");
                Vehicule v = new Vehicule(int.Parse(odr.GetDecimal(9).ToString()), odr.GetString(10), odr.GetString(11), odr.GetString(12), "XXXX", 0, c);
                RendezVous rdv = new RendezVous(int.Parse(odr.GetDecimal(0).ToString()), odr.GetDateTime(1), (DureeType)Enum.Parse(DureeType.UneHeure.GetType(), odr.GetDecimal(2).ToString()), v, c);
                ReparationRDV rep = new ReparationRDV(rdv, new Reparation(254,"DIVERS"),"", 0, 0, 0, odr.GetString(3));
                liste.Add(rep);
            }
            bdd.CloseConnection();

            return liste;
        }

        public static List<RendezVous> GetAll()
        {
            List<RendezVous> liste = new List<RendezVous>();

            string requete = string.Format(@"SELECT r.id, r.date_rdv, r.duree, r.vehicule, r.client
                                            FROM rendezvous r");
            Connexion bdd = new Connexion();
            bdd.OpenConnection();
            OracleDataReader odr = bdd.ExecuteSelect(requete);
            while (odr.Read())
            {
                Vehicule v = VehiculeManager.VEHICULES.Find(x => x.Id == Connexion.getIntFromOdr(3, odr));
                RendezVous rdv = new RendezVous(Connexion.getIntFromOdr(0, odr), 
                                            odr.GetDateTime(1), 
                                            (DureeType)Enum.Parse(DureeType.UneHeure.GetType(), Connexion.getIntFromOdr(2, odr).ToString()), 
                                            v,
                                            v != null ? v.Client : null);
                liste.Add(rdv);
            }
            bdd.CloseConnection();
            return liste;
        }



        public static RendezVous GetRDVById(int id)
        {
            RendezVous rdv = null;
            string requete = string.Format(@"SELECT r.id, r.date_rdv, r.duree,
                                            c.id, c.nom, c.prenom, c.telephone1, c.telephone2, c.email, c.adresse, c.codePostal, c.ville,
                                            v.id, v.marque, v.modele, v.immatriculation, v.annee, v.kilometrage
                                            FROM rendezvous r, client c, vehicule v
                                            WHERE r.vehicule = v.id AND c.id = r.client AND r.id={0}", id);
            Connexion bdd = new Connexion();
            bdd.OpenConnection();
            OracleDataReader odr = bdd.ExecuteSelect(requete);
            if (odr.Read())
            {
                Client c = new Client(Connexion.getIntFromOdr(3, odr),
                                        Connexion.getStringFromOdr(4, odr),
                                        Connexion.getStringFromOdr(5, odr),
                                        Connexion.getStringFromOdr(6, odr),
                                        Connexion.getStringFromOdr(7, odr),
                                        Connexion.getStringFromOdr(8, odr),
                                        Connexion.getStringFromOdr(9, odr),
                                        Connexion.getStringFromOdr(10, odr),
                                        Connexion.getStringFromOdr(11, odr));
                Vehicule v = new Vehicule(Connexion.getIntFromOdr(12, odr),
                                        Connexion.getStringFromOdr(13, odr),
                                        Connexion.getStringFromOdr(14, odr),
                                        Connexion.getStringFromOdr(15, odr),
                                        Connexion.getStringFromOdr(16, odr),
                                        int.Parse(Connexion.getStringFromOdr(17, odr)),
                                        c);
                rdv = new RendezVous(Connexion.getIntFromOdr(0, odr),
                                            odr.GetDateTime(1),
                                            (DureeType)Enum.Parse(DureeType.UneHeure.GetType(), odr.GetDecimal(2).ToString()),
                                            v, c);
                rdv.addReparations(ReparationRDVManager.getReparationRDVByRDV(rdv));
            }
            bdd.CloseConnection();

            rdv.Client.AddVehicules(VehiculeManager.GetVehiculesByClient(rdv.Client));

            return rdv;
        }

        public static List<RendezVous> GetRDVFromDateToDate(DateTime from, DateTime to)
        {
            List<RendezVous> liste = new List<RendezVous>();
            string requete = string.Format(@"SELECT r.id, r.date_rdv, r.duree,
                                            c.id, c.nom, c.prenom, c.telephone1, c.telephone2, c.email, c.adresse, c.codePostal, c.ville,
                                            v.id, v.marque, v.modele, v.immatriculation, v.annee, v.kilometrage
                                            FROM rendezvous r, client c, vehicule v
                                            WHERE r.vehicule = v.id AND c.id = r.client
                                                AND r.date_rdv > TO_DATE('{0}','DD/MM/YYYY HH24:MI:SS') 
                                                AND r.date_rdv < TO_DATE('{1}','DD/MM/YYYY HH24:MI:SS')", from, to);
            Connexion bdd = new Connexion();
            bdd.OpenConnection();
            OracleDataReader odr = bdd.ExecuteSelect(requete);
            while (odr.Read())
            {
                Client c = new Client(Connexion.getIntFromOdr(3, odr),
                                        Connexion.getStringFromOdr(4, odr),
                                        Connexion.getStringFromOdr(5, odr),
                                        Connexion.getStringFromOdr(6, odr),
                                        Connexion.getStringFromOdr(7, odr),
                                        Connexion.getStringFromOdr(8, odr),
                                        Connexion.getStringFromOdr(9, odr),
                                        Connexion.getStringFromOdr(10, odr),
                                        Connexion.getStringFromOdr(11, odr));
                Vehicule v = new Vehicule(Connexion.getIntFromOdr(12, odr),
                                        Connexion.getStringFromOdr(13, odr),
                                        Connexion.getStringFromOdr(14, odr),
                                        Connexion.getStringFromOdr(15, odr),
                                        Connexion.getStringFromOdr(16, odr),
                                        int.Parse(Connexion.getStringFromOdr(17, odr)),
                                        c);
                RendezVous rdv = new RendezVous(Connexion.getIntFromOdr(0, odr),
                                            odr.GetDateTime(1),
                                            (DureeType)Enum.Parse(DureeType.UneHeure.GetType(), odr.GetDecimal(2).ToString()),
                                            v, c);
                rdv.addReparations(ReparationRDVManager.getReparationRDVByRDV(rdv));
                liste.Add(rdv);
            }
            bdd.CloseConnection();

            liste.ForEach(rdv => rdv.Client.AddVehicules(VehiculeManager.GetVehiculesByClient(rdv.Client)));
            return liste;
        }

        public static List<RendezVous> GetTravauxByImmat(string immat)
        {
            List<RendezVous> liste = new List<RendezVous>();
            string requete = string.Format(@"SELECT r.id, r.date_rdv, r.duree,
                                            c.id, c.nom, c.prenom, c.telephone1, c.telephone2, c.email, c.adresse, c.codePostal, c.ville,
                                            v.id, v.marque, v.modele, v.immatriculation, v.annee, v.kilometrage
                                            FROM rendezvous r, client c, vehicule v
                                            WHERE r.vehicule = v.id AND c.id = r.client
                                                AND vehicule IN(SELECT id FROM vehicule where LOWER(immatriculation) = '{0}')
                                            ORDER BY r.date_rdv", immat.ToLower());
            Connexion bdd = new Connexion();
            bdd.OpenConnection();
            OracleDataReader odr = bdd.ExecuteSelect(requete);
            while (odr.Read())
            {
                Client c = new Client(Connexion.getIntFromOdr(3, odr),
                                        Connexion.getStringFromOdr(4, odr),
                                        Connexion.getStringFromOdr(5, odr),
                                        Connexion.getStringFromOdr(6, odr),
                                        Connexion.getStringFromOdr(7, odr),
                                        Connexion.getStringFromOdr(8, odr),
                                        Connexion.getStringFromOdr(9, odr),
                                        Connexion.getStringFromOdr(10, odr),
                                        Connexion.getStringFromOdr(11, odr));
                Vehicule v = new Vehicule(Connexion.getIntFromOdr(12, odr),
                                        Connexion.getStringFromOdr(13, odr),
                                        Connexion.getStringFromOdr(14, odr),
                                        Connexion.getStringFromOdr(15, odr),
                                        Connexion.getStringFromOdr(16, odr),
                                        int.Parse(Connexion.getStringFromOdr(17, odr)),
                                        c);
                RendezVous rdv = new RendezVous(Connexion.getIntFromOdr(0, odr),
                                            odr.GetDateTime(1),
                                            (DureeType)Enum.Parse(DureeType.UneHeure.GetType(), odr.GetDecimal(2).ToString()),
                                            v, c);
                rdv.addReparations(ReparationRDVManager.getReparationRDVByRDV(rdv));
                liste.Add(rdv);
            }
            bdd.CloseConnection();

            liste.ForEach(rdv => rdv.Client.AddVehicules(VehiculeManager.GetVehiculesByClient(rdv.Client)));
            return liste;
        }


        public static List<RendezVous> GetTravauxByClient(int clientID)
        {
            List<RendezVous> liste = new List<RendezVous>();

            string requete = string.Format(@"SELECT r.id, r.date_rdv, r.duree,
                                            c.id, c.nom, c.prenom, c.telephone1, c.telephone2, c.email, c.adresse, c.codePostal, c.ville,
                                            v.id, v.marque, v.modele, v.immatriculation, v.annee, v.kilometrage
                                            FROM rendezvous r, client c, vehicule v
                                            WHERE r.vehicule = v.id AND c.id = r.client
                                                AND c.id={0}
                                            ORDER BY r.date_rdv", clientID);
            Connexion bdd = new Connexion();
            bdd.OpenConnection();
            OracleDataReader odr = bdd.ExecuteSelect(requete);
            while (odr.Read())
            {
                Client c = new Client(Connexion.getIntFromOdr(3, odr),
                                        Connexion.getStringFromOdr(4, odr),
                                        Connexion.getStringFromOdr(5, odr),
                                        Connexion.getStringFromOdr(6, odr),
                                        Connexion.getStringFromOdr(7, odr),
                                        Connexion.getStringFromOdr(8, odr),
                                        Connexion.getStringFromOdr(9, odr),
                                        Connexion.getStringFromOdr(10, odr),
                                        Connexion.getStringFromOdr(11, odr));

                Vehicule v = new Vehicule(Connexion.getIntFromOdr(12, odr),
                                        Connexion.getStringFromOdr(13, odr),
                                        Connexion.getStringFromOdr(14, odr),
                                        Connexion.getStringFromOdr(15, odr),
                                        Connexion.getStringFromOdr(16, odr),
                                        int.Parse(Connexion.getStringFromOdr(17, odr)),
                                        c);

                RendezVous rdv = new RendezVous(Connexion.getIntFromOdr(0, odr),
                                            odr.GetDateTime(1),
                                            (DureeType)Enum.Parse(DureeType.UneHeure.GetType(), odr.GetDecimal(2).ToString()),
                                            v, c);

                rdv.addReparations(ReparationRDVManager.getReparationRDVByRDV(rdv));
                liste.Add(rdv);
            }
            bdd.CloseConnection();

            liste.ForEach(rdv => rdv.Client.AddVehicules(VehiculeManager.GetVehiculesByClient(rdv.Client)));

            return liste;
        }

        public static int CountRdvDay(DateTime d)
        {
            int nb = 0;
            string short_date = d.ToShortDateString();
            string year = short_date.Substring(short_date.Length - 2);
            string date = string.Format("{0}/{1}/{2}", d.Day.ToString("D2"), d.Month.ToString("D2"), year);
            string requete = string.Format("SELECT COUNT(1) FROM rendezvous WHERE date_rdv LIKE '{0}'", date);

            Connexion bdd = new Connexion();
            bdd.OpenConnection();
            OracleDataReader odr = bdd.ExecuteSelect(requete);

            if (odr.Read())
            {
                nb = (int)odr.GetDecimal(0);
            }

            bdd.CloseConnection();

            return nb;
        }

        public static void AddRdv(RendezVous rdv)
        {
            Connexion bdd = new Connexion();
            OracleDataReader odr;
            bdd.OpenConnection();
            // Si le client n'existe pas en bdd
            if (rdv.Vehicule.Client.Id == -1)
            {
                ClientManager.AddClient(rdv.Vehicule.Client);
            }

            // Si le véhicule n'existe pas en bdd
            if (rdv.Vehicule.Id == -1)
            {
                VehiculeManager.AddVehicule(rdv.Vehicule);
                string requeteVehicule = string.Format("SELECT id FROM vehicule WHERE LOWER(immatriculation) = '{0}'",
                                                            bdd.DeleteInjectionSQL(rdv.Vehicule.Immatriculation.ToLower()));
                odr = bdd.ExecuteSelect(requeteVehicule);

                if (odr.Read())
                {
                    rdv.Vehicule.Id = int.Parse(odr.GetDecimal(0).ToString());
                }
            }

            odr = bdd.ExecuteSelect(SELECT_MAXID_RDV);
            int maxID = 0;
            if (odr.Read())
            {
                maxID = int.Parse(odr.GetDecimal(0).ToString());
            }
            rdv.Id = maxID + 1;

            string requete = string.Format(INSERTRDV, rdv.Id, rdv.Date.ToShortDateString(), rdv.Date.ToShortTimeString(), getEntierForDuree(rdv.Duree), rdv.Client.Id, rdv.Vehicule.Id);
            bdd.ExecuteNonQuery(requete);
            
            foreach(ReparationRDV rep in rdv.Travaux)
            {
                rep.RendezVous = rdv;
                ReparationRDVManager.AddReparationRDV(rep);
            }

            bdd.CloseConnection();
            RDVS.Add(rdv);
        }

        private static int getEntierForDuree(DureeType dureeType)
        {
            int i = 9;
            switch (dureeType)
            {
                case DureeType.UneDemiHeure:
                    i = 0;
                    break;
                case DureeType.UneHeure:
                    i = 1;
                    break;
                case DureeType.DeuxHeures:
                    i = 2;
                    break;
                case DureeType.TroisHeures:
                    i = 3;
                    break;
                case DureeType.QuatreHeures:
                    i = 4;
                    break;
                case DureeType.CinqHeures:
                    i = 5;
                    break;
                case DureeType.SixHeures:
                    i = 6;
                    break;
                case DureeType.SeptHeures:
                    i = 7;
                    break;
                case DureeType.HuitHeures:
                    i = 8;
                    break;
                case DureeType.EtPlus:
                    i = 9;
                    break;
            }
            return i;
        }

        public static void UpdateRdv(RendezVous rdv)
        {
            Connexion bdd = new Connexion();
            bdd.OpenConnection();
            
            string requete = string.Format(@"UPDATE rendezvous 
                                                SET 
                                                    date_rdv = to_date('{0} {1}','dd/mm/yyyy hh24:mi:ss'), 
                                                    duree = {2}, vehicule = {3}, client = {4} 
                                                WHERE id = {5}",
                rdv.Date.ToShortDateString(), rdv.Date.ToShortTimeString(), getEntierForDuree(rdv.Duree), 
                rdv.Vehicule != null ? rdv.Vehicule.Id.ToString() : "null", 
                rdv.Client != null ?rdv.Client.Id.ToString() : "null", 
                rdv.Id);
            bdd.ExecuteNonQuery(requete);

            ReparationRDVManager.DeleteReparationRDVByRDV(rdv);
            foreach (ReparationRDV rep in rdv.Travaux)
            {
                ReparationRDVManager.AddReparationRDV(rep);
            }

            bdd.CloseConnection();

            RDVS.Remove(RDVS.Find(r => r.Id == rdv.Id));
            RDVS.Add(rdv);
        }

        public static void DeleteRdv(int id)
        {
            Connexion bdd = new Connexion();
            bdd.OpenConnection();

            string requete = string.Format("DELETE rendezvous WHERE id = {0}", id);
            bdd.ExecuteNonQuery(requete);

            bdd.CloseConnection();
            RDVS.Remove(RDVS.Find(r => r.Id == id));
        }

        #endregion



        public static void Sauvegarde(System.IO.StreamWriter sw)
        {
            foreach (RendezVous r in GetAll())
            {
                string date = string.Format("{0:00}/{1:00}/{2}", r.Date.Day, r.Date.Month, r.Date.Year);
                string time = string.Format("{0:00}:{1:00}:00", r.Date.Hour, r.Date.Minute);
                sw.WriteLine(INSERTRDV+";", r.Id, date, time, r.getDuree(), r.Client != null ? r.Client.Id.ToString() : "null", r.Vehicule.Id);
            }
        }

        internal static void SaveRDV(System.IO.StreamWriter sw, RendezVous rdv)
        {
            string date = string.Format("{0:00}/{1:00}/{2}", rdv.Date.Day, rdv.Date.Month, rdv.Date.Year);
            string time = string.Format("{0:00}:{1:00}:00", rdv.Date.Hour, rdv.Date.Minute);
            sw.WriteLine(INSERTRDV + ";", rdv.Id, date, time, getEntierForDuree(rdv.Duree), rdv.Client != null ? rdv.Client.Id.ToString() : "null", rdv.Vehicule.Id);
        }
    }
}
