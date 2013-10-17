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
using System.Windows.Navigation;
using System.Windows.Shapes;
using FlickrNet;

namespace PhotoImpression.ViewComponents
{
    /// <summary>
    /// Interaction logic for FlickrSearch.xaml
    /// </summary>
    public partial class FlickrSearch : UserControl
    {
        private string apiKey = "5ed32d353e466d9cf09131c46fd95eb5";

        public FlickrSearch()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Flickr flickr = new Flickr(apiKey);
            PhotoSearchOptions searchOptions = new PhotoSearchOptions();
            searchOptions.Tags = "microsoft";
            searchOptions.PerPage = 12;
            Console.WriteLine("Ready to search");
            flickr.PhotosSearchAsync(searchOptions, (callback) =>
                {
                    if (callback.HasError == true)
                    {
                        MessageBox.Show(callback.ErrorMessage);
                    }
                    else
                    {
                        ImageGallery.DataContext = callback.Result;
                    }
                });
            Console.WriteLine("Finished searching....");

           // ImageGallery.DataContext = microsoftPhotos;
        }
    }
}
