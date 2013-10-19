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

        public void clearTransform() {
            imageContainer.LayoutTransform = new RotateTransform(0);
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

        /**
         *add function to get current displayed photo
         */
        public Image<Bgr, Byte> currentPhoto()
        {
            Image<Bgr, Byte> image = new Image<Bgr, Byte>(images[counter]);
            return image;
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
            if (counter < 0)
                counter = images.Length-1;
            imageContainer.Source =  this.retriveImage(images[counter]);
        }

        public void photographic_plate()
        {
            System.Drawing.Color  pixel;
            
         
            Bitmap oldbitmap = currentPhoto().ToBitmap();
            Bitmap newbitmap = new Bitmap(oldbitmap.Width, oldbitmap.Height);
            for (int x = 1; x < oldbitmap.Width; x++)
            {
                for (int y = 1; y < oldbitmap.Height; y++)
                {
                    int r, g, b;
                    pixel = oldbitmap.GetPixel(x, y);
                    r = 255 - pixel.R;
                    g = 255 - pixel.G;
                    b = 255 - pixel.B;
                    newbitmap.SetPixel(x, y, System.Drawing.Color.FromArgb(r, g, b));
                }
            }

            imageContainer.Source = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(newbitmap.GetHbitmap(), IntPtr.Zero, System.Windows.Int32Rect.Empty, BitmapSizeOptions.FromWidthAndHeight(newbitmap.Width, newbitmap.Height));

        }

        public void emboss()
        {
            Bitmap oldbitmap = currentPhoto().ToBitmap();
            Bitmap newbitmap = new Bitmap(oldbitmap.Width, oldbitmap.Height);
            System.Drawing.Color pixel1, pixel2;
            for (int x = 0; x < oldbitmap.Width - 1; x++)
            {
                for (int y = 0; y < oldbitmap.Height - 1; y++)
                {
                    int r = 0, g = 0, b = 0;
                    pixel1 = oldbitmap.GetPixel(x, y);
                    pixel2 = oldbitmap.GetPixel(x + 1, y + 1);
                    r = Math.Abs(pixel1.R - pixel2.R + 128);
                    g = Math.Abs(pixel1.G - pixel2.G + 128);
                    b = Math.Abs(pixel1.B - pixel2.B + 128);
                    if (r > 255)
                        r = 255;
                    if (r < 0)
                        r = 0;
                    if (g > 255)
                        g = 255;
                    if (g < 0)
                        g = 0;
                    if (b > 255)
                        b = 255;
                    if (b < 0)
                        b = 0;
                    newbitmap.SetPixel(x, y, System.Drawing.Color.FromArgb(r, g, b));
                }
            }

            imageContainer.Source = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(newbitmap.GetHbitmap(), IntPtr.Zero, System.Windows.Int32Rect.Empty, BitmapSizeOptions.FromWidthAndHeight(newbitmap.Width, newbitmap.Height));
                
        }

        public void blur()
        {
            Bitmap oldbitmap = currentPhoto().ToBitmap();
            Bitmap newbitmap = new Bitmap(oldbitmap.Width, oldbitmap.Height);
            System.Drawing.Color pixel;
          
            int[] Gauss = { 1, 2, 1, 2, 4, 2, 1, 2, 1 };
            for (int x = 1; x < oldbitmap.Width - 1; x++)
                for (int y = 1; y < oldbitmap.Height - 1; y++)
                {
                    int r = 0, g = 0, b = 0;
                    int Index = 0;
                    for (int col = -1; col <= 1; col++)
                        for (int row = -1; row <= 1; row++)
                        {
                            pixel = oldbitmap.GetPixel(x + row, y + col);
                            r += pixel.R * Gauss[Index];
                            g += pixel.G * Gauss[Index];
                            b += pixel.B * Gauss[Index];
                            Index++;
                        }
                    r /= 16;
                    g /= 16;
                    b /= 16;
                   
                    r = r > 255 ? 255 : r;
                    r = r < 0 ? 0 : r;
                    g = g > 255 ? 255 : g;
                    g = g < 0 ? 0 : g;
                    b = b > 255 ? 255 : b;
                    b = b < 0 ? 0 : b;
                    newbitmap.SetPixel(x - 1, y - 1, System.Drawing.Color.FromArgb(r, g, b));
                }
            imageContainer.Source = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(newbitmap.GetHbitmap(), IntPtr.Zero, System.Windows.Int32Rect.Empty, BitmapSizeOptions.FromWidthAndHeight(newbitmap.Width, newbitmap.Height));


        }

        public void sharpen()
        {
            Bitmap oldbitmap = currentPhoto().ToBitmap();
            Bitmap newbitmap = new Bitmap(oldbitmap.Width, oldbitmap.Height);
            System.Drawing.Color pixel;

            int[] Laplacian = { -1, -1, -1, -1, 9, -1, -1, -1, -1 };
            for (int x = 1; x < oldbitmap.Width - 1; x++)
                for (int y = 1; y < oldbitmap.Height - 1; y++)
                {
                    int r = 0, g = 0, b = 0;
                    int Index = 0;
                    for (int col = -1; col <= 1; col++)
                        for (int row = -1; row <= 1; row++)
                        {
                            pixel = oldbitmap.GetPixel(x + row, y + col); r += pixel.R * Laplacian[Index];
                            g += pixel.G * Laplacian[Index];
                            b += pixel.B * Laplacian[Index];
                            Index++;
                        }

                    r = r > 255 ? 255 : r;
                    r = r < 0 ? 0 : r;
                    g = g > 255 ? 255 : g;
                    g = g < 0 ? 0 : g;
                    b = b > 255 ? 255 : b;
                    b = b < 0 ? 0 : b;
                    newbitmap.SetPixel(x - 1, y - 1, System.Drawing.Color.FromArgb(r, g, b));
                }

            imageContainer.Source = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(newbitmap.GetHbitmap(), IntPtr.Zero, System.Windows.Int32Rect.Empty, BitmapSizeOptions.FromWidthAndHeight(newbitmap.Width, newbitmap.Height));


        }

        public void oil_painting()
        {
            Bitmap oldbitmap = currentPhoto().ToBitmap();
            Bitmap newbitmap = new Bitmap(oldbitmap.Width, oldbitmap.Height);

 
            RectangleF rect = new RectangleF(0, 0, oldbitmap.Width, oldbitmap.Height);
            Bitmap img = oldbitmap.Clone(rect, System.Drawing.Imaging.PixelFormat.DontCare);
          
            Random rnd = new Random();
           
            int iModel = 2;
            int i = oldbitmap.Width - iModel;
            while (i > 1)
            {
                int j = oldbitmap.Height - iModel;
                while (j > 1)
                {
                    int iPos = rnd.Next(100000) % iModel;
                   
                    System.Drawing.Color color = img.GetPixel(i + iPos, j + iPos);
                    img.SetPixel(i, j, color);
                    j = j - 1;
                }
                i = i - 1;
            }

            imageContainer.Source = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(img.GetHbitmap(), IntPtr.Zero, System.Windows.Int32Rect.Empty, BitmapSizeOptions.FromWidthAndHeight(newbitmap.Width, newbitmap.Height));

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
