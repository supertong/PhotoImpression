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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using FlickrNet;

namespace PhotoImpression
{
    /// <summary>
    /// Interaction logic for FlickrWindow.xaml
    /// </summary>
    public partial class FlickrWindow : Window
    {
        private string apiKey = "5ed32d353e466d9cf09131c46fd95eb5";

        public FlickrWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Flickr flickr = new Flickr(apiKey);
            PhotoSearchOptions searchOptions = new PhotoSearchOptions();
            searchOptions.Tags = "microsoft";
            PhotoCollection microsoftPhotos = flickr.PhotosSearch(searchOptions);


            ImageGallery.DataContext = microsoftPhotos;
        }
    }
}
