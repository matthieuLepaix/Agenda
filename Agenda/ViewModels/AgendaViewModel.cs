using Agenda.Consultation;
using Agenda.Gestion;
using Agenda.Utils;
using AgendaBDDManager;
using Agenda.UserControls;
using AgendaCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;

namespace Agenda.ViewModels
{
    public class AgendaViewModel : AbstractViewModel
    {
        #region Attributes
        private const string title = "Agenda 2018";

        private double maxHeight;

        public static DateTime SelectedDateForConverter;

        // Les boutons d'actions
        private string addRdvTextButton;
        private string clientsTextButton;
        private string saveTextButton;
        private string worksTextButton;
        private string refreshImgPath;

        private Command addRdvCommand;
        private Command clientsCommand;
        private Command saveCommand;
        private Command worksCommand;
        private Command refreshCommand;

        private Command detailsDeleteCommand;
        private Command detailsFactureCommand;

        //Agenda
        private ObservableCollection<RendezVous> rendezVousList = new ObservableCollection<RendezVous>();
        private ObservableCollection<UserControlRDV> userControlRendezVousList = new ObservableCollection<UserControlRDV>();
        private RendezVous selectedRendezVous = null;
        private DateTime selectedDate;
        private Command doubleClickUserControlCommand;
        private Command singleClickAgendaCommand;


        #endregion

        #region Properties

        public double MaxHeight
        {
            get
            {
                return maxHeight;
            }
            set
            {
                maxHeight = value;
                OnPropertyChanged("MaxHeight");
            }
        }

        public string AddRdvTextButton
        {
            get
            {
                return addRdvTextButton;
            }
            set
            {
                addRdvTextButton = value;
                OnPropertyChanged("AddRdvTextButton");
            }
        }
        public string ClientsTextButton
        {
            get
            {
                return clientsTextButton;
            }
            set
            {
                clientsTextButton = value;
                OnPropertyChanged("ClientsTextButton");
            }
        }
        public string SaveTextButton
        {
            get
            {
                return saveTextButton;
            }
            set
            {
                saveTextButton = value;
                OnPropertyChanged("SaveTextButton");
            }
        }
        public string WorksTextButton
        {
            get
            {
                return worksTextButton;
            }
            set
            {
                worksTextButton = value;
                OnPropertyChanged("WorksTextButton");
            }
        }
        public string RefreshImgPath
        {
            get
            {
                return refreshImgPath;
            }
            set
            {
                refreshImgPath = value;
                OnPropertyChanged("RefreshImgPath");
            }
        }
        /// <summary>
        /// Commande pour ouvrir la modal permettant de créer un rendez-vous
        /// </summary>
        public Command AddRdvCommand
        {
            get
            {
                return addRdvCommand;
            }
            set
            {
                addRdvCommand = value;
                OnPropertyChanged("AddRdvCommand");
            }
        }
        /// <summary>
        /// Commande pour ouvrir la modal permettant de consulter les clients.
        /// </summary>
        public Command ClientsCommand
        {
            get
            {
                return clientsCommand;
            }
            set
            {
                clientsCommand = value;
                OnPropertyChanged("ClientsCommand");
            }
        }
        /// <summary>
        /// Commande pour réaliser une sauvegarde.
        /// </summary>
        public Command SaveCommand
        {
            get
            {
                return saveCommand;
            }
            set
            {
                saveCommand = value;
                OnPropertyChanged("SaveCommand");
            }
        }
        /// <summary>
        /// Commande pour consulter les travaux réalisés.
        /// </summary>
        public Command WorksCommand
        {
            get
            {
                return worksCommand;
            }
            set
            {
                worksCommand = value;
                OnPropertyChanged("WorksCommand");
            }
        }
        /// <summary>
        /// Commande pour consulter les travaux réalisés.
        /// </summary>
        public Command RefreshCommand
        {
            get
            {
                return refreshCommand;
            }
            set
            {
                refreshCommand = value;
                OnPropertyChanged("RefreshCommand");
            }
        }

        public bool IsRdvSelected
        {
            get
            {
                return SelectedRendezVous != null;
            }
        }


