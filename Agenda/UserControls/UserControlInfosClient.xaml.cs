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
using System.ComponentModel;

namespace Agenda.UserControls
{
    /// <summary>
    /// Logique d'interaction pour UserControlInfosClient.xaml
    /// </summary>
    public partial class UserControlInfosClient : UserControl
    {
        public Vehicule mVehicule { get; set; }
        public Vehicule pVehicule
        {
            get
            {
                return mVehicule;
            }
            set
            {
                mVehicule = value;
                if (mVehicule != null)
                {
                    BindingOperations.GetBindingExpressionBase(NomClient, TextBox.TextProperty).UpdateTarget();
                    BindingOperations.GetBindingExpressionBase(PrenomClient, TextBox.TextProperty).UpdateTarget();
                    BindingOperations.GetBindingExpressionBase(Tel1Client, TextBox.TextProperty).UpdateTarget();
                    BindingOperations.GetBindingExpressionBase(Tel2Client, TextBox.TextProperty).UpdateTarget();
                    BindingOperations.GetBindingExpressionBase(EmailClient, TextBox.TextProperty).UpdateTarget();
                    BindingOperations.GetBindingExpressionBase(AdresseClient, TextBox.TextProperty).UpdateTarget();
                    BindingOperations.GetBindingExpressionBase(codePostalClient, TextBox.TextProperty).UpdateTarget();
                    BindingOperations.GetBindingExpressionBase(VilleClient, TextBox.TextProperty).UpdateTarget();
                    BindingOperations.GetBindingExpressionBase(MarqueVehicule, TextBox.TextProperty).UpdateTarget();
                    BindingOperations.GetBindingExpressionBase(ModeleVehicule, TextBox.TextProperty).UpdateTarget();
                    BindingOperations.GetBindingExpressionBase(ImmatVehicule, TextBox.TextProperty).UpdateTarget();
                    BindingOperations.GetBindingExpressionBase(Annee, TextBox.TextProperty).UpdateTarget();
                    BindingOperations.GetBindingExpressionBase(km, TextBox.TextProperty).UpdateTarget();
                }
            }
        }

        private Window mOwner = null;


        public UserControlInfosClient(Window owner, Vehicule vehicule)
        {
            
            mOwner = owner;
            InitializeComponent();
            DataContext = this;
            pVehicule = vehicule;
        }

        /// <summary>
        /// Met à jour le client et son véhicule
        /// </summary>
        /// <returns></returns>
        public Vehicule GetVehicule()
        {
            Vehicule v = mVehicule;
            if (v != null)
            {
                v.Client.Nom = NomClient.Text.Trim();
                v.Client.Prenom = PrenomClient.Text.Trim();
                v.Client.Telephone1 = Tel1Client.Text.Trim();
                v.Client.Telephone2 = Tel2Client.Text.Trim();
                v.Client.Email = EmailClient.Text.Trim();
                v.Client.Adresse = AdresseClient.Text.Trim();
                v.Client.CodePostal = codePostalClient.Text.Trim();
                v.Client.Ville = VilleClient.Text.Trim();

                v.Marque = MarqueVehicule.Text.Trim();
                v.Modele = ModeleVehicule.Text.Trim();
                v.Immatriculation = ImmatVehicule.Text.Trim();
                v.Annee = Annee.Text.Trim();
                v.Kilometrage = Int32.Parse(km.Text.Trim());
            }
            return v;
        }

        public void SetVehicule(Vehicule v)
        {
            pVehicule = v;
        }


        public void resetFields()
        {
            pVehicule = null;
        }
    }
}
