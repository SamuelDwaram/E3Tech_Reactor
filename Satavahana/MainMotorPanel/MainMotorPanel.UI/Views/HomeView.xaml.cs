using System;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace MainMotorPanel.UI.Views
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {
        public HomeView()
        {
            InitializeComponent();
            imgSource.Source = new BitmapImage(new Uri(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Images\Sindhugosh.jpg"), UriKind.Absolute));
        }
    }
}
