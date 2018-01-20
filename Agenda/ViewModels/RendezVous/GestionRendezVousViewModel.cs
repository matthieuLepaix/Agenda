using Agenda.Gestion;
using Agenda.UserControls;
using Agenda.Utils;
using AgendaBDDManager;
using AgendaCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Agenda.ViewModels
{
    public class GestionRendezVousViewModel : AbstractViewModel
    {
        #region Attributes
        private const string title = "Prise de rendez-vous";
        private bool isNewClient;
        private string searchClientValue;

        private ObservableCollection<string> hours;
        private ObservableCollection<string> durations;


        private ObservableCollection<Client> clients;
        private RendezVous rendezVous;
        private Command newClientCommand;
        private Command updateVehiculeCommand;
        private Command addVehiculeCommand;
        private Command validateRendezVousCommand;
        private Command factureCommand;
        private Command cancelCommand;
        private Command addWorkCommand;
        private Command refreshWorkCommand;
        private bool canChangeVehicule;

        private RendezVous rendezVousOriginal;


        #endregion

        #region Properties

        public bool CanChangeVehicule
        {
            get
            {
                return canChangeVehicule;
            }
            set
            {
                canChangeVehicule = value;
                OnPropertyChanged("CanChangeVehicule");
            }
        }

        public bool IsNewClient
        {
            get
            {
                return isNewClient;
            }
            set
            {
                isNewClient = value;
                OnPropertyChanged("IsNewClient");
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

        public RendezVous RendezVous
        {
            get
            {
                return rendezVous;
            }
            set
            {
                rendezVous = value;
                OnPropertyChanged("RendezVous");
            }
        }


        public Command NewClientCommand
        {
            get
            {
                return newClientCommand;
            }
            set
            {
                newClientCommand = value;
                OnPropertyChanged("NewClientCommand");
            }
        }

        public Command AddVehiculeCommand
        {
            get
            {
                return addVehiculeCommand;
            }
            set
            {
                addVehiculeCommand = value;
                OnPropertyChanged("AddVehiculeCommand");
            }
        }
        public Command UpdateVehiculeCommand
        {
            get
            {
                return updateVehiculeCommand;
            }
            set
            {
                updateVehiculeCommand = value;
                OnPropertyChanged("UpdateVehiculeCommand");
            }
        }



        public Command ValidateRendezVousCommand
        {
            get
            {
                return validateRendezVousCommand;
            }
            set
            {
                validateRendezVousCommand = value;
                OnPropertyChanged("ValidateRendezVousCommand");
            }
        }
        public Command FactureCommand
        {
            get
            {
                return factureCommand;
            }
            set
            {
                factureCommand = value;
                OnPropertyChanged("FactureCommand");
            }
        }

        public Command CancelCommand
        {
            get
            {
                return cancelCommand;
            }
            set
            {
                cancelCommand = value;
                OnPropertyChanged("CancelCommand");
            }
        }

        public Command AddWorkCommand
        {
            get
            {
                return addWorkCommand;
            }
            set
            {
                addWorkCommand = value;
                OnPropertyChanged("AddWorkCommand");
            }
        }

        public Command RefreshWorkCommand
        {
            get
            {
                return refreshWorkCommand;
            }
            set
            {
                refreshWorkCommand = value;
                OnPropertyChanged("RefreshWorkCommand");
            }
        }

        public ObservableCollection<string> Hours
        {
            get
            {
                return hours;
            }
            set
            {
                hours = value;
                OnPropertyChanged("Hours");
            }
        }


        public ObservableCollection<string> Durations
        {
            get
            {
                return durations;
            }
            set
            {
                durations = value;
                OnPropertyChanged("Durations");
            }
        }

        #endregion

        #region Constructors

        public GestionRendezVousViewModel(Window view, AgendaViewModel owner, RendezVous rendezVous)
            : base(view, owner, title)
        {
            InitHoursAndDurations();
            InitCommands();
            RendezVous = new RendezVous(rendezVous);
            rendezVousOriginal = rendezVous;

            SearchClientValue = string.Empty;
            //Set the new client if it is an existing rendez vous. Just show the editable client's info.
            IsNewClient = RendezVous.Client != null && RendezVous.Vehicule != null;
            CanChangeVehicule = IsNewClient; // and then user cannot change the vehicule


            Open();
        }

        private void InitHoursAndDurations()
        {
            if (Durations == null)
                Durations = new ObservableCollection<string>();
            else
                Durations.Clear();
            Durations.Add("30 minutes");
            Durations.Add("1 heure");
            Durations.Add("2 heures");
            Durations.Add("3 heures");
            Durations.Add("4 heures");
            Durations.Add("5 heures");
            Durations.Add("6 heures");
            Durations.Add("7 heures");
            Durations.Add("8 heures");
            Durations.Add("Plus de 8 heures");

            if (Hours == null)
                Hours = new ObservableCollection<string>();
            else
                Hours.Clear();
            Hours.Add("08 heures");
            Hours.Add("09 heures");
            Hours.Add("10 heures");
            Hours.Add("11 heures");
            Hours.Add("12 heures");
            Hours.Add("13 heures");
            Hours.Add("14 heures");
            Hours.Add("15 heures");
            Hours.Add("16 heures");
            Hours.Add("17 heures");
            Hours.Add("18 heures");
        }

        private void InitCommands()
        {
            UpdateVehiculeCommand = new Command((x) =>
            {
                UpdateVehicule();
            });
            NewClientCommand = new Command((x) =>
            {
                NewClient();
            });
            FactureCommand = new Command((x) =>
            {
                Facture();
            });
            ValidateRendezVousCommand = new Command((x) =>
            {
                ValidateRendezVous();
            });
            CancelCommand = new Command((x) =>
            {
                Close();
            });
            AddWorkCommand = new Command((x) =>
            {
                AddWork();
            });
            RefreshWorkCommand = new Command((x) =>
            {
                RefreshWork();
            });
        }


        #endregion

        #region Methods


        private void NewClient()
        {
            RendezVous.Vehicule = new Vehicule();
            IsNewClient = true;
            OnPropertyChanged("RendezVous");
        }

        private void ValidateRendezVous()
        {
            if (JoursFeriesManager.JOURS_FERIES.Any(d => d.Jour.ToShortDateString() == RendezVous.Date.ToShortDateString()))
            {
                MessageBox.Show("Le jour choisi est un jour férié.",
                "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (RendezVous.Vehicule == null)
            {
                if (!IsNewClient)
                {
                    MessageBox.Show("Veuillez choisir un client puis un véhicule.",
                    "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                else
                {
                    MessageBox.Show("Veuillez entrer les informations concernant le client et le véhicule.",
                        "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
            else if (RendezVous.Travaux.Where(t => t.IsActive && t.Reparation != null).Count() == 0)
            {
                MessageBox.Show("Veuillez saisir les travaux à effectuer.",
                    "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                RendezVous.Travaux = RendezVous.Travaux.Where(t => t.IsActive && t.Reparation != null).ToList();
                rendezVousOriginal = RendezVous;
                CreateOrUpdateRDV();
            }
        }

        private void CreateOrUpdateRDV()
        {
            RendezVous.Client = RendezVous.Vehicule.Client;
            try
            {
                if (RendezVous.Id != -1)
                {
                    RdvManager.UpdateRdv(RendezVous);
                }
                else
                {
                    RdvManager.AddRdv(RendezVous);
                }

                ClientManager.UpdateClient(RendezVous.Vehicule.Client);
                VehiculeManager.UpdateVehicule(RendezVous.Vehicule);

                ((AgendaViewModel)Owner).RefreshRendezVousIntoAgenda();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Le rendez-vous n'a pas pu être enregistrer.\nErreur :" + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                this.Close();
            }
        }


        private void Facture()
        {
            //CreateOrUpdateRDV();
            //mOwner.InitFacture(mRdv);
            //mOwner.selectTabIndex(MainWindow.TabOptions.FACTURE);
            Close();
        }

        private void AddWork()
        {
            RendezVous.Travaux.Add(new ReparationRDV(RendezVous));
            OnPropertyChanged("RendezVous");
        }

        private void RefreshWork()
        {
            RendezVous.Travaux.RemoveAll(t => !t.IsActive);
            OnPropertyChanged("RendezVous");
        }


        private void AddVehicule()
        {
            if (RendezVous.Client != null)
            {
                Child = new GestionVehicule(this);
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un client.", "Attention", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void UpdateVehicule()
        {
            if (RendezVous.Client != null)
            {
                if (RendezVous.Client.Vehicules.Count() > 1)
                {
                    Child = new GestionVehicule(this);
                }
                else
                {
                    MessageBox.Show("Inutile, le client ne possède qu'une voiture.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        public void ChangeVehicule(Vehicule Vehicule)
        {
            RendezVous.Vehicule = Vehicule;
            OnPropertyChanged("RendezVous");
        }

        #endregion
    }
}
