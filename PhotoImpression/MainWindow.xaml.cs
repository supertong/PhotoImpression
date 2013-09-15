using System;
using System.Collections.Generic;
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


namespace PhotoImpression
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        private PhotoBrowser browser;
        private bool leftButtonDown;
        private Point MousePreLocation;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void onLoad(object sender, RoutedEventArgs e)
        {
            browser = new PhotoBrowser(sender, e, imageContainer);
            String IconPath = System.Environment.CurrentDirectory.ToString()+"\\image\\";

            //dynamic load the icon from current working directory
            try
            {
                nextButtonIcon.Source = new BitmapImage(new Uri(IconPath + "right.png"));
                wallPaperButtonIcon.Source = new BitmapImage(new Uri(IconPath + "wallpaper.png"));
                previousButtonIcon.Source = new BitmapImage(new Uri(IconPath + "left.png"));
                rightRotateIcon.Source = new BitmapImage(new Uri(IconPath + "rotate_right.gif"));
                leftRotateIcon.Source = new BitmapImage(new Uri(IconPath + "rotate_left.gif"));
                autoRunIcon.Source = new BitmapImage(new Uri(IconPath + "auto.png"));
            }
            catch (Exception)
            {
                System.Windows.MessageBox.Show("Fail loading button icons, please check path "+IconPath);
                System.Environment.Exit(1);
            }
            //browser.autoRunImage();
        }

        private void autoRun_Click(object sender, RoutedEventArgs e) 
        {
            browser.autoRunImage();
        }

        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            browser.NextPhoto();
        }

        private void previousButton_Click(object sender, RoutedEventArgs e)
        {
            browser.PreviousPhoto();
        }

        private void rightRotate_Click(object sender, RoutedEventArgs e)
        {
            browser.RightRotate();
        }

        private void leftRotate_Click(object sender, RoutedEventArgs e)
        {
            browser.LeftRotate();
        }


        private void backgroundButton_Click(object sender, RoutedEventArgs e) {
            browser.setBackGround();
        }

        private void imageContainer_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            //change cursor
            imageContainer.Cursor = Cursors.Hand;

            try
            {
                //get the transformer form the image transform group

                ScaleTransform transform = imageTransformGroup.Children[0] as ScaleTransform;
                if (e.Delta > 0)
                {
                    browser.ZoomIn(1.3, transform);
                }
                else
                {
                    browser.ZoomOut(1.3, transform);
                }
            }
            catch (Exception) {
                System.Windows.MessageBox.Show("Fail to get the scale transformer");
                System.Environment.Exit(1);
            }

        }

        private void imageContainer_MouseMove(object sender, MouseEventArgs e)
        {
            Image image = sender as Image;
            if (image == null)
                return;
            if (leftButtonDown)
                MoveImage(image, e);
        }

        private void MoveImage(Image image, MouseEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed)
                return;
            
            TranslateTransform transform = imageTransformGroup.Children[3] as TranslateTransform;
            Point position = e.GetPosition(image);
        
            transform.X += (position.X - MousePreLocation.X);
            transform.Y += (position.Y - MousePreLocation.Y);
        }

        private void imageContainer_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Image image = sender as Image;
            if (image == null)
                return;

            image.CaptureMouse();
            leftButtonDown = true;
            MousePreLocation = e.GetPosition(image);
        }

        private void imageContainer_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Image image = sender as Image;
            if (image == null)
                return;

            image.ReleaseMouseCapture();
            leftButtonDown = false;
        }

   }
}
