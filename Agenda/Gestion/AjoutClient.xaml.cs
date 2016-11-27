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
    /// Logique d'interaction pour AjoutClient.xaml
    /// </summary>
    public partial class AjoutClient : Window
    {
        private UserControlNewClient ucChampsClient;

        private GestionClient mOwner;

        public AjoutClient(GestionClient owner)
        {
            mOwner = owner;
            mOwner.IsEnabled = false;
            mOwner.Opacity = 0.3;
            InitializeComponent();
            InitializeTitle();
            ucChampsClient = new UserControlNewClient();
            ChampsClient.Children.Add(ucChampsClient);
            Closed += new EventHandler(AjoutClient_Closed);
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

        private void AjoutClient_Closed(object sender, EventArgs e)
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


        private void Bp_Click(object sender, RoutedEventArgs e)
        {
            if (sender != null && sender is Button)
            {
                if ((sender as Button).Name == "BpAjouter")
                {
                    Client client = ucChampsClient.GetClientBrut();
                    if (ValidateClient(client))
                    {
                        Vehicule v = ucChampsClient.GetVehiculeBrut();
                        if (ValidateVehicule(v))
                        {
                            ClientManager.AddClient(client);
                            VehiculeManager.AddVehicule(v);
                            mOwner.RefreshClients();
                            base.Close();
                        }
                        else
                        {
                            MessageBoxResult result = MessageBox.Show("Le véhicule n'est pas saisi correctement.\nVoulez-vous tout de même ajouter le client sans le véhicule ?", "Confirmation",
                           MessageBoxButton.YesNo, MessageBoxImage.Warning);
                            if (result.Equals(MessageBoxResult.Yes))
                            {
                                ClientManager.AddClient(client);
                                mOwner.RefreshClients();
                                base.Close();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Les informations concernant le client sont incorrectes.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }

                }
                else if ((sender as Button).Name == "BpAnnuler")
                {
                    base.Close();
                }
            }
        }

        /// <summary>
        /// Permet de valider les champs du client
        /// </summary>
        /// <returns></returns>
        private bool ValidateClient(Client c)
        {
            bool ok = true;

            if (c.pNom.Length == 0)
            {
                ok = false;
            }else if (c.pTelephone1.Length == 0)
            {
                ok = false;
            }

            return ok;
        }

        /// <summary>
        /// Permet de valider les champs du véhicule
        /// </summary>
        /// <returns></returns>
        private bool ValidateVehicule(Vehicule v)
        {
            bool ok = true;

            if (v.pMarque.Length == 0)
            {
                ok = false;
            }
            else if (v.pModele.Length == 0)
            {
                ok = false;
            }
            
            if (ok && v.pImmatriculation.Length == 0)
            {
                int longNom = v.pClient.pNom.Length;
                v.pImmatriculation = string.Format("{0}{1}", 
                        v.pClient.pPrenom.Substring(0,2), v.pClient.pNom.Substring(0, longNom > 19 ? 19 : longNom));
            }

            return ok;
        }


    }
}
