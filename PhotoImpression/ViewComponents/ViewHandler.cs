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
        private static PhotoGallery _photoGallery;
        private static PhotoPresent _photoPresent;
        private static PhotoMenu _photoMenu;

        public static UserControl LeftMenuPanel
        {
            get
            {
                if (_leftMenuPanel == null)
                    _leftMenuPanel = new LeftMenuPanel();
                return _leftMenuPanel;
            }
        }

        public static UserControl PhotoPresent
        {
            get {
                if (_photoPresent == null)
                    _photoPresent = new PhotoPresent();
                return _photoPresent;
            }
        }

        public static UserControl PhotoMenu
        {
            get
            {
                if (_photoMenu == null)
                    _photoMenu = new PhotoMenu();
                return _photoMenu;
            }
        }

        public static UserControl PhotoGallery
        {
            get {
                if (_photoGallery == null)
                    _photoGallery = new PhotoGallery();
                return _photoGallery;
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
