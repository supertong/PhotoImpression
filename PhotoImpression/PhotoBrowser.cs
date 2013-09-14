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

namespace PhotoImpression
{
    class PhotoBrowser
    {
        private int counter;    //counter for record the index of image
        private String[] images; //array of string store image path
        private int degree;     //controll the image rotate degree,
        private System.Windows.Controls.Image imageContainer;   //container for the images

        //constructor
        public PhotoBrowser(object sender, RoutedEventArgs e, System.Windows.Controls.Image container) {
            var path = folderBrowser(sender, e);
            
            while (path == null)
            {
                path = folderBrowser(sender, e);
            }

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

        /*
         * function to return image to image container by image path with name
         * **/
        private BitmapSource retriveImage(string imgName)
        {
            Image<Bgr, Byte> image = new Image<Bgr, byte>(imgName);
            
            return ToBitmapSource(image);
        }

        public void autoRunImage()
        {
            Timer timer = new Timer();
            timer.Tick += new EventHandler(time_tick);
            timer.Interval = 3000;
            timer.Start();
        }

        private void time_tick(object sender, EventArgs e)
        {
            this.NextPhoto();
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
            if (counter <= images.Length)
                counter = images.Length-1;
            imageContainer.Source =  this.retriveImage(images[counter]);
        }

    }
}