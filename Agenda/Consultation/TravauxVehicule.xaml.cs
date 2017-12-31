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
using Agenda.Config;

namespace Agenda.Consultation
{
    /// <summary>
    /// Logique d'interaction pour Travaux_Vehicule.xaml
    /// </summary>
    public partial class TravauxVehicule : System.Windows.Window
    {
        private List<RendezVous> mesRDVs = new List<RendezVous>();

        private MainWindow mOwner;

        private string pdfFilename { get; set; }

        public TravauxVehicule(MainWindow owner)
        {
            InitializeComponent();
            WindowTitle.Text = "Consultation des travaux effectués";
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
            List<Vehicule> vehicules = VehiculeManager.VEHICULES.FindAll(x => x.Immatriculation == myImmat);
            Les_travaux.Children.Clear();
            if (vehicules != null && vehicules.Count == 0)
            {
                Les_travaux.Children.Add(getTextBlock("Aucun résultats pour cette immatriculation. (immatriculation introuvable)"));
            }
            else
            {
                Vehicule vehicule = vehicules.First();
                mesRDVs = RdvManager.RDVS.FindAll(r => r.Vehicule.Immatriculation == vehicule.Immatriculation);
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
                mesRDVs = RdvManager.RDVS.FindAll(r => r.Vehicule.Immatriculation == vehicule.Immatriculation); ;
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
                mesRDVs = RdvManager.RDVS.FindAll(r => r.Client != null && r.Client.Id == client.Id);
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

        /// <summary>
        /// Permet de formater un rendez-vous pour l'affichage.
        /// </summary>
        /// <param name="rdv">Le rendez-vous à formater</param>
        private void displayRDV(RendezVous rdv)
        {
            string mTravauxTitle = string.Format("{0} - M. ou MMe {1} {2}", rdv.Date.ToShortDateString(), rdv.Client != null ? rdv.Client.Prenom : "", rdv.Client != null ? rdv.Client.Nom : "");
            string mTravauxInfos = string.Format("\t{0} {1} {2} {3} {4}km", rdv.Vehicule.Marque, 
                                                            rdv.Vehicule.Modele, rdv.Vehicule.Immatriculation, 
                                                            rdv.Vehicule.Annee, rdv.Vehicule.Kilometrage);
            TextBlock tDate = getTextBlock(mTravauxInfos);
            TextBlock tTitle = getTextBlock(mTravauxTitle);
            tTitle.FontStyle = FontStyles.Italic;
            tTitle.FontWeight = FontWeights.Bold;
            Les_travaux.Children.Add(tTitle);
            Les_travaux.Children.Add(tDate);
            foreach (ReparationRDV rep in rdv.Travaux)
            {
                string chaine = "Travaux effectués:\n\t- {0} {1}\n\t-qté:{2}\n\t-prix: {3}€";
                if (rep.Comments != null && rep.Comments.Trim().Length > 0)
                {
                    chaine = "Travaux effectués:\n\t- {0}\n\t-qté:{1}\n\t-prix: {2}€\n\t-remarques : {3}";
                }
                chaine = string.Format(chaine, rep.Reparation, rep.Quantite, rep.PrixU, rep.Comments.Replace("\n","\n\t"));
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
            //mOwner.Child = null;
            mOwner.Activate();
        }

        /// <summary>
        /// Au focus du champs de saisi de l'immatriculation.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rech_immat_GotFocus(object sender, RoutedEventArgs e)
        {
            Les_travaux.Children.Clear();
            ucSelectClient.unselect();
        }

        /// <summary>
        /// Permet de visualiser et imprimer le fichier listant les rendez-vous.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_imprimer_Click(object sender, RoutedEventArgs e)
        {
            generatePDF();
        }

        /// <summary>
        /// Permet de générer le pdf contenant tous les rendez-vous sélectionnés.
        /// </summary>
        private void generatePDF()
        {
            if (mesRDVs.Count > 0)
            {
                try
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
                    XFont fontTitle = new XFont("Verdana", 16, XFontStyle.Bold);
                    XFont fontHeader = new XFont("Verdana", 12, XFontStyle.Regular);
                    XTextFormatter tf = new XTextFormatter(graph);

                    tf.DrawString("Rendez-vous de " + mesRDVs.ElementAt(0).Client, fontTitle, XBrushes.DarkRed, new XRect(margin, currentLine, pageWidth, pageHeight), XStringFormats.TopLeft);
                    currentLine += 40;
                    graph.DrawLine(XPens.Black, new XPoint(margin, currentLine), new XPoint(pageWidth - margin, currentLine));
                    currentLine += lineHeight;
                    tf.DrawString("Liste des Rendez-vous :", fontTitle, XBrushes.DarkRed, new XRect(margin, currentLine, pageWidth, pageHeight), XStringFormats.TopLeft);
                    currentLine += lineHeight + 10;
                    foreach (RendezVous rdv in mesRDVs)
                    {
                        string infosRdv = string.Format("{0} - {1} {2} {3} {4} {5}km", rdv.Date.ToShortDateString(), rdv.Vehicule.Marque,
                                                                        rdv.Vehicule.Modele, rdv.Vehicule.Immatriculation,
                                                                        rdv.Vehicule.Annee, rdv.Vehicule.Kilometrage);
                        tf.DrawString(infosRdv, fontParagraph, XBrushes.Black, new XRect(margin * 8, currentLine, pageWidth, pageHeight), XStringFormats.TopLeft);
                        currentLine += lineHeight + 10;
                        string trvx = "Liste des travaux effectués :\n";
                        tf.DrawString(trvx, fontParagraph, XBrushes.Black, new XRect(margin * 8, currentLine, pageWidth - margin * 20, pageHeight), XStringFormats.TopLeft);
                        currentLine += lineHeight;
                        foreach (ReparationRDV rep in rdv.Travaux)
                        {
                            if (currentLine > 750)
                            {
                                page = pdf.AddPage();
                                graph = XGraphics.FromPdfPage(page);
                                tf = new XTextFormatter(graph);
                                currentLine = margin;
                            }
                            string chaine = "Travaux effectués:\n\n\t- {0} {1}\n\t-qté:{2}\n\t-prix: {3}€";
                            if (rep.Comments != null && rep.Comments.Trim().Length > 0)
                            {
                                chaine = "Travaux effectués:\n\n\t- {0}\n\t-qté:{1}\n\t-prix: {2}€\n\t-remarques :\n{3}";
                            }
                            chaine = string.Format(chaine, rep.Reparation, rep.Quantite, rep.PrixU, rep.Comments.Replace("\n", "\n\t"));
                            chaine.Replace("\t", "          ");
                            int nbReturns = rep.Comments.Split('\n').Length;
                            tf.DrawString(chaine, fontParagraph, XBrushes.Black, new XRect(margin * 10, currentLine, pageWidth - margin * 10, pageHeight), XStringFormats.TopLeft);
                            currentLine += lineHeight * (5 + nbReturns);

                        }

                        graph.DrawLine(XPens.Black, new XPoint(margin * 6, currentLine), new XPoint(pageWidth - margin * 6, currentLine));
                        currentLine += lineHeight;
                        if (currentLine > 750)
                        {
                            page = pdf.AddPage();
                            graph = XGraphics.FromPdfPage(page);
                            tf = new XTextFormatter(graph);
                            currentLine = margin;
                        }

                    }
                    pdfFilename = string.Format("RDV_{0}_{1}_{2}_{3}.pdf", mesRDVs.ElementAt(0).Client.Nom, DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year).Replace('/', '_').Replace('\\', '_');
                    pdf.Save(pdfFilename);
                    Process.Start(pdfFilename);
                    pdf.Dispose();
                }
                catch (Exception e)
                {
                    MessageBox.Show(this, "Une erreur est survenue lors de la création du fichier.\n" + e.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.None);
                }
            }
            else
            {
                MessageBox.Show(this, "Aucun rendez-vous n'est sélectionné.", "Information", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.None);
            }
        }
    }
}
