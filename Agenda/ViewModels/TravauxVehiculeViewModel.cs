using AgendaBDDManager;
using AgendaCore;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;

namespace Agenda.ViewModels
{
    public class TravauxVehiculeViewModel : AbstractViewModel
    {
        #region Attributes
        private string immatriculation;
        private ObservableCollection<RendezVous> rendezVousList;
        private ObservableCollection<Client> clients;
        private Vehicule vehicule;
        private Client client;
        private string searchClientValue;
        #endregion

        #region Properties

        public ObservableCollection<Client> Clients
        {
            get
            {
                return clients;
            }
            set
            {
                clients = value;
                OnPropertyChanged("Clients");
            }
        }

        public Client Client
        {
            get
            {
                return client;
            }
            set
            {
                client = value;
                OnPropertyChanged("Client");
            }
        }

        public string SearchClientValue
        {
            get
            {
                return searchClientValue;
            }
            set
            {
                searchClientValue = value;
                if (Clients != null)
                {
                    Clients.Clear();
                    Clients = null;
                }
                if (!(string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value)))
                {
                    searchClientValue = value.Trim();
                    Clients = new ObservableCollection<Client>(ClientManager.CLIENTS.Where(c => c.Nom.ToUpper().StartsWith(searchClientValue.ToUpper())).OrderBy(c => c.Nom));
                }
                else
                {
                    searchClientValue = string.Empty;
                    Clients = new ObservableCollection<Client>(ClientManager.CLIENTS.OrderBy(c => c.Nom));
                }
                OnPropertyChanged("Clients");
                OnPropertyChanged("SearchClientValue");
            }
        }

        public Vehicule Vehicule
        {
            get
            {
                return vehicule;
            }
            set
            {
                vehicule = value;
                if (vehicule != null)
                {
                    RendezVousList = new ObservableCollection<RendezVous>(RdvManager.RDVS.FindAll(
                            r => r.Vehicule.Immatriculation == vehicule.Immatriculation));
                }
                OnPropertyChanged("Vehicule");
            }
        }
        public string Immatriculation
        {
            get
            {
                return immatriculation;
            }
            set
            {
                immatriculation = Regex.Replace(value, "[^a-zA-Z0-9]", string.Empty);
                OnPropertyChanged("Immatriculation");
                var vehicules = VehiculeManager.VEHICULES.FindAll(x => x.Immatriculation == Immatriculation);
                if (vehicules.Count > 0)
                {
                    var vehicule = vehicules.First();
                    RendezVousList = new ObservableCollection<RendezVous>(RdvManager.RDVS.FindAll(
                        r => r.Vehicule.Immatriculation == vehicule.Immatriculation));
                }else
                {
                    //Aucune correspondance
                }
            }
        }

        public ObservableCollection<RendezVous> RendezVousList
        {
            get
            {
                return rendezVousList;
            }
            set
            {
                rendezVousList = value;
                OnPropertyChanged("RendezVousList");
            }
        }
        #endregion

        #region Constructors
        public TravauxVehiculeViewModel(Window view, AbstractViewModel owner, string title) 
            : base(view, owner, title)
        {
            Clients = new ObservableCollection<Client>(ClientManager.CLIENTS);
            Open();
        }
        #endregion

        #region Methods
        

        private void Print()
        {
            if (RendezVousList.Count > 0)
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

                    tf.DrawString("Rendez-vous de " + RendezVousList.ElementAt(0).Client, fontTitle, XBrushes.DarkRed, new XRect(margin, currentLine, pageWidth, pageHeight), XStringFormats.TopLeft);
                    currentLine += 40;
                    graph.DrawLine(XPens.Black, new XPoint(margin, currentLine), new XPoint(pageWidth - margin, currentLine));
                    currentLine += lineHeight;
                    tf.DrawString("Liste des Rendez-vous :", fontTitle, XBrushes.DarkRed, new XRect(margin, currentLine, pageWidth, pageHeight), XStringFormats.TopLeft);
                    currentLine += lineHeight + 10;
                    foreach (RendezVous rdv in RendezVousList)
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
                    var pdfFilename = string.Format("RDV_{0}_{1}_{2}_{3}.pdf", RendezVousList.ElementAt(0).Client.Nom, DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year).Replace('/', '_').Replace('\\', '_');
                    pdf.Save(pdfFilename);
                    Process.Start(pdfFilename);
                    pdf.Dispose();
                }
                catch (Exception e)
                {
                    MessageBox.Show(View, "Une erreur est survenue lors de la création du fichier.\n" + e.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.None);
                }
            }
            else
            {
                MessageBox.Show(View, "Aucun rendez-vous n'est sélectionné.", "Information", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.None);
            }
        }
        /// <summary>
        /// Permet de formater un rendez-vous pour l'affichage.
        /// </summary>
        /// <param name="rdv">Le rendez-vous à formater</param>
        private void displayRDV(RendezVous rdv)
        {
            //string mTravauxTitle = string.Format("{0} - M. ou MMe {1} {2}", rdv.Date.ToShortDateString(), rdv.Client != null ? rdv.Client.Prenom : "", rdv.Client != null ? rdv.Client.Nom : "");
            //string mTravauxInfos = string.Format("\t{0} {1} {2} {3} {4}km", rdv.Vehicule.Marque,
            //                                                rdv.Vehicule.Modele, rdv.Vehicule.Immatriculation,
            //                                                rdv.Vehicule.Annee, rdv.Vehicule.Kilometrage);
            //TextBlock tDate = getTextBlock(mTravauxInfos);
            //TextBlock tTitle = getTextBlock(mTravauxTitle);
            //tTitle.FontStyle = FontStyles.Italic;
            //tTitle.FontWeight = FontWeights.Bold;
            //Les_travaux.Children.Add(tTitle);
            //Les_travaux.Children.Add(tDate);
            //foreach (ReparationRDV rep in rdv.Travaux)
            //{
            //    string chaine = "Travaux effectués:\n\t- {0} {1}\n\t-qté:{2}\n\t-prix: {3}€";
            //    if (rep.Comments != null && rep.Comments.Trim().Length > 0)
            //    {
            //        chaine = "Travaux effectués:\n\t- {0}\n\t-qté:{1}\n\t-prix: {2}€\n\t-remarques : {3}";
            //    }
            //    chaine = string.Format(chaine, rep.Reparation, rep.Quantite, rep.PrixU, rep.Comments.Replace("\n", "\n\t"));
            //    mTravauxInfos += "\n" + chaine;
            //    TextBlock tTravaux = getTextBlock(chaine);
            //    Les_travaux.Children.Add(tTravaux);
            //    mTravauxInfos += "\n\n\n";
            //}
        }

        #endregion

    }
}
