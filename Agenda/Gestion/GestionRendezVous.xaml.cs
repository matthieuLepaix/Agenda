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
    public partial class GestionRendezVous : Window
    {
        public GestionRendezVous(AgendaViewModel owner, RendezVous rendezVous)
        {
            InitializeComponent();
            DataContext = new GestionRendezVousViewModel(this, owner, rendezVous);
        }
    }
}
