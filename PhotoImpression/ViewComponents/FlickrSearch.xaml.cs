using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
        private Flickr flickr;
        private string apiKey = "5ed32d353e466d9cf09131c46fd95eb5";

        public FlickrSearch()
        {
            InitializeComponent();
        }

        private void Flickr_Search(string keyword)
        {
            ImageGallery.DataContext = null;
            PhotoSearchOptions searchOptions = new PhotoSearchOptions();
            searchOptions.Tags = keyword;
            searchOptions.PerPage = 12;
            Console.WriteLine("Ready to search");
            this.flickr.PhotosSearchAsync(searchOptions, (callback) =>
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
        }

        private void UserControl_Initialized(object sender, EventArgs e)
        {
            this.flickr = new Flickr(apiKey);
            this.Flickr_Search("dog");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Searching for keyword: " + this.keyword.Text);
            this.Flickr_Search(this.keyword.Text);
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            ListBoxItem myListBoxItem = (ListBoxItem)(ImageGallery.ItemContainerGenerator.ContainerFromItem(ImageGallery.Items.CurrentItem));
            Photo photo = myListBoxItem.DataContext as Photo;

            var request = WebRequest.Create(photo.LargeUrl);
            Console.WriteLine("Saving image from uri: " + photo.LargeUrl);
            byte[] byteArray;
            using (var response = request.GetResponse())
            using (var stream = response.GetResponseStream())
            {
                var ms = new MemoryStream();
                stream.CopyTo(ms);
                byteArray = ms.ToArray();
                SQLiteDatabase database = new SQLiteDatabase();
                database.saveImageData(photo.Title, byteArray);
            }
        }
    }
}
