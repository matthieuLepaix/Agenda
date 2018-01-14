using Agenda.Utils;
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
        private ObservableCollection<ReparationRDV> worksSelected;
        private ObservableCollection<Client> clients;
        private Vehicule vehicule;
        private Client client;
        private string searchClientValue;
        private Command lookingForHistoryCommand;
        private Command printCommand;
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
                    var rdvs = RdvManager.RDVS.FindAll(r => r.Vehicule.Immatriculation == vehicule.Immatriculation);
                    var tmp = new List<ReparationRDV>();
                    rdvs.ForEach(r => tmp.AddRange(r.Travaux));
                    WorksSelected = new ObservableCollection<ReparationRDV>(tmp);
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
            }
        }


        public Command LookingForHistoryCommand
        {
            get
            {
                return lookingForHistoryCommand;
            }
            set
            {
                lookingForHistoryCommand = value;
                OnPropertyChanged("LookingForHistoryCommand");
            }

        }

        public ObservableCollection<ReparationRDV> WorksSelected
        {
            get
            {
                return worksSelected;
            }
            set
            {
                worksSelected = value;
                OnPropertyChanged("WorksSelected");
            }
        }


        public Command PrintCommand
        {
            get
            {
                return printCommand;
            }
            set
            {
                printCommand = value;
                OnPropertyChanged("PrintCommand");
            }
        }
        #endregion

        #region Constructors
        public TravauxVehiculeViewModel(Window view, AbstractViewModel owner, string title)
            : base(view, owner, title)
        {
            Clients = new ObservableCollection<Client>(ClientManager.CLIENTS.OrderBy(c => c.Nom));
            LookingForHistoryCommand = new Command((x) =>
            {
                var vehicules = VehiculeManager.VEHICULES.FindAll(v => v.Immatriculation == Immatriculation);
                if (vehicules.Count > 0)
                {
                    var v = vehicules.First();
                    SearchClientValue = v.Client.Nom;
                    Vehicule = v;
                    Client = Vehicule.Client;
                }
                else
                {
                    MessageBox.Show("Aucun véhicule immatriculé : " + Immatriculation, "Attention", MessageBoxButton.OK, MessageBoxImage.Information);
                }

            });
            PrintCommand = new Command((x) =>
            {
                Print();
            });
            Open();
        }
        #endregion

        #region Methods


        private void Print()
        {
            if (WorksSelected != null && WorksSelected.Count > 0)
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
                    pageWidth = page.Width.Point - 5;
                    pageHeight = page.Height.Point - 5;
                    page.Orientation = PageOrientation.Portrait;
                    XGraphics graph = XGraphics.FromPdfPage(page);
                    XFont fontParagraph = new XFont("Verdana", 12, XFontStyle.Regular);
                    XFont fontTitle = new XFont("Verdana", 16, XFontStyle.Bold);
                    XFont fontHeader = new XFont("Verdana", 12, XFontStyle.Regular);
                    XTextFormatter tf = new XTextFormatter(graph);

                    tf.DrawString("Rendez-vous de " + WorksSelected.ElementAt(0).RendezVous.Client, fontTitle, XBrushes.DarkRed, new XRect(margin, currentLine, pageWidth, pageHeight), XStringFormats.TopLeft);
                    currentLine += 40;
                    graph.DrawLine(XPens.Black, new XPoint(margin, currentLine), new XPoint(pageWidth - margin, currentLine));
                    currentLine += lineHeight;
                    tf.DrawString("Liste des Rendez-vous :", fontTitle, XBrushes.DarkRed, new XRect(margin, currentLine, pageWidth, pageHeight), XStringFormats.TopLeft);
                    currentLine += lineHeight + 10;
                    foreach (ReparationRDV rep in WorksSelected)
                    {
                        var rdv = rep.RendezVous;
                        string infosRdv = string.Format("{0} - {1} {2} {3} {4} {5}km", rdv.Date.ToShortDateString(), rdv.Vehicule.Marque,
                                                                        rdv.Vehicule.Modele, rdv.Vehicule.Immatriculation,
                                                                        rdv.Vehicule.Annee, rdv.Vehicule.Kilometrage);
                        tf.DrawString(infosRdv, fontParagraph, XBrushes.Black, new XRect(margin * 8, currentLine, pageWidth, pageHeight), XStringFormats.TopLeft);
                        currentLine += lineHeight + 10;

                        if (currentLine > 750)
                        {
                            page = pdf.AddPage();
                            graph = XGraphics.FromPdfPage(page);
                            tf = new XTextFormatter(graph);
                            currentLine = margin;
                        }
                        var chaine = "{0} :\n\n{1}\n\n{2}€";
                        chaine = string.Format(chaine, rep.Reparation, rep.Comments.Replace("\n", "\n\t"), rep.PrixU);
                        chaine.Replace("\t", "          ");
                        int nbReturns = rep.Comments.Split('\n').Length;
                        tf.DrawString(chaine, fontParagraph, XBrushes.Black, new XRect(margin * 10, currentLine, pageWidth - margin * 10, pageHeight), XStringFormats.TopLeft);
                        currentLine += lineHeight * (5 + nbReturns);

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
                    var pdfFilename = string.Format("RDV_{0}_{1}_{2}_{3}.pdf", WorksSelected.ElementAt(0).RendezVous.Client.Nom, DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year).Replace('/', '_').Replace('\\', '_');
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
        #endregion

    }
}
