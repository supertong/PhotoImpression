﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Media.Animation;

namespace PhotoImpression
{
    class PhotoBrowser
    {
        private int counter;    //counter for record the index of image
        private String[] images; //array of string store image path
        private int degree;     //controll the image rotate degree,
        private System.Windows.Controls.Image imageContainer;   //container for the images
        private Config config;

        //constructor
        public PhotoBrowser(object sender, RoutedEventArgs e, System.Windows.Controls.Image container) {

            //declare teh config
            config = new Config();

            //read the path from config file
            var path = config.ReadConfig("path");


            //if it is null, let user define
            if (path == null)
            {
                path = folderBrowser(sender, e);
                //when path not defined
                while (path == null)
                {
                    path = folderBrowser(sender, e);
                }
                //get all images from path
                images = GetImagesFrom(path, true);
                //if there is no image
                while (images.Length <= 0)
                {
                    System.Windows.MessageBox.Show("There is no photos in this directory!try again");
                    path = folderBrowser(sender, e);
                    images = GetImagesFrom(path, true);
                }
                //write the path to config file
                config.WriteConfig("path", path);
            }

            //get all images from path
            images = GetImagesFrom(path, true);

            imageContainer = container;

            
            //display the first image
            imageContainer.Source = this.retriveImage(images[0]);

            counter = 0;
            degree = 0;
        }

        /*
         * Function return the path of folder selected
         * return a folder path
         * **/
        private string folderBrowser(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();

            var result = folderDialog.ShowDialog();

            if (result.ToString().Equals("OK"))
            {
                return folderDialog.SelectedPath;
            }
            return null;
        }

        /*
         * function retrive all the Files from folder
         * return a array of image path
         * **/
        private String[] GetImagesFrom(String searchFolder, bool isRecursive)
        {
            List<String> files = new List<string>();

            //generate the array for picture type
            var filters = new String[] { "jpg", "png", "gif", "tiff", "bmp" };

            var searchOption = isRecursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            foreach (var filter in filters)
            {
                files.AddRange(Directory.GetFiles(searchFolder, String.Format("*.{0}", filter), searchOption));
            }

            return files.ToArray();
        }

        /*
         * function to return image to image container by image path with name
         * **/
        public BitmapSource retriveImage(string imgName)
        {
            Image<Bgr, Byte> image = new Image<Bgr, Byte>(imgName);
            
            return ToBitmapSource(image);
        }

        /*
        private void FadeIn() {

            this.NextPhoto();
            DoubleAnimation opacityAnimation = new DoubleAnimation(0,1, new Duration(TimeSpan.FromSeconds(3)));
            Storyboard.SetTarget(opacityAnimation, imageContainer);
            Storyboard.SetTargetProperty(opacityAnimation, new PropertyPath("(UIElement.Opacity)"));
            Storyboard board = new Storyboard();
            board.Children.Add(opacityAnimation);
            board.Completed += (obj, args) =>
            {
                flipYOut();
            };
            board.Begin();
            
        }

        private void FadeOut()
        {
            DoubleAnimation opacityAnimation = new DoubleAnimation(1,0, new Duration(TimeSpan.FromSeconds(3)));
            Storyboard.SetTarget(opacityAnimation, imageContainer);
            Storyboard.SetTargetProperty(opacityAnimation, new PropertyPath("(UIElement.Opacity)"));
            Storyboard board = new Storyboard();
            board.Children.Add(opacityAnimation);
            board.Completed += (obj, args) =>
            {
                FadeIn();
            };
            board.Begin();
        }

        private void flipXIn() {
            this.NextPhoto();
            DoubleAnimation flipfront = new DoubleAnimation(90, 0, new Duration(TimeSpan.FromSeconds(2)));

            DependencyProperty[] PropertyChain = new DependencyProperty[]
            {
                UIElement.RenderTransformProperty,//0
                TransformGroup.ChildrenProperty, //1
                ScaleTransform.ScaleXProperty, //2
                ScaleTransform.ScaleYProperty, //3
                RotateTransform.AngleProperty, //4
                SkewTransform.AngleXProperty, //5
                SkewTransform.AngleYProperty, //6
                TranslateTransform.XProperty, //7
                TranslateTransform.YProperty //8
            };


            Storyboard.SetTarget(flipfront, imageContainer);
            Storyboard.SetTargetProperty(flipfront, new PropertyPath("(0).(1)[1].(5)", PropertyChain));

            Storyboard board = new Storyboard();
            board.Children.Add(flipfront);

            board.Completed += (obj, args) =>
            {
                FadeOut();
            };

            board.Begin();
        }

        private void flipXOut() {
            DoubleAnimation flipfront = new DoubleAnimation(0, 90, new Duration(TimeSpan.FromSeconds(2)));

            DependencyProperty[] PropertyChain = new DependencyProperty[]
            {
                UIElement.RenderTransformProperty,//0
                TransformGroup.ChildrenProperty, //1
                ScaleTransform.ScaleXProperty, //2
                ScaleTransform.ScaleYProperty, //3
                RotateTransform.AngleProperty, //4
                SkewTransform.AngleXProperty, //5
                SkewTransform.AngleYProperty, //6
                TranslateTransform.XProperty, //7
                TranslateTransform.YProperty //8
            };


            Storyboard.SetTarget(flipfront, imageContainer);
            Storyboard.SetTargetProperty(flipfront, new PropertyPath("(0).(1)[1].(5)", PropertyChain));

            Storyboard board = new Storyboard();
            board.Children.Add(flipfront);
            
            board.Completed += (obj, args) =>
            {
                flipXIn();
            };

            board.Begin();
        }

        private void flipYIn()
        {
            this.NextPhoto();
            DoubleAnimation flipfront = new DoubleAnimation(90, 0, new Duration(TimeSpan.FromSeconds(2)));

            DependencyProperty[] PropertyChain = new DependencyProperty[]
            {
                UIElement.RenderTransformProperty,//0
                TransformGroup.ChildrenProperty, //1
                ScaleTransform.ScaleXProperty, //2
                ScaleTransform.ScaleYProperty, //3
                RotateTransform.AngleProperty, //4
                SkewTransform.AngleXProperty, //5
                SkewTransform.AngleYProperty, //6
                TranslateTransform.XProperty, //7
                TranslateTransform.YProperty //8
            };


            Storyboard.SetTarget(flipfront, imageContainer);
            Storyboard.SetTargetProperty(flipfront, new PropertyPath("(0).(1)[1].(6)", PropertyChain));

            Storyboard board = new Storyboard();
            board.Children.Add(flipfront);

            board.Completed += (obj, args) =>
            {
                flipXOut();
            };

            board.Begin();
        }

        private void flipYOut()
        {
            DoubleAnimation flipfront = new DoubleAnimation(0, 90, new Duration(TimeSpan.FromSeconds(2)));

            DependencyProperty[] PropertyChain = new DependencyProperty[]
            {
                UIElement.RenderTransformProperty,//0
                TransformGroup.ChildrenProperty, //1
                ScaleTransform.ScaleXProperty, //2
                ScaleTransform.ScaleYProperty, //3
                RotateTransform.AngleProperty, //4
                SkewTransform.AngleXProperty, //5
                SkewTransform.AngleYProperty, //6
                TranslateTransform.XProperty, //7
                TranslateTransform.YProperty //8
            };


            Storyboard.SetTarget(flipfront, imageContainer);
            Storyboard.SetTargetProperty(flipfront, new PropertyPath("(0).(1)[1].(6)", PropertyChain));

            Storyboard board = new Storyboard();
            board.Children.Add(flipfront);

            board.Completed += (obj, args) =>
            {
                flipYIn();
            };

            board.Begin();
        }
         * 
         * */

