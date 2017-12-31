using Agenda.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;

namespace Agenda.ViewModels
{
    public abstract class AbstractViewModel : INotifyPropertyChanged
    {
        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
        #endregion

        #region Properties
        private Window view;

        public Window View
        {
            get
            {
                return view;
            }
            set
            {
                view = value;
            }
        }

        private AbstractViewModel owner;

        public AbstractViewModel Owner
        {
            get
            {
                return owner;
            }
            set
            {
                owner = value;
            }
        }
        private Window child;

        public Window Child
        {
            get
            {
                return child;
            }
            set
            {
                child = value;
            }
        }

        private string windowTitle;

        public string WindowTitle
        {
            get
            {
                return windowTitle;
            }
            set
            {
                windowTitle = value;
                OnPropertyChanged("WindowTitle");
            }
        }
        #endregion

        #region Window action Command

        private Command minimizeCommand;
        private Command closeCommand;
        private Command dragCommand;
        private Command activatedCommand;


        public Command ActivatedCommand
        {
            get
            {
                return activatedCommand;
            }
            set
            {
                activatedCommand = value;
                OnPropertyChanged("ActivatedCommand");
            }
        }
        public Command MinimizeCommand
        {
            get
            {
                return minimizeCommand;
            }
            set
            {
                minimizeCommand = value;
                OnPropertyChanged("MinimizeCommand");
            }
        }
        public Command CloseCommand
        {
            get
            {
                return closeCommand;
            }
            set
            {
                closeCommand = value;
                OnPropertyChanged("CloseCommand");
            }
        }
        public Command DragCommand
        {
            get
            {
                return dragCommand;
            }
            set
            {
                dragCommand = value;
                OnPropertyChanged("DragCommand");
            }
        }

        protected void Activated()
        {
            View.Activate();
            if(Child != null)
            {
                Child.Activate();
            }
        }
        protected void Minimize()
        {
            View.WindowState = System.Windows.WindowState.Minimized;
        }

        protected void Close(Window w)
        {
            Close();
        }

        protected void Drag()
        {
            View.DragMove();
        }

        #endregion

        #region Window actions Methods
        public void Close()
        {
            if (Owner != null)
            {
                Owner.View.Opacity = 1;
                Owner.View.IsEnabled = true;
                if (Owner.View is MainWindow)
                {
                    Owner.View.WindowState = System.Windows.WindowState.Maximized;
                }
                else
                {
                    Owner.View.WindowState = System.Windows.WindowState.Normal;
                }
                Owner.View.Activate();
                View.Close();
            }
            else
            {
                //Main window close = close the application
                Application.Current.Shutdown();
            }
        }

        public void Open()
        {
            if (Owner != null)
            {
                Owner.View.Opacity = 0.3;
                Owner.View.IsEnabled = false;
            }
            View.Show();
        }
        #endregion

        #region Constructors
        protected AbstractViewModel(Window view, AbstractViewModel owner)
        {
            View = view;
            Owner = owner;

            ActivatedCommand = new Command((x) =>
            {
                Activated();
            });
            MinimizeCommand = new Command((x) =>
            {
                Minimize();
            });
            CloseCommand = new Command((x) =>
            {
                Close();
            });
            DragCommand = new Command((x) =>
            {
                Drag();
            });
        }
        #endregion

    }
}
