using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PhotoImpression.ViewComponents
{
    /// <summary>
    /// Interaction logic for PhotoGallery.xaml
    /// </summary>
    public partial class PhotoGallery : UserControl
    {
        public static PhotoGallery me;

        public PhotoGallery()
        {
            InitializeComponent();
            me = this;
        }

        public static PhotoGallery Singleton
        {
            get
            {
                return me;
            }
        }

        public void swithPhoto(UserControl userControll)
        {
            TransitionBox.Content = userControll;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            swithPhoto(ViewHandler.PhotoPresent);
        }
    }
}
