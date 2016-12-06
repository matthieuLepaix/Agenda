﻿using System;
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
using AgendaBDDManager;

namespace Agenda.UserControls
{
    /// <summary>
    /// Logique d'interaction pour UserControlWork.xaml
    /// </summary>
    public partial class UserControlWork : UserControl
    {
        private RendezVous mRdv;

        private ReparationRDV mRep;

        public List<Reparation> mReparations;

        public UserControlWork(int i)
        {
            InitializeComponent();
            mReparations = ReparationManager.getAll();
            Work.ItemsSource = mReparations;
            noWork.Text = "-  ";
            Btn_Delete.Visibility = System.Windows.Visibility.Collapsed;
        }

        public UserControlWork(int i, RendezVous rdv) 
            : this(i)
        {
            mRdv = rdv;
        }

        public UserControlWork(int i, ReparationRDV rep)
            : this(i, rep.pRdv)
        {
            mRep = rep;
            Work.SelectedItem = rep.pReparation;
            qte.Text = rep.pQuantite.ToString();
            prix.Text = rep.pPrixU.ToString();
            Comment.Text = rep.pComments;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Visibility = System.Windows.Visibility.Collapsed;
            Work.SelectedItem = null;
        }

        public void setDeleteBtn()
        {
            Btn_Delete.Visibility = System.Windows.Visibility.Visible;
        }

        public ReparationRDV getWork()
        {
            ReparationRDV rep = null;
            Reparation r = Work.SelectedItem == null ? null : (Work.SelectedItem as Reparation);
            if(r != null) 
            {
                rep = new ReparationRDV(mRdv, r, "",qte.Text.Trim().Length > 0 ? int.Parse(qte.Text.Trim()) : 0,
                    prix.Text.Trim().Length > 0 ? float.Parse(prix.Text.Trim()) : 0, 0, Comment.Text.Trim());
            }
            return rep;
        }
    }
}