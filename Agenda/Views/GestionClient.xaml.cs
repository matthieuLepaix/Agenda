using System.Windows;
using Agenda.ViewModels;

namespace Agenda.Gestion
{
    /// <summary>
    /// Logique d'interaction pour GestionClient.xaml
    /// </summary>
    public partial class GestionClients : Window
    {
        public GestionClients(AgendaViewModel owner)
        {
            InitializeComponent();
            DataContext = new GestionClientsViewModel(this, owner);
        }
    }
}
