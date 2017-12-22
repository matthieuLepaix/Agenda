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
        private bool isNewClient;
        private string searchClientValue;

        private DateTime selectedDate;
        private int selectedHour;
        private int selectedDuration;
        private ObservableCollection<string> hours;
        private ObservableCollection<string> durations;


        private ObservableCollection<Client> clients;
        private Client client;
        private Vehicule vehicule;
        private RendezVous rendezVous;
        private Command newClientCommand;
        private Command updateVehiculeCommand;
        private Command addVehiculeCommand;
        private Command validateRendezVousCommand;
        private Command factureCommand;
        private Command cancelCommand;
        private Command addWorkCommand;

        #endregion

        #region Properties

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
                    Clients = new ObservableCollection<Client>(ClientManager.CLIENTS.Where(c => c.Nom.ToUpper().StartsWith(searchClientValue.ToUpper())));
                }
                else
                {
                    searchClientValue = string.Empty;
                    Clients = new ObservableCollection<Client>(ClientManager.CLIENTS);
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

        public Vehicule Vehicule
        {
            get
            {
                return vehicule;
            }
            set
            {
                vehicule = value;
                OnPropertyChanged("Vehicule");
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

        public DateTime SelectedDate
        {
            get
            {
                return selectedDate;
            }
            set
            {
                selectedDate = value;
                OnPropertyChanged("SelectedDate");
            }
        }


        public int SelectedHour
        {
            get
            {
                return selectedHour;
            }
            set
            {
                selectedHour = value;
                OnPropertyChanged("SelectedHour");
            }
        }
        public int SelectedDuration
        {
            get
            {
                return selectedDuration;
            }
            set
            {
                selectedDuration = value;
                OnPropertyChanged("SelectedDuration");
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

        public GestionRendezVousViewModel(Window view, AgendaViewModel owner)
            : base(view, owner)
        {
            View = view;
            Owner = owner;
            WindowTitle = "Gestion d'un rendez-vous";
            //RendezVous = new RendezVous()
            InitHoursAndDurations();
            InitCommands();

            SearchClientValue = string.Empty;
            SelectedDate = DateTime.Now;
            SelectedHour = 0;
            SelectedDuration = 0;

            Open();
        }

        private void InitHoursAndDurations()
        {
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
            AddVehiculeCommand = new Command((x) =>
            {
                AddVehicule();
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
                Vehicule = null;
                Close();
            });
            AddWorkCommand = new Command((x) =>
            {
                AddWork();
            });
        }


        #endregion

        #region Methods


        private void NewClient()
        {
            Vehicule = new Vehicule();
            IsNewClient = true;
        }

        private void ValidateRendezVous()
        {

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

        }


        private void AddVehicule()
        {
            if (Client != null)
            {
                //(Child = new GestionVehicule(this, Client, false)).Show();
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un client.", "Attention", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void UpdateVehicule()
        {
        }

        #endregion
    }
}
