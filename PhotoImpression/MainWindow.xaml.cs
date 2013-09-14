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
using System.Windows.Navigation;


namespace PhotoImpression
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        private PhotoBrowser browser;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void onLoad(object sender, RoutedEventArgs e)
        {
            browser = new PhotoBrowser(sender, e, imageContainer);
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
   }
}
