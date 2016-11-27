﻿using System;
using System.IO;
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
using System.Globalization;
using AgendaCore;
using Agenda.Gestion;
using AgendaBDDManager;
using Agenda.Consultation;
using System.Threading;

namespace Agenda
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Attributes

        private string[] horaires = new string[] { "8h", "9h", "10h", "11h", "12h", "13h", "14h", "15h", "16h", "17h", "18h" };

        /// <summary>
        /// Les mois de l'année.
        /// </summary>
        private string[] months = new string[] {"", "Janvier", "Février", "Mars", "Avril", "Mai", "Juin", "Juillet", "Août",
            "Septembre", "Octobre", "Novembre", "Décembre" };

        private GregorianCalendar calendrier = new GregorianCalendar(GregorianCalendarTypes.MiddleEastFrench);

        private List<DateTime> jours_feries = new List<DateTime>();

        private List<int> col_feries = new List<int>();

        /// <summary>
        /// Ce dictionnaire permettra de traduire les jours de la semaine (Anglais => Français)
        /// </summary>
        private Dictionary<string, string> days = new Dictionary<string, string>();

        /// <summary>
        /// Le jour choisi pour consulter l'agenda
        /// </summary>
        private DateTime day = DateTime.Now;

        private DateTime dateStart;

        private DateTime dateEnd;

        private List<RendezVous> mListeRDV = new List<RendezVous>();

        private List<UserControlRDV> listeUC = new List<UserControlRDV>();

        private RendezVous selectedRDV = null; 

        #endregion

        #region Contructors

        public MainWindow()
        {
            //Migration1to2.doMigration();
            InitializeComponent();
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            this.DataContext = this;
            try
            {
                Connexion.init();
                InitializeTitle();
                InitializeDays();
                CalendarToChoice.SelectedDate = day;
            }
            catch (Exception e)
            {
                MessageBox.Show(this, "Une erreur est survenue lors du démarrage.\n" + e.Message, "Fatal error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.None);
                Application.Current.Shutdown();
            }
        }



        private void InitializeTitle()
        {
            int nbRDV = RdvManager.CountRdvDay(DateTime.Now);
            WindowTitle.Text = String.Format("Agenda - {0} - {1} Rendez-vous", DateTime.Now.ToShortDateString(), nbRDV);
            Btn_ClosePrincipale.MouseDown += new MouseButtonEventHandler(Btn_ClosePrincipale_MouseDown);
            Btn_MinimizePrincipale.MouseDown += new MouseButtonEventHandler(Btn_MinimizePrincipale_MouseDown);
            myWindowHeadBar.MouseLeftButtonDown += new MouseButtonEventHandler(myWindowHeadBar_MouseLeftButtonDown);
        }


        #endregion

        #region Operations

        /// <summary>
        /// Permet d'ajouter un rdv sur l'agenda
        /// </summary>
        /// <param name="rdv"></param>
        private void AddRendezVous(RendezVous rdv)
        {           
                UserControlRDV ucRDV = new UserControlRDV(this, rdv);
                listeUC.Add(ucRDV);
                ucRDV.MouseLeftButtonUp += new MouseButtonEventHandler(border_MouseLeftButtonUp);
                // Les lignes suiventes permettent de retrouver un élément grâce à son nom (car il a été généré par programmation)
                object st = Agenda.FindName(string.Format("Row{0}_Col{1}", getRowForRDV(rdv), getColumnForRDV(rdv)));
                if (st is StackPanel)
                {
                    (st as StackPanel).Children.Add(ucRDV);
                }
        }

        /// <summary>
        /// Permet de définir la ligne d'un rdv
        /// </summary>
        /// <param name="rdv">Le rendez-vous</param>
        /// <returns>La ligne de l'agenda</returns>
        private int getRowForRDV(RendezVous rdv)
        {
            int row = 1;
            row = rdv.pDate.Hour - 7;
            return row;
        }

        /// <summary>
        /// Permet de définir la colonne d'un rdv
        /// </summary>
        /// <param name="rdv">Le rendez-vous</param>
        /// <returns>La colonne de l'agenda</returns>
        private int getColumnForRDV(RendezVous rdv)
        {
            int col = 1;
            List<string> jours = days.Keys.ToList();
            col = jours.IndexOf(calendrier.GetDayOfWeek(rdv.pDate).ToString()) + 1;
            return col;
        }

        /// <summary>
        /// Permet d'initialiser les heures sur l'agenda
        /// </summary>
        private void InitialiseHours()
        {
            int hour = 8;
            TextBlock t = null;
            for (int i = 1; i < 12; ++i)
            {
                t = new TextBlock();
                t.Text = string.Format("{0,2:00}:00", hour++);
                t.TextAlignment = TextAlignment.Center;
                t.FontSize = 13;
                t.FontWeight = FontWeights.Bold;
                t.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                t.Foreground = Brushes.Black;
                Grid.SetColumn(t, 0);
                Grid.SetRow(t, i);
                Agenda.Children.Add(t);
            }
        }

        /// <summary>
        /// Permet d'initialiser l'agenda.
        /// </summary>
        private void InitializeAgenda()
        {
            DateTime from = day.AddDays(-7);
            DateTime to = day.AddDays(7);
            mListeRDV.Clear();
            mListeRDV.AddRange(RdvManager.getRDVFromDateToDate(from,to));
            listeUC.Clear();
            Agenda.Children.Clear();
            InitialiseHours();

            col_feries.Clear();

            DateTime d = GetCalendarToDatetime(day);
            dateStart = new DateTime(d.Ticks);

            TextBlock t = null;

            for(int i = 1; i < 6; ++i)
            {
                t = new TextBlock();
                t.Name = string.Format("Agenda_{0}", i);
                t.Foreground = Brushes.Black;
                try
                {
                    Agenda.UnregisterName(t.Name);
                }
                catch (Exception)
                { }

                try
                {
                    Agenda.RegisterName(t.Name, t);
                }
                catch (Exception)
                { }

                t.Text = d.ToLongDateString();
                t.TextAlignment = TextAlignment.Center;
                t.FontSize = 13;
                t.FontWeight = FontWeights.Bold;
                Grid.SetRow(t, 0);
                Grid.SetColumn(t, i);
                Agenda.Children.Add(t);

                if (jours_feries.Any(x => x.Day == d.Day && x.Month == d.Month))
                {
                    col_feries.Add(i);
                }

                d = d.AddDays(1);
            }

            InitializeBoxForRDV();

            dateEnd = new DateTime(d.Ticks).AddHours(20.0);

            CalendarToChoice.SelectedDate = day;

            foreach (RendezVous r in mListeRDV)
            {
                if (r.pDate.CompareTo(dateStart) >= 0 && r.pDate.CompareTo(dateEnd) <= 0)
                {
                    AddRendezVous(r);
                }
            }
        }

        /// <summary>
        /// Initialise les stackPanel qui vont permettre d'afficher les rendez-vous
        /// </summary>
        private void InitializeBoxForRDV()
        {
            for (int i = 1; i < 6; ++i) // Toutes les colonnes
            {
                for (int j = 1; j < 12; ++j) // Toutes les lignes
                {
                    ScrollViewer scRDV = new ScrollViewer();
                    scRDV.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
                    StackPanel st = new StackPanel();
                    st.Name = string.Format("Row{0}_Col{1}", j, i); // Nom au format "row_col"
                    
                    if (col_feries.Contains(i))
                    {
                        st.Background = Brushes.DarkRed;
                        st.IsEnabled = false;
                    }
                    else
                    {
                        st.IsEnabled = true;
                        st.Background = Brushes.Transparent;
                    }

                    st.MouseDown += new MouseButtonEventHandler(st_MouseDown);
                    try
                    {
                        Agenda.UnregisterName(st.Name);
                    }
                    catch (Exception)
                    { }

                    try
                    {
                        Agenda.RegisterName(st.Name, st);
                    }
                    catch (Exception)
                    { }
                    scRDV.Content = st;
                    Grid.SetColumn(scRDV, i);
                    Grid.SetRow(scRDV, j);
                    st.Children.Clear();
                    Agenda.Children.Add(scRDV);
                }
            }
        }


        /// <summary>
        /// Lorsqu'une case est double cliqué, on peut ajouter un rdv à la date et l'heure où se situe le clique
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void st_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ResetRendezVousStateOnAgenda();

            if (e.ClickCount == 2)
            {
                if (sender is StackPanel)
                {
                    if (!UserControlRDV.IsMouseDownOnRdv)
                    {
                        StackPanel st = sender as StackPanel;

                        int hour = int.Parse(st.Name.Split('_')[0].Substring(3)) + 7;
                        int col = int.Parse(st.Name.Split('_')[1].Substring(3));
                        DateTime date = dateStart.AddDays(col - 1);
                        date = date.AddHours(hour);

                        RendezVous rdv = new RendezVous(date, DureeType.UneHeure, null, null);

                        new GestionRDV(this, rdv).Show();
                    }
                }
            }
        }


        private DateTime GetCalendarToDatetime(DateTime day)
        {
            day = day.AddDays(getIndexOfDaysOfWeeks(calendrier.GetDayOfWeek(day).ToString()) * -1);

            return calendrier.ToDateTime(day.Year,
                                         day.Month,
                                         day.Day,
                                         0, 0, 0, 0);
        }

        /// <summary>
        /// Permet d'initialiser le dictionnaire des jours de la semaine.
        /// </summary>
        private void InitializeDays()
        {
            days.Add("Monday", "Lundi");
            days.Add("Tuesday", "Mardi");
            days.Add("Wednesday", "Mercredi");
            days.Add("Thursday", "Jeudi");
            days.Add("Friday", "Vendredi");
            days.Add("Saturday", "Samedi");
            days.Add("Sunday", "Dimanche");

            //Les jours férriés
            jours_feries.Add(new DateTime(2014, 1, 1));
            jours_feries.Add(new DateTime(2014, 5, 1));
            jours_feries.Add(new DateTime(2014, 5, 8));
            jours_feries.Add(new DateTime(2014, 7, 14));
            jours_feries.Add(new DateTime(2014, 8, 15));
            jours_feries.Add(new DateTime(2014, 11, 1));
            jours_feries.Add(new DateTime(2014, 11, 11));
            jours_feries.Add(new DateTime(2014, 12, 25));
        }

        /// <summary>
        /// Permet d'obtenir l'index d'un jour. (de 0 à 6)
        /// </summary>
        /// <param name="day">Le jour de la semaine en anglais</param>
        /// <returns>un chiffre de 0 à 6</returns>
        private int getIndexOfDaysOfWeeks(string day)
        {
            return days.Keys.ToList().IndexOf(day);
        }

        #endregion

        #region events

        /// <summary>
        /// Permet d'afficher les informations d'un rendez-vous lorsqu'on clique dessus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (sender is UserControlRDV)
            {
                ResetRendezVousStateOnAgenda();
                noRDVselected.Visibility = System.Windows.Visibility.Collapsed;
                RDVselected.Visibility = System.Windows.Visibility.Visible; 
                Btn_DeleteRDV.Visibility = System.Windows.Visibility.Visible;
                UserControlRDV uc = sender as UserControlRDV;
                uc.RdvBorder.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#002860")); 
                selectedRDV = uc.Rdv;
                foreach (object t in uc.RDVText.Children)
                {
                    if (t is TextBlock)
                    {
                        (t as TextBlock).Foreground = Brushes.WhiteSmoke;
                    }
                }
                Details_Client.Text = string.Format("M. ou Mme {0}", uc.Rdv.pVehicule.pClient.pNom); //uc.Rdv.pVehicule.pClient.ToString();
                Details_Vehicule.Text = uc.Rdv.pVehicule.ToString();
                Details_Jour.Text = string.Format("{0} {1} {2} {3}", days[calendrier.GetDayOfWeek(uc.Rdv.pDate).ToString()], uc.Rdv.pDate.Day,
                                                                        months[uc.Rdv.pDate.Month], uc.Rdv.pDate.Year);
                Details_Heure.Text = uc.Rdv.pDate.ToLongTimeString();
                if (uc.Rdv.pTravaux.Count() > 0)
                {
                    StringBuilder sb = new StringBuilder();//uc.Rdv.pTravaux.ElementAt(0).pReparation.pNom);
                    for (int i = 0; i < uc.Rdv.pTravaux.Count(); ++i)
                    {
                        sb.Append("- ").AppendLine(uc.Rdv.pTravaux.ElementAt(i).pReparation.pNom);
                    }
                    Details_Travaux.Text = sb.ToString();
                }
                UserControlRDV.IsMouseDownOnRdv = false;
            }
        }

        /// <summary>
        /// Lorsque la date est changée
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CalendarToChoice_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            day = new DateTime(CalendarToChoice.SelectedDate != null ? CalendarToChoice.SelectedDate.Value.Ticks : day.Ticks);
            ResetDetailsRendezVous();
            InitializeAgenda();
        }

        /// <summary>
        /// Lorsqu'on clique sur un bouton
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button)
            {
                Button bp = sender as Button;
                if (bp.Name == "AjoutRDV")
                {
                    new GestionRDV(this).Show();
                }
                else if (bp.Name == "LesClients")
                {
                    new GestionClient(this).Show();
                }
                else if (bp.Name == "Save")
                {
                    System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
                    if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        string path = fbd.SelectedPath;
                        Thread thSaver = new Thread(new ParameterizedThreadStart(Connexion.Sauvegarde));
                        thSaver.Start(path);
                    }
                }
                else if (bp.Name == "Works")
                {
                    new Travaux_Vehicule(this).Show();
                }
                else if (bp.Name == "Refresh")
                {
                    Raffraichir();
                }
            }
        }

        private void Raffraichir()
        {
            InitializeTitle();
            RefreshAgenda();
        }

        /// <summary>
        /// Cette méthode permet de réinitialisé l'état (cliqué/non cliqué) d'un rdv sur l'agenda
        /// </summary>
        private void ResetRendezVousStateOnAgenda()
        {
            foreach (UserControlRDV u in listeUC)
            {
                u.initializeStyle();
            }
            ResetDetailsRendezVous();
        }

        /// <summary>
        /// Permet de remettre à zéro les détails d'un rendez vous
        /// </summary>
        private void ResetDetailsRendezVous()
        {
            Details_Client.Text = string.Empty;
            Details_Vehicule.Text = string.Empty;
            Details_Jour.Text = string.Empty;
            Details_Heure.Text = string.Empty;
            Details_Travaux.Text = string.Empty;
            RDVselected.Visibility = System.Windows.Visibility.Collapsed;
            noRDVselected.Visibility = System.Windows.Visibility.Visible;
            Btn_DeleteRDV.Visibility = System.Windows.Visibility.Collapsed;
        }



        void Btn_DeleteRDV_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (selectedRDV != null)
            {
                MessageBoxResult result = MessageBox.Show("Êtes-vous sûr de supprimer ce rendez-vous ?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result.Equals(MessageBoxResult.Yes))
                {
                    //TODO : Delete_Reparation_Rdv

                    ReparationRDVManager.DeleteReparationRDVByRDV(selectedRDV);
                    RdvManager.DeleteRdv(selectedRDV.pId);
                    RefreshAgenda();
                }
            }
        }

        void myWindowHeadBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        void Btn_MinimizePrincipale_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Principale.WindowState = System.Windows.WindowState.Minimized;
        }

        void Btn_ClosePrincipale_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        #endregion

        /// <summary>
        /// Permet de raffraîchir l'agenda lorsqu'on se trouve sur une autre fenêtre
        /// </summary>
        public void RefreshAgenda()
        {
            CalendarToChoice_SelectedDatesChanged(null, null);
        }


    }
}
