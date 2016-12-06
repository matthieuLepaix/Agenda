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
using AgendaCore;
using AgendaBDDManager;

namespace Agenda.Gestion
{
    /// <summary>
    /// Logique d'interaction pour GestionVehicule.xaml
    /// </summary>
    public partial class GestionVehicule : Window
    {
        #region Attributes

        private GestionClient mOwnerClient = null;

        private GestionRDV mOwnerRDV = null;

        #endregion

        #region Properties

        public Client pClient
        {
            get;
            set;
        }
        #endregion


        #region Constructors

        public GestionVehicule()
        {
            InitializeComponent();
        }

        public GestionVehicule(Window owner, Client c, bool change)
        {
            InitializeComponent();
            InitializeTitle();
            if (owner is GestionClient)
            {
                mOwnerClient = owner as GestionClient;
                mOwnerClient.IsEnabled = false;
                mOwnerClient.Opacity = 0.3;
            }
            else if (owner is GestionRDV)
            {
                mOwnerRDV = owner as GestionRDV;
                mOwnerRDV.IsEnabled = false;
                mOwnerRDV.Opacity = 0.3;
            }
            pClient = c;
            Closed += new EventHandler(GestionVehicule_Closed);
            if (change)
            {
                ChangeVehicule.ItemsSource = pClient.pVehicules;
                DPAjoutVehicule.Visibility = System.Windows.Visibility.Collapsed;
                DPChangerVehicule.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                DPChangerVehicule.Visibility = System.Windows.Visibility.Collapsed;
                DPAjoutVehicule.Visibility = System.Windows.Visibility.Visible;
            }
        }



        private void InitializeTitle()
        {
            WindowTitle.Text = string.Format("Véhicules de {0}", pClient);
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

        void GestionVehicule_Closed(object sender, EventArgs e)
        {
            if (mOwnerClient != null)
            {
                mOwnerClient.IsEnabled = true;
                mOwnerClient.Opacity = 1;
                mOwnerClient.WindowState = System.Windows.WindowState.Normal;
            }
            else if (mOwnerRDV != null)
            {
                mOwnerRDV.IsEnabled = true;
                mOwnerRDV.Opacity = 1;
                mOwnerRDV.WindowState = System.Windows.WindowState.Normal;
            }
        }

        #endregion

        private void BpChange_Click(object sender, RoutedEventArgs e)
        {
            if (ChangeVehicule.SelectedItem != null)
            {
                mOwnerRDV.refreshVehicule(ChangeVehicule.SelectedItem as Vehicule);
                base.Close();
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un véhicule.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void BpAjouter_Click(object sender, RoutedEventArgs e)
        {
            if (Marque.Text.Trim().Length < 2 || Modele.Text.Trim().Length < 1)
            {
                MessageBox.Show("Les informations sont incorrectes", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else {

                if (Immat.Text.Trim().Length == 0)
                {
                    FormatImmat();
                }
                // Ajouter le véhicule à la base de données
                List<Vehicule> vehicules = new List<Vehicule>();
                if ((vehicules = VehiculeManager.VEHICULES.FindAll(v => v.pImmatriculation.Equals(Immat.Text.Trim()))).Count > 0)
                {
                    MessageBoxResult result = MessageBox.Show(string.Format(@"L'immatriculation du véhicule existe déjà !
                        Ce véhicule appartient à {0} 
                        Voulez-vous attribuer ce véhicule à {1}?", vehicules.First().pClient, pClient), "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (result.Equals(MessageBoxResult.Yes))
                    {
                        UpdateVehiculeToDB(vehicules.First());
                    }
                }
                else
                {
                    AddVehiculeToDB();
                }
            }

        }

        private void FormatImmat()
        {
            string nom = pClient.pNom;
            int longNom = nom.Length;
            int nbVehicule = pClient.pVehicules.Count();
            Immat.Text = string.Format("{0}{1}_{2}", pClient.pPrenom.Substring(0, 2), nom.Substring(0, longNom > 19 ? 19 : longNom), nbVehicule);
        }

        private void AddVehiculeToDB()
        {
            Vehicule v = new Vehicule(Marque.Text.Trim(), Modele.Text.Trim(), Immat.Text.Trim(), Annee.Text.Trim(), Int32.Parse(km.Text.Trim()), pClient);
            VehiculeManager.AddVehicule(v);
            if(mOwnerClient != null)
            {
                mOwnerClient.RefreshClients();
            }else if(mOwnerRDV != null)
            {
                mOwnerRDV.refreshVehicule(v);
            }
            base.Close();
        }

        private void UpdateVehiculeToDB(Vehicule v)
        {
            v.pClient.RemoveVehicule(v);
            v.pClient = pClient;
            v.pClient.AddVehicule(v);
            v.pMarque = Marque.Text.Trim();
            v.pModele = Modele.Text.Trim();
            v.pImmatriculation = Immat.Text.Trim();
            VehiculeManager.UpdateVehicule(v);
            if (mOwnerClient != null)
            {
                mOwnerClient.RefreshClients();
            }
            else if (mOwnerRDV != null)
            {
                mOwnerRDV.refreshVehicule(v);
            }
            base.Close();
        }

        
    }
}
