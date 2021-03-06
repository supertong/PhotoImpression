﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using PhotoImpression.ViewComponents;
using MahApps.Metro.Controls;

namespace PhotoImpression
{
    /// <summary>
    /// Interaction logic for StartWindow.xaml
    /// </summary>
    public partial class StartWindow : MetroWindow
    {
        public static StartWindow me;

        public StartWindow()
        {
            InitializeComponent();
            me = this;

            SQLiteDatabase database = new SQLiteDatabase();
        }

        public static StartWindow Singleton
        {
            get
            {
                return me;
            }
        }

        public void swithRightPanel(UserControl userControll)
        {
            rightPanel.Content = userControll;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // set Gallery as the default user control for displaying
            swithRightPanel(ViewHandler.PhotoGallery);
        }
    }
}
