using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using AgendaBDDManager;
using Agenda.UserControls;
using AgendaCore;

using PdfSharp;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using System.Diagnostics;
using PdfSharp.Drawing.Layout;
using Microsoft.Office.Interop.Excel;

namespace Agenda.Consultation
{
    /// <summary>
    /// Logique d'interaction pour Travaux_Vehicule.xaml
    /// </summary>
    public partial class Travaux_Vehicule : System.Windows.Window
    {
        private List<RendezVous> mesRDVs = new List<RendezVous>();

        private UserControlSelectionClient ucSelectClient;

        private MainWindow mOwner;

        public Travaux_Vehicule(MainWindow owner)
        {
            InitializeComponent();
            mOwner = owner;
            mOwner.IsEnabled = false;
            mOwner.Opacity = 0.3;
            WindowTitle.Text = "Consultation des travaux effectués";
            ucSelectClient = new UserControlSelectionClient(this);
            ClientResearch.Children.Add(ucSelectClient);
            Btn_ClosePrincipale.MouseDown += new MouseButtonEventHandler(Btn_ClosePrincipale_MouseDown);
            Btn_MinimizePrincipale.MouseDown += new MouseButtonEventHandler(Btn_MinimizePrincipale_MouseDown);
            myWindowHeadBar.MouseLeftButtonDown += new MouseButtonEventHandler(myWindowHeadBar_MouseLeftButtonDown);
        }

        private TextBlock getTextBlock(string chaine)
        {
            TextBlock t = new TextBlock();
            t.Text = chaine;
            t.FontSize = 11;
            t.Foreground = Brushes.Black;
            t.Margin = new Thickness(5);
            return t;
        }

        private void btn_recherche_Click(object sender, RoutedEventArgs e)
        {
            string myImmat = rech_immat.Text.Replace('-', ' ');
            myImmat = myImmat.Replace(" ", "");
            myImmat = myImmat.Trim();
            List<Vehicule> vehicules = VehiculeManager.GetVehiculesByImmatriculation(myImmat);
            Les_travaux.Children.Clear();
            if (vehicules != null && vehicules.Count == 0)
            {
                Les_travaux.Children.Add(getTextBlock("Aucun résultats pour cette immatriculation. (immatriculation introuvable)"));
            }
            else
            {
                Vehicule vehicule = vehicules.First();
                mesRDVs = RdvManager.GetTravauxByImmat(vehicule.pImmatriculation);
                if (mesRDVs.Count == 0)
                {
                    Les_travaux.Children.Add(getTextBlock("Aucun résultats pour cette immatriculation. (aucun rendez-vous)"));
                }
                else
                {
                    foreach (RendezVous rdv in mesRDVs)
                    {
                        displayRDV(rdv);
                    }
                }
            }
        }

        public void SetVehiculeSelected(AgendaCore.Vehicule vehicule)
        {
            if (vehicule != null)
            {
                mesRDVs = RdvManager.GetTravauxByImmat(vehicule.pImmatriculation);
                Les_travaux.Children.Clear();
                if (mesRDVs.Count == 0)
                {
                    Les_travaux.Children.Add(getTextBlock("Aucun résultats pour cette immatriculation. (aucun rendez-vous)"));
                }
                else
                {
                    foreach (RendezVous rdv in mesRDVs)
                    {
                        displayRDV(rdv);
                    }
                }
            }
        }

        public void SetClientSelected(Client client)
        {
            if (client != null)
            {
                mesRDVs = RdvManager.GetTravauxByClient(client.pId);
                Les_travaux.Children.Clear();
                if (mesRDVs.Count == 0)
                {
                    Les_travaux.Children.Add(getTextBlock("Aucun résultats pour cette immatriculation."));
                }
                else
                {
                    foreach (RendezVous rdv in mesRDVs)
                    {
                        displayRDV(rdv);
                    }
                }
            }
        }

        private void displayRDV(RendezVous rdv)
        {
            string mTravauxTitle = string.Format("{0} - M. ou MMe {1} {2}",rdv.pDate.ToShortDateString(), rdv.pClient.pPrenom, rdv.pClient.pNom);
            string mTravauxInfos = string.Format("\t{0} {1} {2} {3} {4}km", rdv.pVehicule.pMarque, 
                                                            rdv.pVehicule.pModele, rdv.pVehicule.pImmatriculation, 
                                                            rdv.pVehicule.pAnnee, rdv.pVehicule.pKilometrage);
            TextBlock tDate = getTextBlock(mTravauxInfos);
            TextBlock tTitle = getTextBlock(mTravauxTitle);
            tTitle.FontStyle = FontStyles.Italic;
            tTitle.FontWeight = FontWeights.Bold;
            Les_travaux.Children.Add(tTitle);
            Les_travaux.Children.Add(tDate);
            foreach (ReparationRDV rep in rdv.pTravaux)
            {
                string chaine = "Travaux effectués:\n\t- {0} {1}\n\t-qté:{2}\n\t-prix: {3}€";
                if (rep.pComments != null && rep.pComments.Trim().Length > 0)
                {
                    chaine = "Travaux effectués:\n\t- {0}\n\t-qté:{1}\n\t-prix: {2}€\n\t-remarques : {3}";
                }
                chaine = string.Format(chaine, rep.pReparation, rep.pQuantite, rep.pPrixU, rep.pComments.Replace("\n","\n\t"));
                mTravauxInfos += "\n" + chaine;
                TextBlock tTravaux = getTextBlock(chaine);
                Les_travaux.Children.Add(tTravaux);
                mTravauxInfos += "\n\n\n";
            }
        }

