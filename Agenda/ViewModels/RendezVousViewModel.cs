using Agenda.Gestion;
using Agenda.Utils;
using AgendaCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace Agenda.ViewModels
{
    public class RendezVousViewModel : AbstractViewModel
    {
        #region Attributes
        private AgendaViewModel agendaVM;
        private RendezVous rendezVous;
        private Command doubleClickCommand;
        private Command singleClickCommand;
        private bool isSelected;
        #endregion
        #region Properties

        public AgendaViewModel AgendaVM
        {
            get
            {
                return agendaVM;
            }
            set
            {
                agendaVM = value;
            }
        }
        public RendezVous RendezVous
        {
            get
            {
                return rendezVous;
            }
            set
            {
                rendezVous = value;
                OnPropertyChanged("RendezVous");
            }
        }

        public Command DoubleClickCommand
        {
            get
            {
                return doubleClickCommand;
            }
            set
            {
                doubleClickCommand = value;
                OnPropertyChanged("DoubleClickCommand");
            }
        }
        public Command SingleClickCommand
        {
            get
            {
                return singleClickCommand;
            }
            set
            {
                singleClickCommand = value;
                OnPropertyChanged("SingleClickCommand");
            }
        }

        public bool IsSelected
        {
            get
            {
                return isSelected;
            }
            set
            {
                isSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }

        #endregion

        #region Constructors
        public RendezVousViewModel(Window view, AgendaViewModel owner, RendezVous rdv)
            : base(view, owner)
        {
            AgendaVM = owner;
            RendezVous = rdv;
            IsSelected = false;
            DoubleClickCommand = new Command((x) =>
            {
                AgendaVM.UserControlRendezVousList.ToList().ForEach(
                    u => ((RendezVousViewModel)u.DataContext).IsSelected = false);
                //TODO : mange child
                //new GestionRDV(null, RendezVous);
            });
            SingleClickCommand = new Command((z) =>
            {
                AgendaVM.UserControlRendezVousList.ToList().ForEach(
                    u => ((RendezVousViewModel)u.DataContext).IsSelected = false);
                IsSelected = true;
                AgendaVM.IsRdvSelected = true;
                AgendaVM.SelectedRendezVous = RendezVous;

                
            });
        }
        #endregion

        #region Methods



        #endregion
    }
}
