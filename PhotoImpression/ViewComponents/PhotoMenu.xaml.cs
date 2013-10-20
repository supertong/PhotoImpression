using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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
        private int degree = 0;

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
            PhotoGallery.Singleton.swithPhoto(new PhotoPresent());
        }

        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            PhotoGallery.Singleton.swithPhoto(new PhotoPresent());
        }

        private void backgroundButton_Click(object sender, RoutedEventArgs e)
        {
            this.setBackGround();
        }

        private void previousButton_Click(object sender, RoutedEventArgs e)
        {
            PhotoGallery.Singleton.swithPhoto(new PhotoPresent());
        }

        private void rightRotate_Click(object sender, RoutedEventArgs e)
        {
            degree += 90;
            PhotoPresent.Singleton.imageContainer.LayoutTransform = new RotateTransform(degree);
        }

        private void leftRotate_Click(object sender, RoutedEventArgs e)
        {
            degree -= 90;
            PhotoPresent.Singleton.imageContainer.LayoutTransform = new RotateTransform(degree);
        }

        [DllImport("user32.dll", EntryPoint = "SystemParametersInfo")]
        public static extern int SystemParametersInfo(
             int uAction,
             int uParam,
             string lpvParam,
             int fuWinIni
         );

        public void setBackGround()
        {
            BitmapImage bitmapImg = PhotoPresent.Singleton.imageContainer.Source as BitmapImage;
            BmpBitmapEncoder encoder = new BmpBitmapEncoder();
            
            encoder.Frames.Add(BitmapFrame.Create(bitmapImg));
            System.Drawing.Image img;
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                img = System.Drawing.Image.FromStream(ms);
                var currentPath = System.Environment.CurrentDirectory;

                //create directory for wallpaper
                System.IO.Directory.CreateDirectory(currentPath + "\\backgroundImage\\");

                img.Save(currentPath.ToString() + "\\backgroundImage\\background.bmp", System.Drawing.Imaging.ImageFormat.Bmp);
                SystemParametersInfo(20, 0, currentPath.ToString() + "\\backgroundImage\\background.bmp", 0x2);
            }
        }

    }
}
