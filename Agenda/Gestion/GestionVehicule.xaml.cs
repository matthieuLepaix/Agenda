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

        private Client mClient;

        private GestionClient mOwner;

        #endregion

        #region Properties

        #endregion


        #region Constructors

        public GestionVehicule()
        {
            InitializeComponent();
        }

        public GestionVehicule(GestionClient owner, Client c)
        {
            InitializeComponent();
            InitializeTitle();
            mOwner = owner;
            mOwner.IsEnabled = false;
            mOwner.Opacity = 0.3;
            mClient = c;
            ChampsVehicule.Header = string.Format("Véhicule de M. {0} {1}", mClient.pNom, mClient.pPrenom);
            Closed += new EventHandler(GestionVehicule_Closed);
        }

        private void InitializeTitle()
        {
            WindowTitle.Text = "Gestion d'un véhicle";
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
            mOwner.IsEnabled = true;
            mOwner.Opacity = 1;
            //if (mOwner is MainWindow)
            //{
            //    mOwner.WindowState = System.Windows.WindowState.Maximized;
            //}
            //else
            //{
                mOwner.WindowState = System.Windows.WindowState.Normal;
            //}
        }

        #endregion

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
                        Ce véhicule appartient à M. {0} {1}
                        Voulez-vous attribuer ce véhicule à M. {2} {3}?", vehicules.First().pClient.pNom, vehicules.First().pClient.pPrenom,  
                                                                        mClient.pNom, mClient.pPrenom), "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);
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
            string nom = mClient.pNom;
            int longNom = nom.Length;
            int nbVehicule = mClient.pVehicules.Count();
            Immat.Text = string.Format("{0}{1}_{2}", mClient.pPrenom.Substring(0, 2), nom.Substring(0, longNom > 19 ? 19 : longNom), nbVehicule);
        }

        private void AddVehiculeToDB()
        {
            VehiculeManager.AddVehicule(Marque.Text.Trim(), Modele.Text.Trim(), Immat.Text.Trim(), Annee.Text.Trim(), Int32.Parse(km.Text.Trim()), mClient);
            mOwner.RefreshClients();
            base.Close();
        }

        private void UpdateVehiculeToDB(Vehicule v)
        {
            v.pClient.RemoveVehicule(v);
            v.pClient = mClient;
            v.pClient.AddVehicule(v);
            v.pMarque = Marque.Text.Trim();
            v.pModele = Modele.Text.Trim();
            v.pImmatriculation = Immat.Text.Trim();
            VehiculeManager.UpdateVehicule(v);
            mOwner.RefreshClients();
            base.Close();
        }

        
    }
}
