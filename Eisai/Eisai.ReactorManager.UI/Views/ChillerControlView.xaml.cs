using System;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Eisai.ReactorManager.UI.Views
{
    /// <summary>
    /// Interaction logic for ChillerControlView.xaml
    /// </summary>
    public partial class ChillerControlView : UserControl
    {
        public ChillerControlView()
        {
            InitializeComponent();

            _6trImage.Source = new BitmapImage(new Uri(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Images\6_TR_Image.jpg"), UriKind.Absolute));
            _10trImage.Source = new BitmapImage(new Uri(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Images\10_TR_Image.jpg"), UriKind.Absolute));
            huber_3Image_1.Source = new BitmapImage(new Uri(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Images\Huber_3.png"), UriKind.Absolute));
            huber_3Image_2.Source = new BitmapImage(new Uri(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Images\Huber_3.png"), UriKind.Absolute));
            huber_3Image_3.Source = new BitmapImage(new Uri(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Images\Huber_3.png"), UriKind.Absolute));
        }
    }
}
