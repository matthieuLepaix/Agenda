using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;
using AgendaCore;
using System.Windows;
using System.Runtime.InteropServices;
using Agenda.Config;

namespace Agenda.Factures
{
    public class ExcelManager
    {
        enum RowsIndex
        {
            NFacture = 7,
            NomPrenom = 6,
            Adresse = 7,
            CodePostal = 8,
            Ville = 8,
            Date = 11,
            Vehicule = 11,
            Annee = 11,
            KM = 11,
            Immatriculation = 11,
            FirstWork = 14,
            TypeReglement = 30,
            TotalHTAvantMO = 29,
            ForfaitMO1 = 30,
            TotalHTApresMO = 35,
            TVA = 36,
            TotalTTC = 37
        }

        enum ColumnsIndex
        {
            NFacture = 3,
            NomPrenom = 4,
            Adresse = 4,
            CodePostal = 4,
            Ville = 5,
            Date = 2,
            Vehicule = 3,
            Annee = 4,
            KM = 5,
            Immatriculation = 6,
            Designation = 1,
            References = 5,
            Quantite = 6,
            PU = 7,
            MontantHT = 8,
            TypeReglement = 1,
            TotalHTAvantMO = 8,
            ForfaitMO = 8,
            TotalHTApresMO = 8,
            TVA = 8,
            TotalTTC = 8
        }


        private static int nbWorks;
        private static int nbMO;
        private static float montantHT;
        private static float TotalHT;


