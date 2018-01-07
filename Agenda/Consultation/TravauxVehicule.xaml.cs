using Agenda.ViewModels;
using System.Windows;

namespace Agenda.Consultation
{
    /// <summary>
    /// Logique d'interaction pour Travaux_Vehicule.xaml
    /// </summary>
    public partial class TravauxVehicule : Window
    {
        public TravauxVehicule(AbstractViewModel owner)
        {
            InitializeComponent();
            DataContext = new TravauxVehiculeViewModel(this, owner, "Consultation des travaux");
        }
    }
}
