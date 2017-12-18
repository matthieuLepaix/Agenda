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
using System.Windows.Threading;

namespace Agenda.Gestion
{
    /// <summary>
    /// Logique d'interaction pour GestionRDV.xaml
    /// </summary>
    public partial class GestionRDV : Window
    {

        #region Attributes

        private int nbWorks = 0;

        // useful to get the parent of the btn add work
        private StackPanel st_LastWork;

        private MainWindow mOwner;

        public Window Child = null;

        private RendezVous mRdv;

        private UserControlNewClient mUcNewClient;

        private UserControlSelectionClient mUcSelectClient;

        private UserControlInfosClient mUcInfosClient;

        private bool mIsNewClient;

        private List<UserControlWork> myWorks = new List<UserControlWork>();

        #endregion

        #region Properties

        public RendezVous pRdv
        {
            get
            {
                return mRdv;
            }
            set
            {
                mRdv = value;
            }
        }

        #endregion

        public GestionRDV(MainWindow owner)
        {
            mOwner = owner;
            mOwner.IsEnabled = false;
            mOwner.Opacity = 0.3;
            InitializeComponent();
            InitializeTitle();
            InitializeComboBoxDuree();
            DatePickerRDV.SelectedDate = DateTime.Now;
            HourRDV.SelectedIndex = 0;

            mUcSelectClient = new UserControlSelectionClient();
            mUcNewClient = null;
            mUcInfosClient = null;
            Le_Client.Children.Clear();
            Le_Client.Children.Add(mUcSelectClient);


            st_LastWork = AllWorks;
            initWorks();
            sb_Works.Height = 300;
            ActionVehicule.Visibility = System.Windows.Visibility.Collapsed;
            this.Activated += new EventHandler(GestionRDV_Activated);
        }

        void GestionRDV_Activated(object sender, EventArgs e)
        {
            if (Child != null)
            {
                Child.Activate();
            }
        }

        public GestionRDV(MainWindow owner, RendezVous rdv)
        {
            mOwner = owner;
            mOwner.IsEnabled = false;
            mOwner.Opacity = 0.3;
            mRdv = rdv;
            InitializeComponent();
            InitializeTitle();
            InitializeComboBoxDuree();

            DatePickerRDV.SelectedDate = mRdv.Date;
            HourRDV.SelectedIndex = rdv.Date.Hour - 8;
            DurationRDV.SelectedIndex = rdv.getDuree();                   

            if (mRdv.Vehicule != null)
            {
                BtnNewClientPanel.Children.Clear();
                Le_Client.Children.Clear();
                mUcInfosClient = new UserControlInfosClient(this, mRdv.Vehicule);
                mUcNewClient = null;
                mUcSelectClient = null;
                Le_Client.Children.Add(mUcInfosClient);
                ActionVehicule.Visibility = System.Windows.Visibility.Visible;
            }
            else {
                mUcSelectClient = new UserControlSelectionClient();
                mUcNewClient = null;
                mUcInfosClient = null;
                Le_Client.Children.Clear();
                Le_Client.Children.Add(mUcSelectClient);
                ActionVehicule.Visibility = System.Windows.Visibility.Collapsed;
            }
            st_LastWork = AllWorks;
            initWorks();
            sb_Works.Height = 300;
            this.Activated += new EventHandler(GestionRDV_Activated);
        }

