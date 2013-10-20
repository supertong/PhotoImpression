using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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

namespace PhotoImpression.ViewComponents
{
    /// <summary>
    /// Interaction logic for ToolBoxMenu.xaml
    /// </summary>
    public partial class ToolBoxMenu : UserControl
    {
        public ToolBoxMenu()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {


            System.Drawing.Color pixel;


            Bitmap oldbitmap = ImageCovert(PhotoPresent.Singleton.imageContainer);
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

            PhotoPresent.Singleton.imageContainer.Source = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(newbitmap.GetHbitmap(), IntPtr.Zero, System.Windows.Int32Rect.Empty, BitmapSizeOptions.FromWidthAndHeight(newbitmap.Width, newbitmap.Height));
        }


        public Bitmap ImageCovert(System.Windows.Controls.Image image)
        {
            System.Windows.Media.Imaging.BitmapSource transformedBitmapSource = image.Source as BitmapSource;

            int width = transformedBitmapSource.PixelWidth;
            int height = transformedBitmapSource.PixelHeight;
            int stride = width * ((transformedBitmapSource.Format.BitsPerPixel + 7) / 8);

            byte[] bits = new byte[height * stride];

            transformedBitmapSource.CopyPixels(bits, stride, 0);

            unsafe
            {
                fixed (byte* pBits = bits)
                {
                    IntPtr ptr = new IntPtr(pBits);

                    System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(
                        width,
                        height,
                        stride,
                        System.Drawing.Imaging.PixelFormat.Format32bppPArgb,
                        ptr);

                    return bitmap;
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Bitmap oldbitmap = ImageCovert(PhotoPresent.Singleton.imageContainer);
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

            PhotoPresent.Singleton.imageContainer.Source = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(newbitmap.GetHbitmap(), IntPtr.Zero, System.Windows.Int32Rect.Empty, BitmapSizeOptions.FromWidthAndHeight(newbitmap.Width, newbitmap.Height));
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Bitmap oldbitmap = ImageCovert(PhotoPresent.Singleton.imageContainer);
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
            PhotoPresent.Singleton.imageContainer.Source = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(newbitmap.GetHbitmap(), IntPtr.Zero, System.Windows.Int32Rect.Empty, BitmapSizeOptions.FromWidthAndHeight(newbitmap.Width, newbitmap.Height));
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Bitmap oldbitmap = ImageCovert(PhotoPresent.Singleton.imageContainer);
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

            PhotoPresent.Singleton.imageContainer.Source = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(newbitmap.GetHbitmap(), IntPtr.Zero, System.Windows.Int32Rect.Empty, BitmapSizeOptions.FromWidthAndHeight(newbitmap.Width, newbitmap.Height));

        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {

            Bitmap oldbitmap = ImageCovert(PhotoPresent.Singleton.imageContainer);
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

            PhotoPresent.Singleton.imageContainer.Source = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(img.GetHbitmap(), IntPtr.Zero, System.Windows.Int32Rect.Empty, BitmapSizeOptions.FromWidthAndHeight(newbitmap.Width, newbitmap.Height));

        }

   
    }
}