        /// <summary>
        /// Commande pour éditer la facture du RDV sélectionné
        /// </summary>
        public Command DetailsFactureCommand
        {
            get
            {
                return detailsFactureCommand;
            }
            set
            {
                detailsFactureCommand = value;
                OnPropertyChanged("DetailsFactureCommand");
            }
        }

        // <summary>
        /// Commande pour supprimer le rendez-vous sélectionné.
        /// </summary>
        public Command DetailsDeleteCommand
        {
            get
            {
                return detailsDeleteCommand;
            }
            set
            {
                detailsDeleteCommand = value;
                OnPropertyChanged("DetailsDeleteCommand");
            }
        }

        public RendezVous SelectedRendezVous
        {
            get
            {
                return selectedRendezVous;
            }
            set
            {
                selectedRendezVous = value;
                if (selectedRendezVous == null && UserControlRendezVousList != null)
                    UserControlRendezVousList.ToList().ForEach(
                                        u => ((UserControlRendezVousViewModel)u.DataContext).IsSelected = false);
                OnPropertyChanged("IsRdvSelected");
                OnPropertyChanged("SelectedRendezVous");
            }
        }

        public DateTime SelectedDate
        {
            get
            {
                return selectedDate;
            }
            set
            {
                var date = value.AddDays(Config.Configuration.GetIndexOfDaysOfWeeks(
                    Config.Configuration.Calendrier.GetDayOfWeek(value).ToString()) * -1);
                SelectedDateForConverter = Config.Configuration.Calendrier.ToDateTime(date.Year,
                                         date.Month,
                                         date.Day,
                                         0, 0, 0, 0);
                InitializeAgenda();
                selectedDate = value;
                SelectedRendezVous = null;
                OnPropertyChanged("SelectedDate");
            }
        }

        public ObservableCollection<RendezVous> RendezVousList
        {
            get
            {
                return rendezVousList;
            }
            set
            {
                rendezVousList = value;
                OnPropertyChanged("RendezVousList");
            }
        }

        public ObservableCollection<UserControlRDV> UserControlRendezVousList
        {
            get
            {
                return userControlRendezVousList;
            }
            set
            {
                userControlRendezVousList = value;
                OnPropertyChanged("UserControlRendezVousList");
            }
        }

        public Command DoubleClickUserControlCommand
        {
            get
            {
                return doubleClickUserControlCommand;
            }
            set
            {
                doubleClickUserControlCommand = value;
                OnPropertyChanged("DoubleClickUserControlCommand");
            }
        }

        public Command SingleClickAgendaCommand
        {
            get
            {
                return singleClickAgendaCommand;
            }
            set
            {
                singleClickAgendaCommand = value;
                OnPropertyChanged("SingleClickAgendaCommand");
            }
        }

        #endregion

