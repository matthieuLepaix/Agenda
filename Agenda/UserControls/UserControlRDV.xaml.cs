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

namespace Agenda
{
    /// <summary>
    /// Logique d'interaction pour UserControlRDV.xaml
    /// </summary>
    public partial class UserControlRDV : UserControl
    {
        public static bool IsMouseDownOnRdv;

        public RendezVous Rdv;

        private MainWindow mOwner;

        public UserControlRDV(MainWindow owner, RendezVous rdv)
        {
            mOwner = owner;
            Rdv = rdv;
            initializeStyle();
            this.MouseDoubleClick += new MouseButtonEventHandler(UserControlRDV_DoubleClick);
            this.MouseDown += new MouseButtonEventHandler(UserControlRDV_MouseDown);
        }

        public void initializeStyle()
        {
            InitializeComponent();
            RdvBorder.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#0048ac")); 
            TextBlock t = new TextBlock();
            t.TextAlignment = TextAlignment.Center;
            t.Width = 132;
            t.Foreground = Brushes.WhiteSmoke;
            t.FontSize = 10;
            t.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            t.Text = Rdv.ToString();
            RDVText.Children.Clear();
            RDVText.Children.Add(t);
        }

        void UserControlRDV_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount != 2)
            {
                IsMouseDownOnRdv = true;
            }
        }

        void UserControlRDV_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            mOwner.Child = new GestionRDV(mOwner, Rdv);
            mOwner.Child.Show();
            IsMouseDownOnRdv = true;
        }
    }
}
