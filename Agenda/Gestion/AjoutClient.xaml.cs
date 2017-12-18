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
using Agenda.ViewModels;

namespace Agenda.Gestion
{
    /// <summary>
    /// Logique d'interaction pour AjoutClient.xaml
    /// </summary>
    public partial class AjoutClient : Window
    {
        public AjoutClient(GestionClientsViewModel owner)
        {
            InitializeComponent();
            DataContext = new AddClientViewModel(this, owner);
        }

        #region WindowEvent
        void myWindowHeadBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        void Btn_MinimizePrincipale_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
        }

        void Btn_ClosePrincipale_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        #endregion
    }
}
