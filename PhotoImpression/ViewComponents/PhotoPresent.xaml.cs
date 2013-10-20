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

namespace PhotoImpression
{
    /// <summary>
    /// Interaction logic for PhotoPresent.xaml
    /// </summary>
    public partial class PhotoPresent : UserControl
    {
        private static PhotoBrowser browser;
        private static int index = 1;

        public PhotoPresent()
        {
            InitializeComponent();
        }

        public static int getIndex() {
            return index;
        }

        public PhotoPresent(int counter) {
            index = counter;
            InitializeComponent();
        }

        public static void autoRun() {
            browser.autoRunImage();
        }

        public static void rightRotate() {
            browser.RightRotate();
        }

        public static void leftRotate() {
            browser.LeftRotate();
        }

        public static void setWallpaper() {
            browser.setBackGround();
        }

        private void imageContainer_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
           
        }

        private void imageContainer_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void imageContainer_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void imageContainer_MouseWheel(object sender, MouseWheelEventArgs e)
        {

        }

        private void imageContainer_Loaded(object sender, RoutedEventArgs e)
        {
            browser = new PhotoBrowser(sender, e, imageContainer,ref index);
        }
    }
}
