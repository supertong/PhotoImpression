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
        private SQLiteDatabase database;
        public static PhotoPresent me;

        
        public PhotoPresent()
        {
            InitializeComponent();
            me = this;
            database = new SQLiteDatabase();
        }

        public static PhotoPresent Singleton
        {
            get
            {
                return me;
            }
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

        private void imageContainer_Loaded(object sender, RoutedEventArgs e)
        {
            imageContainer.Source = database.getRandomImageFromDatabase();
        }

    }
}
