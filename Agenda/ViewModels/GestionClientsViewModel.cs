using Agenda.Gestion;
using Agenda.Utils;
using AgendaBDDManager;
using AgendaCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;

namespace Agenda.ViewModels
{
    public class GestionClientsViewModel : AbstractViewModel
    {
        #region Attributes
        
        private string searchClientValue;
        private ObservableCollection<Client> clients;
        private Client client;
        private Vehicule selectedVehicule;
        private Command addClientCommand;
        private Command delClientCommand;
        private Command delVehiculeCommand;
        private Command updateCommand;
        private Command addVehiculeCommand;
        private Command goToRendezVousCommand;

        #endregion

        #region Properties
        
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

        public Vehicule SelectedVehicule
        {
            get
            {
                return selectedVehicule;
            }
            set
            {
                selectedVehicule = value;
                OnPropertyChanged("SelectedVehicule");
            }
        }


        public Command AddClientCommand
        {
            get
            {
                return addClientCommand;
            }
            set
            {
                addClientCommand = value;
                OnPropertyChanged("AddClientCommand");
            }
        }

        public Command DelClientCommand
        {
            get
            {
                return delClientCommand;
            }
            set
            {
                delClientCommand = value;
                OnPropertyChanged("DelClientCommand");
            }
        }
        public Command DelVehiculeCommand
        {
            get
            {
                return delVehiculeCommand;
            }
            set
            {
                delVehiculeCommand = value;
                OnPropertyChanged("DelVehiculeCommand");
            }
        }

        public Command UpdateCommand
        {
            get
            {
                return updateCommand;
            }
            set
            {
                updateCommand = value;
                OnPropertyChanged("UpdateCommand");
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

        public Command GoToRendezVousCommand
        {
            get
            {
                return goToRendezVousCommand;
            }
            set
            {
                goToRendezVousCommand = value;
                OnPropertyChanged("GoToRendezVousCommand");
            }
        }
        #endregion

        #region Constructors

        public GestionClientsViewModel(Window view, AgendaViewModel owner)
            :base(view,owner)
        {
            View = view;
            Owner = owner;
            SearchClientValue = string.Empty;
            AddClientCommand = new Command((x) =>
            {
                new AjoutClient(this).Show();
            });
            DelClientCommand = new Command((x) =>
            {
                DelClient();
            });
            DelVehiculeCommand = new Command((x) =>
            {
                DelVehicule();
            });

            UpdateCommand = new Command((x) =>
            {
                Update();
            });
            AddVehiculeCommand = new Command((x) =>
            {
                AddVehicule();
            });
            GoToRendezVousCommand = new Command((x) =>
            {
                GoToRendezVous();
            });
            Open();
        }


        #endregion

        #region Methods

        private void DelClient()
        {
            if (Client != null)
            {
                MessageBoxResult result = MessageBox.Show(string.Format(@"Êtes-vous sûr de supprimer ce client ?
                        Nom : {0}
                        Prénom : {1}", Client.Nom, Client.Prenom), "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result.Equals(MessageBoxResult.Yes))
                {
                    ClientManager.DeleteClient(Client);
                    Client = null;
                    SearchClientValue = string.Empty;
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un client.", "Attention", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void DelVehicule()
        {
            if (SelectedVehicule != null && SelectedVehicule.Client != null)
            {
                MessageBoxResult result = MessageBox.Show(string.Format(@"Êtes-vous sûr de supprimer ce véhicule ?
                        Client : {0} {1}
                        Véhicule : {2} {3}", SelectedVehicule.Client.Nom, SelectedVehicule.Client.Prenom,
                    SelectedVehicule.Marque, SelectedVehicule.Modele), "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result.Equals(MessageBoxResult.Yes))
                {
                    VehiculeManager.DesaffecterClient(SelectedVehicule);
                    Client.RemoveVehicule(SelectedVehicule);
                    SelectedVehicule = null;
                    
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un véhicule à supprimer.", "Attention", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void GoToRendezVous()
        {
            if (SelectedVehicule != null)
            {
                if (SelectedVehicule.Client != null)
                {
                    RendezVous rdv = new RendezVous(DateTime.Now, DureeType.UneHeure, SelectedVehicule, SelectedVehicule.Client);
                    //Close();
                    //new GestionRDV(mOwner, rdv).Show();
                }
                else
                {
                    //Pop up avertissant qu'il faut choisir un véhicule
                    MessageBox.Show("Veuillez choisir un véhicule.", "Attention", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                //Pop up avertissant qu'il faut choisir un véhicule
                MessageBox.Show("Veuillez choisir un véhicule.", "Attention", MessageBoxButton.OK, MessageBoxImage.Information);
            }
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

        private void Update()
        {
            if (SelectedVehicule != null)
            {
                MessageBoxResult result = MessageBox.Show("Êtes-vous sûr de modifier les informations de ce client ?", "Confirmation",
                    MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result.Equals(MessageBoxResult.Yes))
                {
                    ClientManager.UpdateClient(SelectedVehicule.Client);
                    VehiculeManager.UpdateVehicule(SelectedVehicule);
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un client.", "Attention", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        #endregion
    }
}
