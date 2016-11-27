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
using System.Collections.ObjectModel;
using AgendaCore;
using AgendaBDDManager;
using Agenda.UserControls;

namespace Agenda.Gestion
{
    /// <summary>
    /// Logique d'interaction pour GestionClient.xaml
    /// </summary>
    public partial class GestionClient : Window
    {
        private MainWindow mOwner;
        private UserControlSelectionClient mUcSelectClients;
        private UserControlInfosClient mUcSelectedClient;

        public GestionClient(MainWindow owner)
        {
            mOwner = owner;
            mOwner.IsEnabled = false;
            mOwner.Opacity = 0.3;
            InitializeComponent();
            InitializeTitle();
            mUcSelectClients = new UserControlSelectionClient(this);
            Clients.Children.Clear();
            Clients.Children.Add(mUcSelectClients);

            mUcSelectedClient = new UserControlInfosClient(this, null);
            Le_Client.Children.Clear();
            Le_Client.Children.Add(mUcSelectedClient);
        }

        private void InitializeTitle()
        {
            WindowTitle.Text = "Gestion des clients";
            Btn_ClosePrincipale.MouseDown += new MouseButtonEventHandler(Btn_ClosePrincipale_MouseDown);
            Btn_MinimizePrincipale.MouseDown += new MouseButtonEventHandler(Btn_MinimizePrincipale_MouseDown);
            myWindowHeadBar.MouseLeftButtonDown += new MouseButtonEventHandler(myWindowHeadBar_MouseLeftButtonDown);
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender != null && sender is Button)
            {
                Button bp = sender as Button;
                if (bp.Name == "Ajouter")
                {
                    new AjoutClient(this).Show();
                }
                else if (bp.Name == "SupprimerClient")
                {
                    Client c = mUcSelectClients.GetClient();

                    if (c != null)
                    {
                        MessageBoxResult result = MessageBox.Show(string.Format(@"Êtes-vous sûr de supprimer ce client ?
                        Nom : {0}
                        Prénom : {1}", c.pNom, c.pPrenom), "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                        if (result.Equals(MessageBoxResult.Yes))
                        {
                            ClientManager.DeleteClient(c);
                            RefreshClients();
                            RefreshChampsClient();
                            mOwner.RefreshAgenda();
                        }
                    }
                }
                else if (bp.Name == "SupprimerVehicule")
                {
                    Vehicule v = mUcSelectClients.GetVehicule();

                    if (v != null)
                    {
                        MessageBoxResult result = MessageBox.Show(string.Format(@"Êtes-vous sûr de supprimer ce véhicule ?
                        Client : {0} {1}
                        Véhicule : {2} {3}", v.pClient.pNom, v.pClient.pPrenom, v.pMarque, v.pModele), "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                        if (result.Equals(MessageBoxResult.Yes))
                        {
                            VehiculeManager.DeleteVehicule(v.pId);
                            v.pClient.RemoveVehicule(v);
                            RefreshClients();
                            RefreshChampsClient();
                            mOwner.RefreshAgenda();
                        }
                    }
                }else if (bp.Name == "AjouterVehicule")
                {
                    Client c = null;
                    if ((c = mUcSelectClients.GetClient()) != null)
                    {
                        new GestionVehicule(this, c).Show();
                    }
                }
                else if (bp.Name == "AjouterRDV")
                {
                    Vehicule v = null;
                    if ((v = mUcSelectClients.GetVehicule()) != null)
                    {
                        if (v.pClient != null)
                        {
                            RendezVous rdv = new RendezVous(DateTime.Now, DureeType.UneHeure, v, v.pClient);
                            Close();
                            new GestionRDV(mOwner, rdv).Show();
                        }
                        else
                        {
                            //Pop up avertissant qu'il faut choisir un véhicule
                            MessageBox.Show("Veuillez choisir un véhicule.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        }
                    }
                    else
                    {
                        //Pop up avertissant qu'il faut choisir un client
                        MessageBox.Show("Veuillez choisir un client.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                }
                else if (bp.Name == "ValidModif")
                {
                    Vehicule v = null;
                    if ((v = mUcSelectedClient.GetVehicule()) != null)
                    {
                        MessageBoxResult result = MessageBox.Show("Êtes-vous sûr de modifier les informations de ce client ?", "Confirmation",
                            MessageBoxButton.YesNo, MessageBoxImage.Warning);
                        if (result.Equals(MessageBoxResult.Yes))
                        {
                            ClientManager.UpdateClient(v.pClient);
                            VehiculeManager.UpdateVehicule(v);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void RefreshChampsClient()
        {
            mUcSelectedClient.resetFields();
        }


        public void SetClientSelected(Client c)
        {
            SetVehiculeSelected(new Vehicule(null,null,null,null,0,c));
        }

        public void SetVehiculeSelected(Vehicule v)
        {
            mUcSelectedClient.SetVehicule(v);
        }

        public void RefreshClients()
        {
            mUcSelectClients.RefreshClients();
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
            if (mOwner is MainWindow)
            {
                mOwner.WindowState = System.Windows.WindowState.Maximized;
            }
            else
            {
                mOwner.WindowState = System.Windows.WindowState.Normal;
            }

        }

    }
}
