using Agenda.Utils;
using Agenda.Utils;
using AgendaBDDManager;
using AgendaCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Agenda.ViewModels
{
    public class ParamsViewModel : AbstractViewModel
    {
        private ObservableCollection<JoursFeries> jours_feries = new ObservableCollection<JoursFeries>();

        private DateTime jourSelected = DateTime.Now;

        private JoursFeries jourListSelected;
        

        public ObservableCollection<JoursFeries> Jours_feries
        {
            get
            {
                return jours_feries;
            }
            set
            {
                jours_feries = value;
                OnPropertyChanged("Jours_Feries");
            }
        }


        public DateTime JourSelected
        {
            get
            {
                return jourSelected;
            }
            set
            {
                jourSelected = value;
                OnPropertyChanged("JourSelected");
            }
        }

        public JoursFeries JourListSelected
        {
            get
            {
                return jourListSelected;
            }
            set
            {
                jourListSelected = value;
                OnPropertyChanged("JourListSelected");
            }
        }

        private Command add_JoursFeries;
        public Command Add_JoursFeries
        {
            get
            {
                return add_JoursFeries;
            }
            set
            {
                add_JoursFeries = value;
                OnPropertyChanged("Add_JoursFeries");
            }
        }

        private Command del_JoursFeries;
        public Command Del_JoursFeries
        {
            get
            {
                return del_JoursFeries;
            }
            set
            {
                del_JoursFeries = value;
                OnPropertyChanged("Del_JoursFeries");
            }
        }


        public ParamsViewModel(): base(null,null)
        {
            Add_JoursFeries = new Command(AddJF);
            Del_JoursFeries = new Command(DelJF);
        }

        private void AddJF(object o)
        {
            if (JourSelected != null)
            {
                var jf = new JoursFeries(JourSelected);
                if (!Jours_feries.Contains(jf))
                {
                    JoursFeriesManager.Add(jf);
                    Jours_feries.Add(jf);
                }
            }
        }

        private void DelJF(object o)
        {
            if (JourListSelected != null)
            {
                if (Jours_feries.Contains(JourListSelected))
                {
                    JoursFeriesManager.Delete(JourListSelected);
                    Jours_feries.Remove(JourListSelected);
                }
            }
        }
    }
}
