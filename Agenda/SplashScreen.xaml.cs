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

namespace Agenda
{
    /// <summary>
    /// Logique d'interaction pour SplashScreen.xaml
    /// </summary>
    public partial class SplashScreen : Window
    {
        public SplashScreen()
        {
            InitializeComponent();
            MouseEnter += new MouseEventHandler(SplashScreen_MouseEnter);
        }

        void SplashScreen_MouseEnter(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Wait;
        }

        public void setProgressValue(int val)
        {
            myProgBar.Value = val;
        }
    }
}
