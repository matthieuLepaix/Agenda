using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;
using AgendaCore;
using System.Windows;
using System.Runtime.InteropServices;

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
            if (facture != null && facture.pRdv != null)
            {
                nbWorks = 0;
                nbMO = 0;
                montantHT = 0;
                TotalHT = 0;
                var excelApp = new Excel.Application();
                // Make the object visible.
                excelApp.Visible = !imprimer;

                Excel._Workbook wb = excelApp.Workbooks.Open("C:\\modele_facture.xlsx");

                // This example uses a single workSheet. The explicit type casting is
                // removed in a later procedure.
                Excel._Worksheet workSheet = (Excel.Worksheet)excelApp.ActiveSheet;

                workSheet.Cells[RowsIndex.NFacture, ColumnsIndex.NFacture] = 9876543210;

                workSheet.Cells[RowsIndex.NomPrenom, ColumnsIndex.NomPrenom] = facture.pRdv.pClient == null ? string.Empty : facture.pRdv.pClient.ToString();
                workSheet.Cells[RowsIndex.Adresse, ColumnsIndex.Adresse] = string.IsNullOrEmpty(facture.pRdv.pClient == null ? string.Empty : facture.pRdv.pClient.pAdresse) ? string.Empty : facture.pRdv.pClient.pAdresse.ToUpperInvariant();
                workSheet.Cells[RowsIndex.CodePostal, ColumnsIndex.CodePostal] = facture.pRdv.pClient == null ? string.Empty : facture.pRdv.pClient.pCodePostal;
                workSheet.Cells[RowsIndex.Ville, ColumnsIndex.Ville] = string.IsNullOrEmpty(facture.pRdv.pClient.pVille) ? string.Empty : facture.pRdv.pClient.pVille.ToUpper();

                workSheet.Cells[RowsIndex.Date, ColumnsIndex.Date] = string.Format("{0:00}/{1:00}/{2}", DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year);
                workSheet.Cells[RowsIndex.Vehicule, ColumnsIndex.Vehicule] = facture.pRdv.pVehicule == null ? string.Empty : string.Format("{0} {1}", facture.pRdv.pVehicule.pMarque, facture.pRdv.pVehicule.pModele);
                workSheet.Cells[RowsIndex.Annee, ColumnsIndex.Annee] = facture.pRdv.pVehicule == null ? string.Empty : facture.pRdv.pVehicule.pAnnee.ToString();
                workSheet.Cells[RowsIndex.KM, ColumnsIndex.KM] = facture.pRdv.pVehicule == null ? string.Empty : facture.pRdv.pVehicule.pKilometrage.ToString();
                workSheet.Cells[RowsIndex.Immatriculation, ColumnsIndex.Immatriculation] = facture.pRdv.pVehicule == null ? string.Empty : facture.pRdv.pVehicule.pImmatriculation.ToString();

                facture.pRdv.pTravaux.ToList().ForEach(x => AddWork(workSheet, x));
                TotalHT = montantHT;
                facture.pMainOeuvres.ForEach(x => AddMO(workSheet, x));
                workSheet.Cells[RowsIndex.TypeReglement, ColumnsIndex.TypeReglement] = Facture.getReglementFromEnum(facture.pReglement);

                facture.pTotalPieceHT = montantHT;
                facture.pTotalHT = TotalHT;

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
            workSheet.Cells[RowsIndex.FirstWork + nbWorks, ColumnsIndex.Designation] = string.IsNullOrEmpty(work.pComments) ? work.pReparation.ToString() : string.Format("{0} - {1}", work.pReparation, work.pComments);
            workSheet.Cells[RowsIndex.FirstWork + nbWorks, ColumnsIndex.References] = string.IsNullOrEmpty(work.pReference) ? string.Empty : work.pReference.Trim().ToUpper();
            workSheet.Cells[RowsIndex.FirstWork + nbWorks, ColumnsIndex.Quantite] = work.pQuantite;
            workSheet.Cells[RowsIndex.FirstWork + nbWorks, ColumnsIndex.PU] = work.pPrixU;
            workSheet.Cells[RowsIndex.FirstWork + nbWorks, ColumnsIndex.MontantHT] = work.pPrixU * work.pQuantite;
            montantHT += work.pPrixU * work.pQuantite;
            nbWorks++;
        }
    }
}
