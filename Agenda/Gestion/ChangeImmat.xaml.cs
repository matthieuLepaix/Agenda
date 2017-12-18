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
using Agenda.UserControls;

namespace Agenda.Gestion
{
    /// <summary>
    /// Logique d'interaction pour ChangeImmat.xaml
    /// </summary>
    public partial class ChangeImmat : Window
    {
        private Vehicule mVehicule;

        private UserControlInfosClient mInfosClient;

        private Window mOwner;

        public ChangeImmat(Window owner, UserControlInfosClient infosClient, Vehicule vehicule)
        {
            InitializeComponent();
            InitializeTitle();
            mInfosClient = infosClient;
            mOwner = owner;
            mOwner.Opacity = 0.3;
            mVehicule = vehicule;
            old_immat.Text = mVehicule.Immatriculation;
            Closed += new EventHandler(ChangeImmat_Closed);
        }

        private void InitializeTitle()
        {
            WindowTitle.Text = "Changement de l'immatriculation";
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

        void ChangeImmat_Closed(object sender, EventArgs e)
        {
            mOwner.Opacity = 1;
            if (mOwner is MainWindow)
            {
                mOwner.WindowState = System.Windows.WindowState.Maximized;
            }
            else
            {
                mOwner.WindowState = System.Windows.WindowState.Normal;
            }
            Close();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (e.Source == Confirm)
            {
                string immat = new_immat.Text.Trim();
                if (immat.Equals(new_immat2.Text.Trim()))
                {
                    mVehicule.Immatriculation = immat;
                    VehiculeManager.UpdateVehicule(mVehicule);
                    if (mInfosClient != null)
                    {
                        mInfosClient.ImmatVehicule.Text = immat;
                    }
                    Close();
                }
                else
                {
                    MessageBox.Show("Attention, les deux champs de la nouvelle immatriculation ne sont pas identiques.", "Erreur", 
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else if (e.Source == Cancel)
            {
                Close();
            }
        }
    }
}
