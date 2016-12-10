using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Threading;
using System.Security.Permissions;
using System.Windows.Threading;
using System.Threading.Tasks;
using AgendaBDDManager;

namespace Agenda
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application
    {

        private MainWindow mainView;

        public App() { }

        protected override void  OnStartup(StartupEventArgs e)
        {
            Splasher.Splash = new SplashScreen();
            Splasher.ShowSplash();
            mainView = new Agenda.MainWindow();
            var startup = new Task(() =>
            {
                Connexion.init();
                // Simulate application loading
                for (int i = 5; i < 100; i += 5)
                {
                    if (i == 60)
                    {
                        Thread.Sleep(500);
                        Splasher.Splash.Dispatcher.BeginInvoke((Action)(() =>
                            (Splasher.Splash as SplashScreen).MyText.Text = "Chargement du contenu..."));
                    }
                    Splasher.Splash.Dispatcher.BeginInvoke((Action)(() =>
                    (Splasher.Splash as SplashScreen).setProgressValue(i)));
                    Thread.Sleep(500);
                }
            });


            startup.ContinueWith(t =>
            {
                mainView.Loaded += (sender, args) => Splasher.Splash.Close();
                mainView.Show();
            }, TaskScheduler.FromCurrentSynchronizationContext());

            startup.Start();

            

        }
    }

    /// <summary>
    /// Helper to show or close given splash window
    /// </summary>
    public static class Splasher
    {
        /// <summary>
        /// 
        /// </summary>
        private static Window mSplash;
        /// <summary>
        /// Get or set the splash screen window
        /// </summary>
        public static Window Splash
        {
            get
            {
                return mSplash;
            }
            set
            {
                mSplash = value;
            }
        }
        /// <summary>
        /// Show splash screen
        /// </summary>
        public static void ShowSplash()
        {
            if (mSplash != null)
            {
                mSplash.Show();
            }
        }
        /// <summary>
        /// Close splash screen
        /// </summary>
        public static void CloseSplash()
        {
            if (mSplash != null)
            {
                mSplash.Close();
                if (mSplash is IDisposable)
                    (mSplash as IDisposable).Dispose();
            }
        }
    } 
}
