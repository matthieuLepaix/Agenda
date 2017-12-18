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

namespace Agenda.UserControls
{
    /// <summary>
    /// Logique d'interaction pour UserControlNewClient.xaml
    /// </summary>
    public partial class UserControlNewClient : UserControl
    {
        public UserControlNewClient()
        {
            InitializeComponent();
        }

        public Client GetClient()
        {
            Client c = new Client(NomClient.Text.Trim(), PrenomClient.Text.Trim(), Tel1Client.Text.Trim(), Tel2Client.Text.Trim(),
                                  EmailClient.Text.Trim(), string.Format("{0} {1}",AdresseClient.Text.Trim(), AdresseClient.Text.Trim()),
                                  codePostalClient.Text.Trim(), VilleClient.Text.Trim());
            if (c.Nom.Length == 0 || c.Prenom.Length == 0 || c.Telephone1.Length == 0)
            {
                c = null;
            }
            return c;
        }

        public Vehicule GetVehicule()
        {
            Vehicule v = new Vehicule(MarqueVehicule.Text.Trim(), ModeleVehicule.Text.Trim(), ImmatVehicule.Text.Trim(), Annee.Text.Trim(), km.Text.Trim().Length > 0 ?int.Parse(km.Text.Trim()) : 0,
                        GetClient());
            if (v.Client != null)
            {
                if (v.Client.Nom.Length == 0 || v.Client.Prenom.Length == 0 || v.Client.Telephone1.Length == 0 ||
                    v.Client.Telephone1.Length == 0)
                {
                    v = null;
                }
                else if (v.Immatriculation.Length == 0 || v.Marque.Length == 0 || v.Modele.Length == 0)
                {
                    v = null;
                }
            }
            else
            {
                v = null;
            }
            return v;
        }

        public Client GetClientBrut()
        {
            Client c = new Client(NomClient.Text.Trim(), PrenomClient.Text.Trim(), Tel1Client.Text.Trim(), Tel2Client.Text.Trim(),
                                    EmailClient.Text.Trim(), string.Format("{0} {1}", AdresseClient.Text.Trim(), AdresseClient.Text.Trim()),
                                  codePostalClient.Text.Trim(), VilleClient.Text.Trim());
            return c;
        }

        public Vehicule GetVehiculeBrut()
        {
            Vehicule v = new Vehicule(MarqueVehicule.Text.Trim(), ModeleVehicule.Text.Trim(), ImmatVehicule.Text.Trim(), Annee.Text.Trim(), 
                km.Text.Trim().Length > 0 ? int.Parse(km.Text.Trim()) : 0, GetClient());
            return v;
        }
    }
}
