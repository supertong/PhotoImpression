﻿using System;
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
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PhotoImpression.ViewComponents
{
    /// <summary>
    /// Interaction logic for Gallery.xaml
    /// </summary>
    public partial class Gallery : UserControl
    {
        private PhotoBrowser browser;
        private bool leftButtonDown;
        private Point MousePreLocation;

        public Gallery()
        {
            InitializeComponent();
        }

        private void onLoad(object sender, RoutedEventArgs e)
        {         
            //browser = new PhotoBrowser(sender, e, imageContainer);
            String IconPath = "pack://application:,,,/Icons/";

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
                System.Windows.MessageBox.Show("Fail loading button icons, please check path " + IconPath);
                System.Environment.Exit(1);
            }
            //browser.autoRunImage();
        }

        private void autoRun_Click(object sender, RoutedEventArgs e)
        {
            //set windows location to right corner
            double width = System.Windows.SystemParameters.WorkArea.Width;
            double height = System.Windows.SystemParameters.WorkArea.Height;

            Window main = Application.Current.MainWindow;

            //this.Width = main.Width;
           // this.Height = main.Height;

            browser.autoRunImage();
        }

        //function clean the transform effect 
        private void clearTransform() {
            browser.clearTransform();
            TranslateTransform transform = imageTransformGroup.Children[3] as TranslateTransform;
            transform.X = 0;
            transform.Y = 0;

            ScaleTransform transformScale = imageTransformGroup.Children[0] as ScaleTransform;

            transformScale.ScaleX = 1;
            transformScale.ScaleY = 1;

            browser.ZoomIn(1, transformScale);
        }

        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            this.clearTransform();
            browser.NextPhoto();
        }

        private void previousButton_Click(object sender, RoutedEventArgs e)
        {
            this.clearTransform();
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


        private void backgroundButton_Click(object sender, RoutedEventArgs e)
        {
            browser.setBackGround();
        }

  
        private void imageContainer_MouseWheel(object sender, MouseWheelEventArgs e)
        {
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
                //change the cursor according zoom in and out
                if (transform.ScaleX >= 1.3)
                    imageContainer.Cursor = Cursors.Hand;
                else
                    imageContainer.Cursor = Cursors.Arrow;

            }
            catch (Exception)
            {
                System.Windows.MessageBox.Show("Fail to get the scale transformer");
                System.Environment.Exit(1);
            }

        }

        private void imageContainer_MouseMove(object sender, MouseEventArgs e)
        {
            Image image = sender as Image;
            if (image == null)
                return;

            //get the transformer form the image transform group
            ScaleTransform transform = imageTransformGroup.Children[0] as ScaleTransform;

            //only leftbutton is down and zoom in can move picture
            if (leftButtonDown && transform.ScaleX >= 1.3)
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

 

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            browser.photographic_plate();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            browser.emboss();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            browser.blur();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            browser.sharpen();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            browser.oil_painting();
        }
    
    }


}