        public static void GenerateExcelFacture(Facture facture, bool imprimer)
        {
            if (facture != null && facture.RendezVous != null)
            {
                nbWorks = 0;
                nbMO = 0;
                montantHT = 0;
                TotalHT = 0;
                var excelApp = new Excel.Application();
                // Make the object visible.
                excelApp.Visible = !imprimer;
                Excel._Workbook wb = excelApp.Workbooks.Open(string.Format("{0}\\{1}", Configuration.ApplicationPath, Configuration.ModeleFactureFileName));

                // This example uses a single workSheet. The explicit type casting is
                // removed in a later procedure.
                Excel._Worksheet workSheet = (Excel.Worksheet)excelApp.ActiveSheet;

                workSheet.Cells[RowsIndex.NFacture, ColumnsIndex.NFacture] = facture.Id;

                workSheet.Cells[RowsIndex.NomPrenom, ColumnsIndex.NomPrenom] = facture.RendezVous.Client == null ? string.Empty : facture.RendezVous.Client.ToString();
                workSheet.Cells[RowsIndex.Adresse, ColumnsIndex.Adresse] = string.IsNullOrEmpty(facture.RendezVous.Client == null ? string.Empty : facture.RendezVous.Client.Adresse) ? string.Empty : facture.RendezVous.Client.Adresse.ToUpperInvariant();
                workSheet.Cells[RowsIndex.CodePostal, ColumnsIndex.CodePostal] = facture.RendezVous.Client == null ? string.Empty : facture.RendezVous.Client.CodePostal;
                workSheet.Cells[RowsIndex.Ville, ColumnsIndex.Ville] = string.IsNullOrEmpty(facture.RendezVous.Client.Ville) ? string.Empty : facture.RendezVous.Client.Ville.ToUpper();

                workSheet.Cells[RowsIndex.Date, ColumnsIndex.Date] = string.Format("{0:00}/{1:00}/{2}", DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year);
                workSheet.Cells[RowsIndex.Vehicule, ColumnsIndex.Vehicule] = facture.RendezVous.Vehicule == null ? string.Empty : string.Format("{0} {1}", facture.RendezVous.Vehicule.Marque, facture.RendezVous.Vehicule.Modele);
                workSheet.Cells[RowsIndex.Annee, ColumnsIndex.Annee] = facture.RendezVous.Vehicule == null ? string.Empty : facture.RendezVous.Vehicule.Annee.ToString();
                workSheet.Cells[RowsIndex.KM, ColumnsIndex.KM] = facture.RendezVous.Vehicule == null ? string.Empty : facture.RendezVous.Vehicule.Kilometrage.ToString();
                workSheet.Cells[RowsIndex.Immatriculation, ColumnsIndex.Immatriculation] = facture.RendezVous.Vehicule == null ? string.Empty : facture.RendezVous.Vehicule.Immatriculation.ToString();

                facture.RendezVous.Travaux.ToList().ForEach(x => AddWork(workSheet, x));
                TotalHT = montantHT;
                facture.MainOeuvres.ForEach(x => AddMO(workSheet, x));
                workSheet.Cells[RowsIndex.TypeReglement, ColumnsIndex.TypeReglement] = Facture.getReglementFromEnum(facture.Reglement);

                facture.TotalPieceHT = montantHT;
                facture.TotalHT = TotalHT;

                if (imprimer)
                {
                    workSheet.PrintOut(Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                        Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    Marshal.FinalReleaseComObject(workSheet);
                    wb.Close(false, Type.Missing, Type.Missing);
                    Marshal.FinalReleaseComObject(wb);
                    excelApp.Quit();
                    Marshal.FinalReleaseComObject(excelApp);
                }
            }
            else
            {
                MessageBox.Show("Création de la facture impossible. La facture n'existe pas.", "Alerte",
                        MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }

        }


        public static void GenerateExcelDevis(Devis devis, bool imprimer)
        {
            if (devis != null)
            {
                nbWorks = 0;
                nbMO = 0;
                montantHT = 0;
                TotalHT = 0;
                var excelApp = new Excel.Application();
                // Make the object visible.
                excelApp.Visible = !imprimer;
                Excel._Workbook wb = excelApp.Workbooks.Open(string.Format("{0}\\{1}", Configuration.ApplicationPath, Configuration.ModeleDevisFileName));

                // This example uses a single workSheet. The explicit type casting is
                // removed in a later procedure.
                Excel._Worksheet workSheet = (Excel.Worksheet)excelApp.ActiveSheet;

                workSheet.Cells[RowsIndex.NomPrenom, ColumnsIndex.NomPrenom] = devis.Vehicule.Client == null ? string.Empty : devis.Vehicule.Client.ToString();
                workSheet.Cells[RowsIndex.Adresse, ColumnsIndex.Adresse] = string.IsNullOrEmpty(devis.Vehicule.Client == null ? string.Empty : devis.Vehicule.Client.Adresse) ? string.Empty : devis.Vehicule.Client.Adresse.ToUpperInvariant();
                workSheet.Cells[RowsIndex.CodePostal, ColumnsIndex.CodePostal] = devis.Vehicule.Client == null ? string.Empty : devis.Vehicule.Client.CodePostal;
                workSheet.Cells[RowsIndex.Ville, ColumnsIndex.Ville] = string.IsNullOrEmpty(devis.Vehicule.Client.Ville) ? string.Empty : devis.Vehicule.Client.Ville.ToUpper();

                workSheet.Cells[RowsIndex.Date, ColumnsIndex.Date] = string.Format("{0:00}/{1:00}/{2}", DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year);
                workSheet.Cells[RowsIndex.Vehicule, ColumnsIndex.Vehicule] = devis.Vehicule == null ? string.Empty : string.Format("{0} {1}", devis.Vehicule.Marque, devis.Vehicule.Modele);
                workSheet.Cells[RowsIndex.Annee, ColumnsIndex.Annee] = devis.Vehicule == null ? string.Empty : devis.Vehicule.Annee.ToString();
                workSheet.Cells[RowsIndex.KM, ColumnsIndex.KM] = devis.Vehicule == null ? string.Empty : devis.Vehicule.Kilometrage.ToString();
                workSheet.Cells[RowsIndex.Immatriculation, ColumnsIndex.Immatriculation] = devis.Vehicule == null ? string.Empty : devis.Vehicule.Immatriculation.ToString();

                devis.Reparations.ForEach(x => AddWork(workSheet, x));
                TotalHT = montantHT;
                devis.Maindoeuvres.ForEach(x => AddMO(workSheet, x));
                workSheet.Cells[RowsIndex.TypeReglement, ColumnsIndex.TypeReglement] = Facture.getReglementFromEnum(devis.Reglement);
                
                if (imprimer)
                {
                    workSheet.PrintOut(Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                        Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    Marshal.FinalReleaseComObject(workSheet);
                    wb.Close(false, Type.Missing, Type.Missing);
                    Marshal.FinalReleaseComObject(wb);
                    excelApp.Quit();
                    Marshal.FinalReleaseComObject(excelApp);
                }
            }
            else
            {
                MessageBox.Show("Création de la facture impossible. La facture n'existe pas.", "Alerte",
                        MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }

        }

        private static void AddMO(Excel._Worksheet workSheet, float mo)
        {
            workSheet.Cells[RowsIndex.ForfaitMO1 + nbMO++, ColumnsIndex.ForfaitMO] = mo;
            TotalHT += mo;
        }

        private static void AddWork(Excel._Worksheet workSheet, ReparationRDV work)
        {
            workSheet.Cells[RowsIndex.FirstWork + nbWorks, ColumnsIndex.Designation] = string.IsNullOrEmpty(work.Comments) ? work.Reparation.ToString() : string.Format("{0} - {1}", work.Reparation, work.Comments);
            workSheet.Cells[RowsIndex.FirstWork + nbWorks, ColumnsIndex.References] = string.IsNullOrEmpty(work.Reference) ? string.Empty : work.Reference.Trim().ToUpper();
            workSheet.Cells[RowsIndex.FirstWork + nbWorks, ColumnsIndex.Quantite] = work.Quantite;
            workSheet.Cells[RowsIndex.FirstWork + nbWorks, ColumnsIndex.PU] = work.PrixU;
            workSheet.Cells[RowsIndex.FirstWork + nbWorks, ColumnsIndex.MontantHT] = work.PrixU * work.Quantite;
            montantHT += work.PrixU * work.Quantite;
            nbWorks++;
        }
    }
}
