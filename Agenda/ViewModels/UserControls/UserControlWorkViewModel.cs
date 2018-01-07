using Agenda.Utils;
using AgendaBDDManager;
using AgendaCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;

namespace Agenda.ViewModels
{
    public class UserControlWorkViewModel : AbstractViewModel
    {
        #region Attributes

        private ReparationRDV reparation;
        private ObservableCollection<Reparation> reparationsList;
        private Command deleteCommand;

        #endregion

        #region Properties

        public ReparationRDV Reparation
        {
            get
            {
                return reparation;
            }
            set
            {
                reparation = value;
                OnPropertyChanged("Reparation");
            }
        }
        public ObservableCollection<Reparation> ReparationsList
        {
            get
            {
                return reparationsList;
            }
            set
            {
                reparationsList = value;
                OnPropertyChanged("ReparationsList");
            }
        }

        public Command DeleteCommand
        {
            get
            {
                return deleteCommand;
            }
            set
            {
                deleteCommand = value;
                OnPropertyChanged("DeleteCommand");
            }
        }

        #endregion

        #region Constructors

        public UserControlWorkViewModel(Window view, AbstractViewModel owner, ReparationRDV reparation)
            : base(view, owner, string.Empty)
        {
            ReparationsList = new ObservableCollection<Reparation>(ReparationManager.REPARATIONS);
            Reparation = reparation != null 
                ? reparation 
                : new ReparationRDV(((GestionRendezVousViewModel)owner).RendezVous,
                ReparationsList.Count > 0 ? ReparationsList.First() : null, 
                string.Empty, 1, 0f, 0f, string.Empty);
            DeleteCommand = new Command((x) =>
            {
                Delete();
            });
        }

        #endregion

        #region Methods

        private void Delete()
        {
            Reparation.RendezVous.Travaux.Remove(Reparation);
            Reparation = null;
        }

        #endregion
    }
}
