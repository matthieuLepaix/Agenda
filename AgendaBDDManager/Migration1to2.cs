using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using AgendaCore;

namespace AgendaBDDManager
{
    public class Migration1to2
    {
        private static void MigrateClient(StreamWriter sw)
        {
            foreach (Client c in ClientManager.getAllforOldVersion())
            {
                ClientManager.SaveClient(sw, c);
                //VehiculeManager.saveVehicule(sw, c.pVehicules.ToList());
            }
        }

        private static void MigrateReparation(StreamWriter sw, int rdv, int vehicle, string travaux)
        {

        }

        private static void MigrateVehicule(StreamWriter sw)
        {
            VehiculeManager.saveVehicule(sw, VehiculeManager.getAllForOldVersion());
        }

        private static void MigrateRDV(StreamWriter sw)
        {
            List<ReparationRDV> reps = RdvManager.getAllForOldVersion();
            foreach (ReparationRDV rep in reps)
            {
                RdvManager.SaveRDV(sw, rep.pRdv);
            }
            foreach (ReparationRDV rep in reps)
            {
                ReparationRDVManager.SaveReparation(sw, rep);
            }
        }

        public static void doMigration()
        {
            DateTime today = DateTime.Now;
            string nomFichier = string.Format("C:\\Users\\Matthieu\\Documents\\ISIMA\\Agenda\\BDD\\{0}_Sauvegarde_RendezVous.txt", today.Ticks);
            FileStream fichier = File.Create(nomFichier);
            StreamWriter sw = new StreamWriter(fichier);
            sw.WriteLine("delete from rendezvous;");
            sw.WriteLine("delete from vehicule;");
            sw.WriteLine("delete from client;");
            sw.WriteLine("alter trigger tr_client disable;");
            sw.WriteLine("alter trigger tr_vehicule disable;");
            sw.WriteLine("alter trigger tr_rendezvous disable;");
            MigrateClient(sw);
            MigrateVehicule(sw);
            MigrateRDV(sw);
            sw.WriteLine("alter trigger tr_client enable;");
            sw.WriteLine("alter trigger tr_vehicule enable;");
            sw.WriteLine("alter trigger tr_rendezvous enable;");
            sw.Dispose();
        }




    }
}
