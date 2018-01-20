using System;
using System.Windows;
using System.Windows.Controls;
using Agenda.ViewModels;
using System.Windows.Media;

namespace Agenda
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Global

        public enum TabOptions
        {
            AGENDA = 0,
            FACTURE = 1,
            DEVIS = 2
        }

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new AgendaViewModel(this, null);
            //Params.DataContext = paramsVM;
        }

        //private void GenerateAnalysis()
        //{
        //    CoreAnalysis.LoadRDVAnalysis();
        //    CoreAnalysis.LoadClientAnalysis();
        //    YearChart.DataContext = RDVAnalysis.byYear.OrderBy(x => x.Key);
        //    var dataSourceMonthList = new List<List<KeyValuePair<string, int>>>();
        //    int i = 0;
        //    RDVAnalysis.byMonth.Select(x => x.Key).Distinct().ToList().ForEach(x =>
        //    {
        //        if (int.Parse(x) > (DateTime.Now.Year - 3))
        //        {
        //            (MonthChart.Series[i++] as LineSeries).Title = x;
        //            dataSourceMonthList.Add(RDVAnalysis.byMonth.Where(x2 => x2.Key == x).Select(v => v.Value).ToList());
        //        }
        //    });
        //    MonthChart.DataContext = dataSourceMonthList;
        //    //WeekChart.DataContext = RDVAnalysis.byWeek;

        //    //ClientChart.DataContext = ClientAnalysis.byClients.OrderByDescending(x => x.Value).Take(5);
        //    //MarqueChart.DataContext = ClientAnalysis.byVehicules.OrderByDescending(x => x.Value).Take(5);

        //    var years = RDVAnalysis.byYear.Select(x => x.Key).ToList();
        //    years.Sort();
        //    YearToAnalysis.DataContext = years;
        //    YearToAnalysis.SelectionChanged += YearToAnalysis_SelectionChanged;
        //}

        private void YearToAnalysis_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        

        public void selectTabIndex(TabOptions index)
        {
            Tabs.SelectedIndex = (int)index;
        }

        #endregion

        //#region Devis

        //private UserControlSelectionClient mUCDevisClient;

        //private int nbWorksDevis = 0;

        //private List<UserControlWork> myWorksDevis = new List<UserControlWork>();

        //// useful to get the parent of the btn add work
        //private StackPanel st_LastWorkDevis;

        //public void InitDevis(object sender, RoutedEventArgs e)
        //{
        //    nbWorksDevis = 0;
        //    if (myWorksDevis != null)
        //    {
        //        myWorksDevis.RemoveAll(x => true);
        //    }
        //    initTypeReglementDevis();
        //    if (st_LastWorkDevis != null)
        //    {
        //        st_LastWorkDevis.Children.Remove(Devis_Btn_AddWork);
        //    }
        //    st_LastWorkDevis = Devis_AllWorks;
        //    st_LastWorkDevis.Children.Clear();


        //    mUCDevisClient = new UserControlSelectionClient();
        //    mUCDevisClient.FontSize = 12;
        //    Devis_Le_Client.Children.Clear();
        //    Devis_Le_Client.Children.Add(mUCDevisClient);

        //    Add_WorkDevis(null, null);

        //    for (int i = 0; i < 5; ++i)
        //    {
        //        var tb = this.FindName(string.Format("Devis_MO{0}", i + 1));
        //        if (tb is TextBox)
        //        {
        //            (tb as TextBox).Text = 0.ToString("n2");
        //        }
        //    }
        //    Devis_TypeReglement.SelectedIndex = 0;

        //    Devis.Visibility = System.Windows.Visibility.Visible;
        //}

        //private void initTypeReglementDevis()
        //{
        //    Devis_TypeReglement.Items.Clear();
        //    Devis_TypeReglement.Items.Add(AgendaCore.Facture.getReglementFromEnum(AgendaCore.Facture.Reglement.CB));
        //    Devis_TypeReglement.Items.Add(AgendaCore.Facture.getReglementFromEnum(AgendaCore.Facture.Reglement.Cheque));
        //    Devis_TypeReglement.Items.Add(AgendaCore.Facture.getReglementFromEnum(AgendaCore.Facture.Reglement.Especes));
        //}

        //private void Add_WorkDevis(object sender, RoutedEventArgs e)
        //{
        //    Devis_AllWorks.Children.Add(createWorkDevis());
        //}

        //private StackPanel createWorkDevis()
        //{
        //    StackPanel st_work = new StackPanel();
        //    nbWorksDevis++;
        //    st_work.Orientation = Orientation.Horizontal;
        //    foreach (UserControlWork uc in myWorksDevis)
        //    {
        //        uc.setDeleteBtn();
        //    }
        //    Panel parent = Devis_Btn_AddWork.Parent as Panel;
        //    if (parent != null)
        //    {
        //        parent.Children.Remove(Devis_Btn_AddWork);
        //    }
        //    UserControlWork work = new UserControlWork(nbWorksDevis);
        //    myWorksDevis.Add(work);
        //    st_work.Children.Add(work);
        //    st_LastWorkDevis.Children.Remove(Devis_Btn_AddWork);
        //    st_work.Children.Add(Devis_Btn_AddWork);
        //    st_LastWorkDevis = st_work;

        //    return st_work;
        //}

        //private void Devis_Click(object sender, RoutedEventArgs e)
        //{
        //    if (sender is Button)
        //    {
        //        Button bp = sender as Button;
        //        if (bp.Name == "Devis_ToExcel")
        //        {
        //            if (MessageBox.Show("Etes-vous sûr de vouloir éditer ce devis?", "Information",
        //                        MessageBoxButton.YesNoCancel, MessageBoxImage.Question) == MessageBoxResult.Yes)
        //            {
        //                EnregistrerDevis(true, false);
        //            }
        //        }
        //        else if (bp.Name == "Devis_Imprimer")
        //        {
        //            if (MessageBox.Show("Etes-vous sûr de vouloir imprimer ce devis?", "Information",
        //                           MessageBoxButton.YesNoCancel, MessageBoxImage.Question) == MessageBoxResult.Yes)
        //            {
        //                EnregistrerDevis(true, true);
        //            }
        //        }
        //        else if (bp.Name == "Devis_Enregistrer")
        //        {
        //            if (MessageBox.Show("Etes-vous sûr de vouloir enregistrer ce devis?", "Information",
        //                        MessageBoxButton.YesNoCancel, MessageBoxImage.Question) == MessageBoxResult.Yes)
        //            {
        //                EnregistrerDevis(false, false);
        //            }
        //        }
        //    }
        //}

        //private void EnregistrerDevis(bool excel, bool imprimer)
        //{
        //    var vehicule = mUCDevisClient.GetVehicule();
        //    if (vehicule != null)
        //    {
        //        var reps = new List<ReparationRDV>();
        //        foreach (UserControlWork uc in myWorksDevis)
        //        {
        //            var work = uc.getWork();
        //            if (work != null)
        //            {
        //                reps.Add(work);
        //            }
        //        }
        //        var devis = new Devis(vehicule, reps, GetMainOeuvresDevis(), AgendaCore.Facture.getReglementFromString((string)Devis_TypeReglement.SelectedValue));
        //        if (excel)
        //        {
        //            ExcelManager.GenerateExcelDevis(devis, imprimer);
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show("Veuillez sélectionner un client et son véhicule", "Error",
        //                                    MessageBoxButton.OK, MessageBoxImage.Error);
        //    }

        //}

        //private List<float> GetMainOeuvresDevis()
        //{
        //    List<float> MOs = new List<float>();
        //    if (!string.IsNullOrEmpty(Devis_MO1.Text))
        //    {
        //        try
        //        {
        //            MOs.Add(float.Parse(Devis_MO1.Text));
        //        }
        //        catch (Exception)
        //        {
        //            MessageBox.Show("Attention, Le forfait MO 1 est incorrect.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        //        }
        //    }
        //    if (!string.IsNullOrEmpty(Devis_MO2.Text))
        //    {
        //        try
        //        {
        //            MOs.Add(float.Parse(Devis_MO2.Text));
        //        }
        //        catch (Exception)
        //        {
        //            MessageBox.Show("Attention, Le forfait MO 2 est incorrect.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        //        }
        //    }
        //    if (!string.IsNullOrEmpty(Devis_MO3.Text))
        //    {
        //        try
        //        {
        //            MOs.Add(float.Parse(Devis_MO3.Text));
        //        }
        //        catch (Exception)
        //        {
        //            MessageBox.Show("Attention, Le forfait MO 3 est incorrect.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        //        }
        //    }
        //    if (!string.IsNullOrEmpty(Devis_MO4.Text))
        //    {
        //        try
        //        {
        //            MOs.Add(float.Parse(Devis_MO4.Text));
        //        }
        //        catch (Exception)
        //        {
        //            MessageBox.Show("Attention, Le forfait MO 4 est incorrect.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        //        }
        //    }
        //    if (!string.IsNullOrEmpty(Devis_MO5.Text))
        //    {
        //        try
        //        {
        //            MOs.Add(float.Parse(Devis_MO5.Text));
        //        }
        //        catch (Exception)
        //        {
        //            MessageBox.Show("Attention, Le forfait MO 5 est incorrect.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        //        }
        //    }
        //    return MOs;
        //}
        //#endregion

        //#region Facture

        //private UserControlInfosClient mUCFactureClient;

        //private int nbWorks = 0;

        //private List<UserControlWork> myWorks = new List<UserControlWork>();

        //// useful to get the parent of the btn add work
        //private StackPanel st_LastWork;

        //private RendezVous mFactureRDV;

        //private static Facture mFacture;

        //public void InitFacture(RendezVous rdv)
        //{
        //    mFactureRDV = rdv;
        //    nbWorks = 0;
        //    if (myWorks != null)
        //    {
        //        myWorks.RemoveAll(x => true);
        //    }
        //    InitTypeReglementFacture();
        //    if (st_LastWork != null)
        //    {
        //        st_LastWork.Children.Remove(Facture_Btn_AddWork);
        //    }
        //    st_LastWork = Facture_AllWorks;
        //    st_LastWork.Children.Clear();

        //    if (rdv != null)
        //    {
        //        mUCFactureClient = new UserControlInfosClient(this, mFactureRDV.pVehicule);
        //        Facture_Le_Client.Children.Clear();
        //        Facture_Le_Client.Children.Add(mUCFactureClient);

        //        foreach (ReparationRDV rep in mFactureRDV.pTravaux)
        //        {
        //            StackPanel st_work = new StackPanel();
        //            nbWorks++;
        //            st_work.Orientation = Orientation.Horizontal;
        //            UserControlWork work = new UserControlWork(nbWorks, rep);
        //            myWorks.Add(work);
        //            st_work.Children.Add(work);
        //            st_LastWork.Children.Remove(Facture_Btn_AddWork);
        //            st_work.Children.Add(Facture_Btn_AddWork);
        //            st_LastWork = st_work;
        //            Facture_AllWorks.Children.Add(st_work);
        //        }
        //    }
        //    Add_Work(null, null);

        //    if ((mFacture = FactureManager.GetFactureByRdv(mFactureRDV.pId)) != null)
        //    {
        //        for (int i = 0; i < mFacture.pMainOeuvres.Count; ++i)
        //        {
        //            var tb = this.FindName(string.Format("Facture_MO{0}", i + 1));
        //            if (tb is TextBox)
        //            {
        //                (tb as TextBox).Text = mFacture.pMainOeuvres[i].ToString("n2");
        //            }
        //        }

        //        Facture_TypeReglement.SelectedIndex = (int)mFacture.pReglement;
        //    }
        //    else
        //    {
        //        for (int i = 0; i < 5; ++i)
        //        {
        //            var tb = this.FindName(string.Format("Facture_MO{0}", i + 1));
        //            if (tb is TextBox)
        //            {
        //                (tb as TextBox).Text = 0.ToString("n2");
        //            }
        //        }
        //        Facture_TypeReglement.SelectedIndex = 0;

        //    }
        //    Facture_SearchRDV.Visibility = Visibility.Collapsed;
        //    Facture.Visibility = System.Windows.Visibility.Visible;
        //}

        //private void InitTypeReglementFacture()
        //{
        //    Facture_TypeReglement.Items.Clear();
        //    Facture_TypeReglement.Items.Add(AgendaCore.Facture.getReglementFromEnum(AgendaCore.Facture.Reglement.CB));
        //    Facture_TypeReglement.Items.Add(AgendaCore.Facture.getReglementFromEnum(AgendaCore.Facture.Reglement.Cheque));
        //    Facture_TypeReglement.Items.Add(AgendaCore.Facture.getReglementFromEnum(AgendaCore.Facture.Reglement.Especes));
        //}

        //private void Add_Work(object sender, RoutedEventArgs e)
        //{
        //    Facture_AllWorks.Children.Add(CreateWorkFacture());
        //}

        //private void Facture_Fermer(object sender, RoutedEventArgs e)
        //{
        //    Facture.Visibility = Visibility.Collapsed;
        //    Facture_SearchRDV.Visibility = Visibility.Visible;
        //}

        //private StackPanel CreateWorkFacture()
        //{
        //    StackPanel st_work = new StackPanel();
        //    nbWorks++;
        //    st_work.Orientation = Orientation.Horizontal;
        //    foreach (UserControlWork uc in myWorks)
        //    {
        //        uc.setDeleteBtn();
        //    }
        //    Panel parent = Facture_Btn_AddWork.Parent as Panel;
        //    if (parent != null)
        //    {
        //        parent.Children.Remove(Facture_Btn_AddWork);
        //    }
        //    UserControlWork work = new UserControlWork(nbWorks, mFactureRDV);
        //    myWorks.Add(work);
        //    st_work.Children.Add(work);
        //    st_LastWork.Children.Remove(Facture_Btn_AddWork);
        //    st_work.Children.Add(Facture_Btn_AddWork);
        //    st_LastWork = st_work;

        //    return st_work;
        //}


        //private void Facture_Click(object sender, RoutedEventArgs e)
        //{
        //    if (sender is Button)
        //    {
        //        Button bp = sender as Button;
        //        if (bp.Name == "Facture_ToExcel")
        //        {
        //            if (MessageBox.Show("Etes-vous sûr de vouloir éditer cette facture?", "Information",
        //                        MessageBoxButton.YesNoCancel, MessageBoxImage.Question) == MessageBoxResult.Yes)
        //            {
        //                EnregistrerFacture(true, false);
        //            }
        //        }
        //        else if (bp.Name == "Facture_Imprimer")
        //        {
        //            if (MessageBox.Show("Etes-vous sûr de vouloir imprimer cette facture?", "Information",
        //                           MessageBoxButton.YesNoCancel, MessageBoxImage.Question) == MessageBoxResult.Yes)
        //            {
        //                EnregistrerFacture(true, true);
        //            }
        //        }
        //        else if (bp.Name == "Facture_Enregistrer")
        //        {
        //            if (MessageBox.Show("Etes-vous sûr de vouloir enregistrer cette facture?", "Information",
        //                        MessageBoxButton.YesNoCancel, MessageBoxImage.Question) == MessageBoxResult.Yes)
        //            {
        //                EnregistrerFacture(false, false);
        //            }
        //        }
        //    }
        //}

        //private void EnregistrerFacture(bool excel, bool imprimer)
        //{
        //    if (mFactureRDV.pClient != null)
        //    {
        //        CreateOrUpdateFactureRDV();
        //        mFacture = new AgendaCore.Facture(0, AgendaCore.Facture.getReglementFromString((string)Facture_TypeReglement.SelectedValue), GetMainOeuvresFacture(), 0, mFactureRDV);
        //        Facture factureTemp = FactureManager.GetFactureByRdv(mFactureRDV.pId);
        //        if (factureTemp == null)
        //        {
        //            FactureManager.AddFacture(mFacture);
        //        }
        //        else
        //        {
        //            if (MessageBox.Show("La facture existe déjà pour ce rendez-vous. Voulez-vous enregistrer les modifications apportées à celle-ci?", "Information",
        //                                    MessageBoxButton.YesNoCancel, MessageBoxImage.Question) == MessageBoxResult.Yes)
        //            {
        //                mFacture.pId = factureTemp.pId;
        //                FactureManager.UpdateFacture(mFacture);
        //            }
        //        }
        //        if (excel)
        //        {
        //            ExcelManager.GenerateExcelFacture(mFacture, imprimer);
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show("La facture est invalide. Le client n'est plus renseigné pour ce rendez-vous.", "Error",
        //                                    MessageBoxButton.OK, MessageBoxImage.Error);
        //    }
        //}

        //private void CreateOrUpdateFactureRDV()
        //{
        //    try
        //    {
        //        if (mFactureRDV != null)
        //        {
        //            //Met à jour le client et le véhicule
        //            mFactureRDV.pVehicule = mUCFactureClient.GetVehicule();
        //        }

        //        ReparationRDV work = null;
        //        mFactureRDV.RemoveAllWorks();
        //        foreach (UserControlWork uc in myWorks)
        //        {
        //            if ((work = uc.getWork()) != null)
        //            {
        //                mFactureRDV.addReparation(work);
        //            }
        //        }

        //        mFactureRDV.pClient = mFactureRDV.pVehicule.pClient;

        //        if (mFactureRDV.pId != -1)
        //        {
        //            RdvManager.UpdateRdv(mFactureRDV);
        //        }
        //        else
        //        {
        //            RdvManager.AddRdv(mFactureRDV);
        //        }

        //        ClientManager.UpdateClient(mFactureRDV.pClient);
        //        VehiculeManager.UpdateVehicule(mFactureRDV.pVehicule);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Le rendez-vous n'a pas pu être enregistrer.\nErreur :" + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }
        //}

        //private List<float> GetMainOeuvresFacture()
        //{
        //    List<float> MOs = new List<float>();
        //    if (!string.IsNullOrEmpty(Facture_MO1.Text))
        //    {
        //        try
        //        {
        //            MOs.Add(float.Parse(Facture_MO1.Text));
        //        }
        //        catch (Exception)
        //        {
        //            MessageBox.Show("Attention, Le forfait MO 1 est incorrect.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        //        }
        //    }
        //    if (!string.IsNullOrEmpty(Facture_MO2.Text))
        //    {
        //        try
        //        {
        //            MOs.Add(float.Parse(Facture_MO2.Text));
        //        }
        //        catch (Exception)
        //        {
        //            MessageBox.Show("Attention, Le forfait MO 2 est incorrect.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        //        }
        //    }
        //    if (!string.IsNullOrEmpty(Facture_MO3.Text))
        //    {
        //        try
        //        {
        //            MOs.Add(float.Parse(Facture_MO3.Text));
        //        }
        //        catch (Exception)
        //        {
        //            MessageBox.Show("Attention, Le forfait MO 3 est incorrect.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        //        }
        //    }
        //    if (!string.IsNullOrEmpty(Facture_MO4.Text))
        //    {
        //        try
        //        {
        //            MOs.Add(float.Parse(Facture_MO4.Text));
        //        }
        //        catch (Exception)
        //        {
        //            MessageBox.Show("Attention, Le forfait MO 4 est incorrect.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        //        }
        //    }
        //    if (!string.IsNullOrEmpty(Facture_MO5.Text))
        //    {
        //        try
        //        {
        //            MOs.Add(float.Parse(Facture_MO5.Text));
        //        }
        //        catch (Exception)
        //        {
        //            MessageBox.Show("Attention, Le forfait MO 5 est incorrect.", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        //        }
        //    }
        //    return MOs;
        //}

        //void Btn_FactureRDV_MouseUp(object sender, MouseButtonEventArgs e)
        //{
        //    if (selectedRDV != null)
        //    {
        //        InitFacture(selectedRDV);
        //        selectTabIndex(TabOptions.FACTURE);
        //    }
        //}
        //#endregion

    }
}
