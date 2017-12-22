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
using Agenda.ViewModels;

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

        private List<UserControlWork> myWorks = new List<UserControlWork>();

        #endregion

        #region Properties

        #endregion

        public GestionRDV(AgendaViewModel owner)
        {
            InitializeComponent();
            DataContext = new GestionRendezVousViewModel(this, owner);

            //st_LastWork = AllWorks;
            //initWorks();
            //sb_Works.Height = 300;
            //ActionVehicule.Visibility = System.Windows.Visibility.Collapsed;
        }
        
        //public GestionRDV(MainWindow owner, RendezVous rdv)
        //{
        //    mOwner = owner;
        //    mOwner.IsEnabled = false;
        //    mOwner.Opacity = 0.3;
        //    mRdv = rdv;
        //    InitializeComponent();
        //    InitializeTitle();
        //    InitializeComboBoxDuree();

        //    DatePickerRDV.SelectedDate = mRdv.Date;
        //    HourRDV.SelectedIndex = rdv.Date.Hour - 8;
        //    DurationRDV.SelectedIndex = rdv.getDuree();                   

        //    if (mRdv.Vehicule != null)
        //    {
        //        BtnNewClientPanel.Children.Clear();
        //        Le_Client.Children.Clear();
        //        mUcInfosClient = new UserControlInfosClient(this, mRdv.Vehicule);
        //        mUcNewClient = null;
        //        mUcSelectClient = null;
        //        Le_Client.Children.Add(mUcInfosClient);
        //        ActionVehicule.Visibility = System.Windows.Visibility.Visible;
        //    }
        //    else {
        //        mUcSelectClient = new UserControlSelectionClient();
        //        mUcNewClient = null;
        //        mUcInfosClient = null;
        //        Le_Client.Children.Clear();
        //        Le_Client.Children.Add(mUcSelectClient);
        //        ActionVehicule.Visibility = System.Windows.Visibility.Collapsed;
        //    }
        //    st_LastWork = AllWorks;
        //    initWorks();
        //    sb_Works.Height = 300;
        //    this.Activated += new EventHandler(GestionRDV_Activated);
        //}
        
        
        //private void ButtonVehicule_Click(object sender, RoutedEventArgs e)
        //{
        //    if (sender is Button)
        //    {
        //        Button bp = sender as Button;
        //        if (bp.Name == "AjouterVehicule")
        //        {
        //            Client c = null;
        //            if ((c = mRdv.Client) != null)
        //            {
        //                (Child = new GestionVehicule(this, c, false)).Show();
        //            }
        //        }
        //        else if (bp.Name == "ChangerVehicule")
        //        {
        //            Client c = null;
        //            if ((c = mRdv.Client) != null)
        //            {
        //                if (mRdv.Client.Vehicules.Count() > 1)
        //                {
        //                    (Child = new GestionVehicule(this, c, true)).Show();
        //                }
        //                else
        //                {
        //                    MessageBox.Show("Inutile, le client ne possède qu'une voiture.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
        //                }
        //            }
        //        }
                
        //    }
        //}

       
        //private void ButtonValider_Click(object sender, RoutedEventArgs e)
        //{
        //    if (HourRDV.SelectedValue == null)
        //    {
        //        MessageBox.Show("Veuillez entrer l'heure à laquelle vous souhaitez enregistrer le rendez-vous.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                
        //    }
        //    else
        //    {
        //        if ((mUcSelectClient != null && mUcSelectClient.GetVehicule() == null) || (mUcNewClient != null && mUcNewClient.GetVehicule() == null))
        //        {
        //            if (!mIsNewClient)
        //            {
        //                MessageBox.Show("Veuillez choisir un client puis un véhicule.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        //            }
        //            else
        //            {
        //                MessageBox.Show("Veuillez entrer les informations concernant le client et le véhicule.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        //            }
        //        }
        //        else
        //        {
        //            if (NoWorkSelected())
        //            {
        //                MessageBox.Show("Veuillez saisir les travaux à effectuer.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        //            }
        //            else
        //            {
        //                CreateOrUpdateRDV();
        //            }
        //        }
        //    }
        //}

        //private void CreateOrUpdateRDV()
        //{
        //    try
        //    {
        //        DateTime dt = (DateTime)DatePickerRDV.SelectedDate;
        //        dt = dt.AddHours(double.Parse((HourRDV.SelectedValue as ComboBoxItem).Content.ToString().Substring(0, 2)));
        //        if (mRdv != null)
        //        {
        //            mRdv.Date = dt;
        //            mRdv.Duree = getDureeType(DurationRDV.SelectedIndex);
        //            if (mRdv.Vehicule == null)
        //            {
        //                if (!mIsNewClient)
        //                {
        //                    mRdv.Vehicule = mUcSelectClient.GetVehicule();
        //                }
        //                else
        //                {
        //                    mRdv.Vehicule = mUcNewClient.GetVehicule();
        //                }
        //            }
        //        }
        //        else
        //        {
        //            mRdv = new RendezVous(dt, getDureeType(DurationRDV.SelectedIndex),
        //                mIsNewClient ? mUcNewClient.GetVehicule() : mUcSelectClient.GetVehicule(),
        //                mIsNewClient ? mUcNewClient.GetVehicule().Client : mUcSelectClient.GetVehicule().Client);
        //        }

        //        ReparationRDV work = null;
        //        mRdv.RemoveAllWorks();
        //        foreach (UserControlWork uc in myWorks)
        //        {
        //            if ((work = uc.getWork()) != null)
        //            {
        //                mRdv.addReparation(work);
        //            }
        //        }

        //        mRdv.Client = mRdv.Vehicule.Client;

        //        if (mRdv.Id != -1)
        //        {
        //            RdvManager.UpdateRdv(mRdv);
        //        }
        //        else
        //        {
        //            RdvManager.AddRdv(mRdv);
        //        }

        //        ClientManager.UpdateClient(mRdv.Client);
        //        VehiculeManager.UpdateVehicule(mRdv.Vehicule);

        //        //mOwner.RefreshAgenda();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Le rendez-vous n'a pas pu être enregistrer.\nErreur :" + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }
        //    finally
        //    {
        //        this.Close();
        //    }
        //}

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

        //private void initWorks()
        //{
        //    if (mRdv != null)
        //    {
                
        //        foreach (ReparationRDV rep in mRdv.Travaux)
        //        {
        //            StackPanel st_work = new StackPanel();
        //            nbWorks++;
        //            st_work.Orientation = Orientation.Horizontal;
        //            UserControlWork work = new UserControlWork(nbWorks, rep);
        //            myWorks.Add(work);
        //            st_work.Children.Add(work);
        //            st_LastWork.Children.Remove(Btn_AddWork);
        //            st_work.Children.Add(Btn_AddWork);
        //            st_LastWork = st_work;
        //            AllWorks.Children.Add(st_work);
        //        }
        //    }
        //    Add_Work(null, null);
        //}

        //private StackPanel createWork()
        //{
        //    StackPanel st_work = new StackPanel();
        //    nbWorks++;
        //    st_work.Orientation = Orientation.Horizontal;
        //    foreach (UserControlWork uc in myWorks)
        //    {
        //        uc.setDeleteBtn();
        //    }
            
        //    UserControlWork work = new UserControlWork(nbWorks, mRdv);
        //    myWorks.Add(work);
        //    st_work.Children.Add(work);
        //    st_LastWork.Children.Remove(Btn_AddWork);
        //    st_work.Children.Add(Btn_AddWork);
        //    st_LastWork = st_work;

        //    return st_work;

        //}

        //private void Add_Work(object sender, RoutedEventArgs e)
        //{
        //    AllWorks.Children.Add(createWork());
        //}

    }
}