        void Btn_MinimizePrincipale_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
        }

        void Btn_ClosePrincipale_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }


        void myWindowHeadBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closed(object sender, EventArgs e)
        {
            mOwner.IsEnabled = true;
            mOwner.Opacity = 1;
            mOwner.WindowState = System.Windows.WindowState.Maximized;
        }

        private void rech_immat_GotFocus(object sender, RoutedEventArgs e)
        {
            Les_travaux.Children.Clear();
            ucSelectClient.unselect();
        }

        private void btn_imprimer_Click(object sender, RoutedEventArgs e)
        {
            generatePDF();
        }

        private void completeBill()
        {

        }

        private void generatePDF()
        {
            
            if (mesRDVs.Count > 0)
            {
                int margin = 10;
                int lineHeight = 20;
                double pageWidth;
                double pageHeight;
                int currentLine = 10;

                PdfDocument pdf = new PdfDocument();
                pdf.Info.Title = "My First PDF";
                PdfPage page = pdf.AddPage();
                page.Size = PageSize.A4;
                pageWidth = page.Width.Point;
                pageHeight = page.Height.Point;
                page.Orientation = PageOrientation.Portrait;
                XGraphics graph = XGraphics.FromPdfPage(page);
                XFont fontParagraph = new XFont("Verdana", 12, XFontStyle.Regular);
                XFont fontTitle = new XFont("Verdana", 20, XFontStyle.Bold);
                XFont fontHeader = new XFont("Verdana", 12, XFontStyle.Regular);
                XTextFormatter tf = new XTextFormatter(graph);

                tf.DrawString("Rendez-vous de M. ou Mme "+mesRDVs.ElementAt(0).pClient.pNom, fontTitle, XBrushes.Black, new XRect(margin, currentLine, pageWidth, pageHeight), XStringFormats.TopLeft);
                currentLine += 30;
                graph.DrawLine(XPens.Black, new XPoint(margin, currentLine), new XPoint(pageWidth-margin, currentLine));
                currentLine += lineHeight;
                tf.DrawString("Liste des Rendez-vous :", fontTitle, XBrushes.Black, new XRect(margin, currentLine, pageWidth, pageHeight), XStringFormats.TopLeft);
                currentLine += lineHeight+10;
                foreach (RendezVous rdv in mesRDVs)
                {
                    string infosRdv = string.Format("{0} - M. ou MMe {1} {2}\n", rdv.pDate.ToShortDateString(), rdv.pClient.pPrenom, rdv.pClient.pNom);
                    infosRdv += string.Format("{0} {1} {2} {3} {4}km", rdv.pVehicule.pMarque,
                                                                    rdv.pVehicule.pModele, rdv.pVehicule.pImmatriculation,
                                                                    rdv.pVehicule.pAnnee, rdv.pVehicule.pKilometrage);
                    tf.DrawString(infosRdv, fontParagraph, XBrushes.Black, new XRect(margin*3, currentLine, pageWidth, pageHeight), XStringFormats.TopLeft);
                    currentLine += lineHeight+10;
                    string trvx = "Liste des travaux effectués :\n\n";
                    tf.DrawString(trvx, fontParagraph, XBrushes.Black, new XRect(margin*3, currentLine, pageWidth, pageHeight), XStringFormats.TopLeft);
                    currentLine += lineHeight;
                    foreach (ReparationRDV rep in rdv.pTravaux)
                    {
                        if (currentLine > 750)
                        {
                            page = pdf.AddPage();
                            graph = XGraphics.FromPdfPage(page);
                            tf = new XTextFormatter(graph);
                            currentLine = margin;
                        }
                        string chaine = "Travaux effectués:\n\t- {0} {1}\n\t-qté:{2}\n\t-prix: {3}€";
                        if (rep.pComments != null && rep.pComments.Trim().Length > 0)
                        {
                            chaine = "Travaux effectués:\n\t- {0}\n\t-qté:{1}\n\t-prix: {2}€\n\t-remarques : {3}";
                        }
                        chaine = string.Format(chaine, rep.pReparation, rep.pQuantite, rep.pPrixU, rep.pComments.Replace("\n", "\n\t"));
                        int nbReturns = rep.pComments.Split('\n').Length;
                        tf.DrawString(chaine, fontParagraph, XBrushes.Black, new XRect(margin * 4, currentLine, pageWidth, pageHeight), XStringFormats.TopLeft);
                        currentLine += lineHeight*(3+nbReturns);
                        
                    }

                    graph.DrawLine(XPens.Black, new XPoint(margin*6, currentLine), new XPoint(pageWidth - margin*6, currentLine));
                    currentLine += lineHeight;
                    if (currentLine > 750)
                    {
                        page = pdf.AddPage();
                        graph = XGraphics.FromPdfPage(page);
                        tf = new XTextFormatter(graph);
                        currentLine = margin;
                    }

                }


                string pdfFilename = string.Format("RDV_{0}_{1}_{2}_{3}.pdf", mesRDVs.ElementAt(0).pClient.pNom, DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year);
                pdf.Save(pdfFilename);
                Process.Start(pdfFilename);
            }
        }
    }
}
