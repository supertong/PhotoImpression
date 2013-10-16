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

namespace PhotoImpression.ViewComponents
{
    /// <summary>
    /// Interaction logic for LeftMenuPanel.xaml
    /// </summary>
    public partial class LeftMenuPanel : UserControl
    {
        public LeftMenuPanel()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StartWindow.Singleton.swithRightPanel(ViewHandler.FlickrSearch);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            StartWindow.Singleton.swithRightPanel(ViewHandler.Gallery);
        }
    }
}
