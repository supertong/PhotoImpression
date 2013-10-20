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
using System.Windows.Threading;

namespace PhotoImpression.ViewComponents
{
    /// <summary>
    /// Interaction logic for PhotoMenu.xaml
    /// </summary>
    public partial class PhotoMenu : UserControl
    {

        public PhotoMenu()
        {
            InitializeComponent();
        }

        private void autoRun_Click(object sender, RoutedEventArgs e)
        {
            DispatcherTimer timer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 0, 2) };
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();

        }

        void timer_Tick(object sender, EventArgs e)
        {
            int counter = PhotoPresent.getIndex();
            PhotoGallery.Singleton.swithPhoto(new PhotoPresent(++counter));
        }

        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            int counter = PhotoPresent.getIndex();
            PhotoGallery.Singleton.swithPhoto(new PhotoPresent(++counter));
        }

        private void backgroundButton_Click(object sender, RoutedEventArgs e)
        {
            PhotoPresent.setWallpaper();
        }

        private void previousButton_Click(object sender, RoutedEventArgs e)
        {
            int counter = PhotoPresent.getIndex();
            PhotoGallery.Singleton.swithPhoto(new PhotoPresent(--counter));
        }

        private void rightRotate_Click(object sender, RoutedEventArgs e)
        {
            PhotoPresent.rightRotate();
        }

        private void leftRotate_Click(object sender, RoutedEventArgs e)
        {
            PhotoPresent.leftRotate();
        }
    }
}
