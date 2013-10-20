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
using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Media.Animation;

namespace PhotoImpression
{
    class PhotoBrowser
    {
        private string[] images;
        //constructor
        public PhotoBrowser(object sender, RoutedEventArgs e) {
           string path = folderBrowser(sender, e);

           if (path == null)
               images = null;
           else
           {
               //get all images from path
               images = GetImagesFrom(path, true);
           }
        }

        public string[] getImages() {
            return this.images;
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
        public byte[] retriveImage(string imgName)
        {
            MemoryStream ms = new MemoryStream();
            FileStream fs = new FileStream(imgName, FileMode.Open, FileAccess.Read);
            ms.SetLength(fs.Length);
            fs.Read(ms.GetBuffer(), 0, (int)fs.Length);

            ms.Flush();
            fs.Close();

            return ms.ToArray();
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

    }
}
