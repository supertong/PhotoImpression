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
        private SQLiteDatabase database;
        public static PhotoPresent me;

        public PhotoPresent()
        {
            InitializeComponent();
            me = this;
            database = new SQLiteDatabase();
            imageContainer.Source = database.getRandomImageFromDatabase();
        }

        public static PhotoPresent Singleton
        {
            get
            {
                return me;
            }
        }

        public static void autoRun() {
            //browser.autoRunImage();
        }

        public static void leftRotate() {
            //browser.LeftRotate();
        }

        public static void setWallpaper() {
            //browser.setBackGround();
        }

        private void imageContainer_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

    }
}
