using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace PhotoImpression.ViewComponents
{
    public static class ViewHandler
    {
        private static LeftMenuPanel _leftMenuPanel;
        private static Gallery _gallery;
        private static FlickrSearch _flickrSearch;

        public static UserControl LeftMenuPanel
        {
            get
            {
                if (_leftMenuPanel == null)
                    _leftMenuPanel = new LeftMenuPanel();
                return _leftMenuPanel;
            }
        }

        public static UserControl Gallery
        {
            get
            {
                if (_gallery == null)
                    _gallery = new Gallery();
                return _gallery;
            }
        }

        public static UserControl FlickrSearch
        {
            get
            {
                if (_flickrSearch == null)
                    _flickrSearch = new FlickrSearch();
                return _flickrSearch;
            }
        }
    }
}