        private void InitializeTitle()
        {
            WindowTitle.Text = "Gestion d'un rendez-vous";
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

        private void InitializeComboBoxDuree()
        {
            DurationRDV.Items.Add("30 minutes");
            DurationRDV.Items.Add("1 heure");
            DurationRDV.Items.Add("2 heures");
            DurationRDV.Items.Add("3 heures");
            DurationRDV.Items.Add("4 heures");
            DurationRDV.Items.Add("5 heures");
            DurationRDV.Items.Add("6 heures");
            DurationRDV.Items.Add("7 heures");
            DurationRDV.Items.Add("8 heures");
            DurationRDV.Items.Add("Plus de 8 heures"); 
            DurationRDV.SelectedIndex = 1;

        }
        
        private void ButtonVehicule_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button)
            {
                Button bp = sender as Button;
                if (bp.Name == "AjouterVehicule")
                {
                    Client c = null;
                    if ((c = mRdv.Client) != null)
                    {
                        (Child = new GestionVehicule(this, c, false)).Show();
                    }
                }
                else if (bp.Name == "ChangerVehicule")
                {
                    Client c = null;
                    if ((c = mRdv.Client) != null)
                    {
                        if (mRdv.Client.Vehicules.Count() > 1)
                        {
                            (Child = new GestionVehicule(this, c, true)).Show();
                        }
                        else
                        {
                            MessageBox.Show("Inutile, le client ne possède qu'une voiture.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                }
                
            }
        }

        public void refreshVehicule(Vehicule v)
        {
            mRdv.Vehicule = v;
            mUcInfosClient.SetVehicule(v);
        }
        
        private void ButtonValider_Click(object sender, RoutedEventArgs e)
        {
            if (HourRDV.SelectedValue == null)
            {
                MessageBox.Show("Veuillez entrer l'heure à laquelle vous souhaitez enregistrer le rendez-vous.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                
            }
            else
            {
                if ((mUcSelectClient != null && mUcSelectClient.GetVehicule() == null) || (mUcNewClient != null && mUcNewClient.GetVehicule() == null))
                {
                    if (!mIsNewClient)
                    {
                        MessageBox.Show("Veuillez choisir un client puis un véhicule.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                    else
                    {
                        MessageBox.Show("Veuillez entrer les informations concernant le client et le véhicule.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                }
                else
                {
                    if (NoWorkSelected())
                    {
                        MessageBox.Show("Veuillez saisir les travaux à effectuer.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                    else
                    {
                        CreateOrUpdateRDV();
                    }
                }
            }
        }

        private void CreateOrUpdateRDV()
        {
            try
            {
                DateTime dt = (DateTime)DatePickerRDV.SelectedDate;
                dt = dt.AddHours(double.Parse((HourRDV.SelectedValue as ComboBoxItem).Content.ToString().Substring(0, 2)));
                if (mRdv != null)
                {
                    mRdv.Date = dt;
                    mRdv.Duree = getDureeType(DurationRDV.SelectedIndex);
                    if (mRdv.Vehicule == null)
                    {
                        if (!mIsNewClient)
                        {
                            mRdv.Vehicule = mUcSelectClient.GetVehicule();
                        }
                        else
                        {
                            mRdv.Vehicule = mUcNewClient.GetVehicule();
                        }
                    }
                }
                else
                {
                    mRdv = new RendezVous(dt, getDureeType(DurationRDV.SelectedIndex),
                        mIsNewClient ? mUcNewClient.GetVehicule() : mUcSelectClient.GetVehicule(),
                        mIsNewClient ? mUcNewClient.GetVehicule().Client : mUcSelectClient.GetVehicule().Client);
                }

                ReparationRDV work = null;
                mRdv.RemoveAllWorks();
                foreach (UserControlWork uc in myWorks)
                {
                    if ((work = uc.getWork()) != null)
                    {
                        mRdv.addReparation(work);
                    }
                }

                mRdv.Client = mRdv.Vehicule.Client;

                if (mRdv.Id != -1)
                {
                    RdvManager.UpdateRdv(mRdv);
                }
                else
                {
                    RdvManager.AddRdv(mRdv);
                }

                ClientManager.UpdateClient(mRdv.Client);
                VehiculeManager.UpdateVehicule(mRdv.Vehicule);

                //mOwner.RefreshAgenda();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Le rendez-vous n'a pas pu être enregistrer.\nErreur :" + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                this.Close();
            }
        }

        private bool NoWorkSelected()
        {
            bool ok = true;

            foreach(UserControlWork uc in myWorks)
            {
                ok = ok && uc.getWork() == null;
            }

            return ok;
        }

        private DureeType getDureeType(int p)
        {
            DureeType d = DureeType.EtPlus;

            if (p == 0)
            {
                d = DureeType.UneDemiHeure;
            }
            else if (p == 1)
            {
                d = DureeType.UneHeure;
            }
            else if (p == 2)
            {
                d = DureeType.DeuxHeures;
            }
            else if (p == 3)
            {
                d = DureeType.TroisHeures;
            }
            else if (p == 4)
            {
                d = DureeType.QuatreHeures;
            }
            else if (p == 5)
            {
                d = DureeType.CinqHeures;
            }
            else if (p == 6)
            {
                d = DureeType.SixHeures;
            }
            else if (p == 7)
            {
                d = DureeType.SeptHeures;
            }
            else if (p == 8)
            {
                d = DureeType.HuitHeures;
            }
            return d;
        }


        void myWindowHeadBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void ButtonFacture_Click(object sender, RoutedEventArgs e)
        {
            CreateOrUpdateRDV();
            //mOwner.InitFacture(mRdv);
            mOwner.selectTabIndex(MainWindow.TabOptions.FACTURE);
            this.Close();
        }


        private void ButtonAnnuler_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            //UserControlRDV.IsMouseDownOnRdv = false;
            mOwner.IsEnabled = true;
            mOwner.Opacity = 1;
            mOwner.WindowState = System.Windows.WindowState.Maximized;
            //mOwner.Child = null;
            mOwner.Activate();
        }

        private void BtnNewClient_Click(object sender, RoutedEventArgs e)
        {
            mUcSelectClient = null;
            mUcNewClient = new UserControlNewClient();
            BtnNewClientPanel.Children.Clear();
            Le_Client.Children.Clear();
            Le_Client.Children.Add(mUcNewClient);
            mIsNewClient = true;
        }

        private void initWorks()
        {
            if (mRdv != null)
            {
                
                foreach (ReparationRDV rep in mRdv.Travaux)
                {
                    StackPanel st_work = new StackPanel();
                    nbWorks++;
                    st_work.Orientation = Orientation.Horizontal;
                    UserControlWork work = new UserControlWork(nbWorks, rep);
                    myWorks.Add(work);
                    st_work.Children.Add(work);
                    st_LastWork.Children.Remove(Btn_AddWork);
                    st_work.Children.Add(Btn_AddWork);
                    st_LastWork = st_work;
                    AllWorks.Children.Add(st_work);
                }
            }
            Add_Work(null, null);
        }

        private StackPanel createWork()
        {
            StackPanel st_work = new StackPanel();
            nbWorks++;
            st_work.Orientation = Orientation.Horizontal;
            foreach (UserControlWork uc in myWorks)
            {
                uc.setDeleteBtn();
            }
            
            UserControlWork work = new UserControlWork(nbWorks, mRdv);
            myWorks.Add(work);
            st_work.Children.Add(work);
            st_LastWork.Children.Remove(Btn_AddWork);
            st_work.Children.Add(Btn_AddWork);
            st_LastWork = st_work;

            return st_work;

        }

        private void Add_Work(object sender, RoutedEventArgs e)
        {
            AllWorks.Children.Add(createWork());
        }

    }
}
