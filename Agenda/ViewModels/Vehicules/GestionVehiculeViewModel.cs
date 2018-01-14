using Agenda.Utils;
using AgendaBDDManager;
using AgendaCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Agenda.ViewModels
{
    public class GestionVehiculeViewModel : AbstractViewModel
    {
        #region Attributes
        private const string title = "Gestion des véhicules";
        private bool isVehiculeChange;
        private Client client;
        private Vehicule vehicule;
        private Command addVehiculeCommand;
        private Command changeVehiculeCommand;
        #endregion

        #region Properties
        public bool IsVehiculeChange
        {
            get
            {
                return isVehiculeChange;
            }
            set
            {
                isVehiculeChange = value;
                OnPropertyChanged("IsVehiculeChange");
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
        public Command ChangeVehiculeCommand
        {
            get
            {
                return changeVehiculeCommand;
            }
            set
            {
                changeVehiculeCommand = value;
                OnPropertyChanged("ChangeVehiculeCommand");
            }
        }
        #endregion

        #region Constructors
        public GestionVehiculeViewModel(Window view, AbstractViewModel owner)
            : base(view, owner, title)
        {
            IsVehiculeChange = Owner is GestionRendezVousViewModel;
            if (IsVehiculeChange)
            {
                Client = ((GestionRendezVousViewModel)Owner).RendezVous.Client;
                WindowTitle = string.Format("Changement de véhicule du RDV de M. {0} le {1}", Client.Nom, ((GestionRendezVousViewModel)Owner).RendezVous.Date.ToShortDateString());
            }
            else
            {
                Client = ((GestionClientsViewModel)Owner).Client;
                Vehicule = new Vehicule(string.Empty, string.Empty, string.Empty, string.Empty, 0, Client);
                WindowTitle = string.Format("Ajout d'un véhicule pour M. {0}", Client.Nom);
            }
            AddVehiculeCommand = new Command((x) =>
            {
                AddVehicule();
            });
            ChangeVehiculeCommand = new Command((x) =>
            {
                ChangeVehicule();
            });
            Open();
        }
        #endregion

        #region Methods

        private void AddVehicule()
        {
            if (Vehicule.Marque.Length < 2 || Vehicule.Modele.Length < 1)
            {
                MessageBox.Show("Les informations sont incorrectes", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            if (Vehicule.Immatriculation == string.Empty)
                FormatImmat();

            // Ajouter le véhicule à la base de données
            List<Vehicule> vehicules = new List<Vehicule>();
            if ((vehicules = VehiculeManager.VEHICULES.ToList().FindAll(v => v.Immatriculation.Equals(Vehicule.Immatriculation))).Count > 0)
            {
                var existing = vehicules.First();
                MessageBoxResult result = MessageBox.Show(string.Format(@"L'immatriculation du véhicule existe déjà !
                        Ce véhicule appartient à {0} 
                        Voulez-vous attribuer ce véhicule à {1}?", existing.Client, Client), "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result.Equals(MessageBoxResult.Yes))
                {
                    Vehicule.Id = existing.Id;
                    Client.Vehicules.RemoveAll(v => v.Immatriculation == Vehicule.Immatriculation);
                    Client.Vehicules.Add(Vehicule);
                    ClientManager.UpdateClient(Client);
                    VehiculeManager.UpdateVehicule(Vehicule);
                    ClientManager.UpdateClient(Client);
                }else
                {
                    return;
                }
            }
            else
            {
                VehiculeManager.AddVehicule(Vehicule);
            }

            if (!IsVehiculeChange)
            {
                ((GestionClientsViewModel)Owner).RefreshClientAfterChange();
                ((GestionClientsViewModel)Owner).SelectedVehicule = Vehicule;
            }

            Close();

        }
        private void FormatImmat()
        {
            string nom = Client.Nom;
            int longNom = nom.Length;
            int nbVehicule = Client.Vehicules.Count();
            Vehicule.Immatriculation = string.Format("{0}{1}_{2}", Client.Prenom.Substring(0, 2), nom.Substring(0, longNom > 19 ? 19 : longNom), nbVehicule);
        }

        private void ChangeVehicule()
        {
            if (Vehicule != null)
            {
                ((GestionRendezVousViewModel)Owner).ChangeVehicule(Vehicule);
                Close();
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un véhicule.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
        #endregion
    }
}