        public void autoRunImage()
        {
            //flipXOut();
        }

        //private void time_tick(object sender, EventArgs e)
        //{
        //    this.NextPhoto();
        //}

        [DllImport("user32.dll", EntryPoint = "SystemParametersInfo")]
        public static extern int SystemParametersInfo(
             int uAction,
             int uParam,
             string lpvParam,
             int fuWinIni
         ); 

        public void setBackGround() {
            System.Drawing.Image img = System.Drawing.Image.FromFile(images[counter]);
            var currentPath = System.Environment.CurrentDirectory;
            
            //create directory for wallpaper
            System.IO.Directory.CreateDirectory(currentPath + "\\backgroundImage\\");

            img.Save(currentPath.ToString()+"\\backgroundImage\\background.bmp", System.Drawing.Imaging.ImageFormat.Bmp);
            SystemParametersInfo(20, 0, currentPath.ToString() + "\\backgroundImage\\background.bmp", 0x2);
        }

        public void ZoomIn(double scale,ScaleTransform transform) {
            transform.ScaleX *= scale;
            transform.ScaleY *= scale;
        }

        public void ZoomOut(double scale, ScaleTransform transform) {
            transform.ScaleX /= scale;
            transform.ScaleY /= scale;
        }
        /**
         rotate image to right
         */
        public void RightRotate() {
            degree += 90;

            imageContainer.LayoutTransform = new RotateTransform(degree);
        }

        /*
         * rotate image to left
         * **/
        public void LeftRotate() {
            degree += -90;
            imageContainer.LayoutTransform = new RotateTransform(degree);
        }

     

        /*
         * Return the next photo
         * **/
        public void NextPhoto() {
            counter++;
            if (counter >= images.Length)
                counter = 0;
            imageContainer.Source =  this.retriveImage(images[counter]);
        }

        /*
         * return the previous photo
         * **/
        public void PreviousPhoto()
        {
            counter--;
            if (counter <= 0)
                counter = images.Length-1;
            imageContainer.Source =  this.retriveImage(images[counter]);
        }


        /*
         * Functions from the emgu.cv documents
         * **/

        //functions tobitmapsource from emug cv
        [DllImport("gdi32")]
        private static extern int DeleteObject(IntPtr o);

        /// <summary>
        /// Convert an IImage to a WPF BitmapSource. The result can be used in the Set Property of Image.Source
        /// </summary>
        /// <param name="image">The Emgu CV Image</param>
        /// <returns>The equivalent BitmapSource</returns>
        private static BitmapSource ToBitmapSource(IImage image)
        {
            using (System.Drawing.Bitmap source = image.Bitmap)
            {
                IntPtr ptr = source.GetHbitmap(); //obtain the Hbitmap

                BitmapSource bs = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                    ptr,
                    IntPtr.Zero,
                    Int32Rect.Empty,
                    System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());

                DeleteObject(ptr); //release the HBitmap
                return bs;
            }
        }

    }
}
