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
    public class AddClientViewModel : AbstractViewModel
    {
        #region Attributes
        private Window parent;
        private GestionClientsViewModel clientsViewModel;
        private string windowTitle;
        private Vehicule vehicule;
        private Command addCommand;
        private Command cancelCommand;
        #endregion

        #region Properties

        public string WindowTitle
        {
            get
            {
                return windowTitle;
            }
            set
            {
                windowTitle = value;
                OnPropertyChanged("WindowTitle");
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

        public Command AddCommand
        {
            get
            {
                return addCommand;
            }
            set
            {
                addCommand = value;
                OnPropertyChanged("AddCommand");
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
        #endregion

        #region Constructors
        public AddClientViewModel(Window view, GestionClientsViewModel owner)
            : base(view, owner)
        {
            View = view;
            clientsViewModel = owner;
            WindowTitle = "Enregistrement d'un nouveau client";
            Vehicule = new Vehicule();
            AddCommand = new Command((x) =>
            {
                Add((Window)x);
            });
            CancelCommand = new Command((x) =>
            {
                Cancel((Window)x);
            });
            Open();
        }
        #endregion

        #region Methods
        private void Add(Window w)
        {
            if (ValidateClient())
            {
                if (ValidateVehicule())
                {
                    ClientManager.AddClient(Vehicule.Client);
                    VehiculeManager.AddVehicule(Vehicule);
                    ((GestionClientsViewModel)Owner).SearchClientValue = string.Empty;
                    Close();
                }
                else
                {
                    MessageBoxResult result = MessageBox.Show("Le véhicule n'est pas saisi correctement.\nVoulez-vous tout de même ajouter le client sans le véhicule ?", "Confirmation",
                   MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (result.Equals(MessageBoxResult.Yes))
                    {
                        ClientManager.AddClient(Vehicule.Client);
                        ((GestionClientsViewModel)Owner).SearchClientValue = string.Empty;
                        Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("Les informations concernant le client sont incorrectes.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void Cancel(Window w)
        {
            Close();
        }

        /// <summary>
        /// Permet de valider les champs du client
        /// </summary>
        /// <returns></returns>
        private bool ValidateClient()
        {
            if (Vehicule.Client.Nom.Length == 0 || Vehicule.Client.Telephone1.Length == 0)
                return false;
            return true;
        }

        /// <summary>
        /// Permet de valider les champs du véhicule
        /// </summary>
        /// <returns></returns>
        private bool ValidateVehicule()
        {
            if (Vehicule.Marque.Length == 0 || Vehicule.Modele.Length == 0 || Vehicule.Immatriculation.Length == 0)
                return false;
            return true;
        }
        #endregion
    }
}
