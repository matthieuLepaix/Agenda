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
using System.Windows.Navigation;
using System.Windows.Shapes;
using AgendaCore;
using System.Collections.ObjectModel;
using AgendaBDDManager;

namespace Agenda.UserControls
{
    /// <summary>
    /// Logique d'interaction pour UserControlSelectionClient.xaml
    /// </summary>
    public partial class UserControlSelectionClient : UserControl
    {
        public List<Client> listeClients = new List<Client>();

        private ObservableCollection<Client> listeTriee = new ObservableCollection<Client>();

        private Gestion.GestionClients mOwnerGestion;

        private Consultation.Travaux_Vehicule mOwnerTravaux;

        public UserControlSelectionClient()
        {
            InitializeComponent();
            this.DataContext = this;
            Clients.ItemsSource = listeTriee;
            RefreshClients();
        }

        public UserControlSelectionClient(Gestion.GestionClients owner) : this()
        {
            mOwnerGestion = owner;
            Clients.SelectionChanged += new SelectionChangedEventHandler(GestionClients_SelectionChanged);
            //Vehicules_Client.SelectionChanged += new SelectionChangedEventHandler(GestionVehicules_Client_SelectionChanged);
        }

        public UserControlSelectionClient(Consultation.Travaux_Vehicule owner) : this()
        {
            mOwnerTravaux = owner;
            Clients.SelectionChanged += new SelectionChangedEventHandler(TravauxClients_SelectionChanged);
            //Vehicules_Client.SelectionChanged += new SelectionChangedEventHandler(TravauxVehicules_Client_SelectionChanged);
        }

        void GestionVehicules_Client_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //mOwnerGestion.SetVehiculeSelected(GetVehicule());
        }

        void GestionClients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //mOwnerGestion.SetClientSelected(GetClient());
        }

        void TravauxVehicules_Client_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            mOwnerTravaux.SetVehiculeSelected(GetVehicule());
        }

        void TravauxClients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            mOwnerTravaux.SetClientSelected(GetClient());
        }



        /// <summary>
        /// Permet de raffraichir la liste des clients
        /// </summary>
        public void RefreshClients()
        {
            listeClients.Clear();
            listeTriee.Clear();
            listeClients.AddRange(ClientManager.CLIENTS.OrderBy(c => c.Nom));
            AddListIntoListeTriee(listeClients);
        }

        /// <summary>
        /// Ajoute une liste de clients à la liste triée
        /// </summary>
        /// <param name="l">La liste à ajouter</param>
        private void AddListIntoListeTriee(List<Client> l)
        {
            foreach (Client c in l)
            {
                listeTriee.Add(c);
            }
        }

        private void Champs_Nom_KeyUp(object sender, KeyEventArgs e)
        {
            listeTriee.Clear();
            string valeur = Champs_Nom.Text.Trim();
            if (valeur.Length > 0)
            {
                AddListIntoListeTriee(listeClients.Where(c => c.Nom.ToUpper().StartsWith(valeur.ToUpper())).ToList());
            }
            else
            {
                AddListIntoListeTriee(listeClients);
            }
        }

        public Vehicule GetVehicule()
        {
            return null;// Vehicules_Client.SelectedItem as Vehicule;
        }

        public Client GetClient()
        {
            return Clients.SelectedItem as Client;
        }

        public void setClient(Client c)
        {
            Clients.SelectedItem = c;
        }

        public void unselect()
        {
            Clients.UnselectAll();
        }
    }
}