        #region Constructors
        public AgendaViewModel(Window view, AbstractViewModel owner)
            : base(view, owner, title)
        {
            try
            {
                MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
                InitCommands();
                Config.Configuration.InitializeDays();
                AddRdvTextButton = "Ajouter RDV";
                ClientsTextButton = "Clients";
                WorksTextButton = "Travaux";
                SaveTextButton = "Sauvegarder";
                RefreshImgPath = "images/refresh.png";
                SelectedDate = DateTime.Now;
            }
            catch (Exception e)
            {
                MessageBox.Show(View, "Une erreur est survenue lors du démarrage.\n" + e.Message, "Fatal error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.None);
                Application.Current.Shutdown();
            }
        }
        #endregion

        #region Methods

        /// <summary>
        /// Permet d'initialiser l'agenda.
        /// </summary>
        private void InitializeAgenda()
        {
            RefreshRendezVousIntoAgenda();
            int nbRDV = RdvManager.CountRdvDay(DateTime.Now);
            WindowTitle = String.Format("Agenda - {0} - {1} Rendez-vous aujourd'hui", DateTime.Now.ToShortDateString(), nbRDV);
        }

        public void RefreshRendezVousIntoAgenda()
        {
            UserControlRendezVousList.Clear();
            RendezVousList.Clear();

            var from = SelectedDateForConverter;
            var to = from.AddDays(5);
            var rdvList = RdvManager.RDVS.Where(r => r.Date.CompareTo(from) >= 0 && r.Date.CompareTo(to) <= 0).ToList();
            rdvList.ForEach(x =>
            {
                RendezVousList.Add(x);
                UserControlRendezVousList.Add(new UserControlRDV(this, x));
            });
            OnPropertyChanged("UserControlRendezVousList");
            
        }

        private void InitCommands()
        {
            AddRdvCommand = new Command((x) =>
            {
                AddRdv(x);
            });
            ClientsCommand = new Command((x) =>
            {
                Clients(x);
            });
            WorksCommand = new Command((x) =>
            {
                Works(x);
            });
            RefreshCommand = new Command((x) =>
            {
                Refresh(x);
            });
            SaveCommand = new Command((x) =>
            {
                Save(x);
            });

            DetailsDeleteCommand = new Command((x) =>
            {
                DeleteSelectedRdv(x);
            });

            DetailsFactureCommand = new Command((x) =>
            {
                FactureSelectedRdv(x);
            });
            SingleClickAgendaCommand = new Command((x) =>
            {
                SelectedRendezVous = null;
            });
            DoubleClickUserControlCommand = new Command((x) =>
            {
                SelectedRendezVous = null;
                if (x != null)
                {
                    var strParameter = x.ToString();
                    var day = strParameter[0];
                    var hour = int.Parse(strParameter.Substring(1));
                    var dayToAdd = 0;
                    switch (day)
                    {
                        case 'M':
                            // Mardi = Lundi + 1
                            dayToAdd = 1;
                            break;
                        case 'm':
                            // Mardi = Lundi + 2
                            dayToAdd = 2;
                            break;
                        case 'J':
                            // Mardi = Lundi + 3
                            dayToAdd = 3;
                            break;
                        case 'V':
                            // Mardi = Lundi + 4
                            dayToAdd = 4;
                            break;
                        default:
                            // Lundi
                            dayToAdd = 0;
                            break;
                    }
                    var date = SelectedDateForConverter.AddDays(dayToAdd).AddHours(hour);
                    Child = new GestionRendezVous(this, new RendezVous(date, DureeType.UneHeure, null, null));
                }
                else
                {
                    var now = DateTime.Now;
                    Child = new GestionRendezVous(this, new RendezVous(now.AddHours(now.Hour * -1).AddHours(9), DureeType.UneHeure, null, null));
                }
            });
        }

        private void AddRdv(object input)
        {
            SelectedRendezVous = null;
            var now = DateTime.Now;
            Child = new GestionRendezVous(this, new RendezVous(now.AddHours(now.Hour * -1).AddHours(9), DureeType.UneHeure, null, null));
        }

        private void Clients(object input)
        {
            SelectedRendezVous = null;
            Child = new GestionClients(this);
        }

        private void Works(object input)
        {
            SelectedRendezVous = null;
            Child = new TravauxVehicule(this);
        }

        private void Refresh(object input)
        {
            RdvManager.Initialize();
            SelectedRendezVous = null;
            RefreshRendezVousIntoAgenda();
        }

        private void DeleteSelectedRdv(object input)
        {
            if (SelectedRendezVous != null)
            {
                MessageBoxResult result = MessageBox.Show("Êtes-vous sûr de supprimer ce rendez-vous ?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result.Equals(MessageBoxResult.Yes))
                {
                    FactureManager.DeleteFactureByRDV(SelectedRendezVous);
                    ReparationRDVManager.DeleteReparationRDVByRDV(SelectedRendezVous);
                    RdvManager.DeleteRdv(SelectedRendezVous.Id);
                    SelectedRendezVous = null;
                    RefreshRendezVousIntoAgenda();
                }
            }
        }

        private void FactureSelectedRdv(object input)
        {

        }

        /// <summary>
        /// Permet d'effectuer une sauvegarde de la BDD
        /// 
        /// </summary>
        /// <param name="input"></param>
        private void Save(object input)
        {
            System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string path = fbd.SelectedPath;
                Thread thSaver = new Thread(new ParameterizedThreadStart(Connexion.Sauvegarde));
                thSaver.Start(path);
            }
        }

        #endregion

    }
}
