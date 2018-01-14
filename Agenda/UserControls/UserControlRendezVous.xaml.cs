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
using System.Windows.Navigation;
using System.Windows.Shapes;
using AgendaCore;
using Agenda.Gestion;
using AgendaBDDManager;
using Agenda.ViewModels;

namespace Agenda.UserControls
{
    /// <summary>
    /// Logique d'interaction pour UserControlRDV.xaml
    /// </summary>
    public partial class UserControlRendezVous : UserControl
    {
        public static bool IsMouseDownOnRdv;

        public UserControlRendezVous(AgendaViewModel owner, RendezVous rdv)
        {
            InitializeComponent();
            DataContext = new UserControlRendezVousViewModel(owner.View, owner, rdv); 
        }
    }
}
