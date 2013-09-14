using System;
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

namespace PhotoImpression
{
    class PhotoBrowser
    {
        private int counter;    //counter for record the index of image
        private String[] images; //array of string store image path
        private int degree;     //controll the image rotate degree,
        private Image imageContainer;   //container for the images

        //constructor
        public PhotoBrowser(object sender, RoutedEventArgs e, Image container) {
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

        /*
         * function to return image to image container by image path with name
         * **/
        private BitmapImage retriveImage(string imgName)
        {
            BitmapImage image = new BitmapImage();
            using (System.IO.FileStream stream = File.OpenRead(imgName))
            {
                image.BeginInit();
                image.StreamSource = stream;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.EndInit();
            }

            return image;
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
