using System;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Anathem.Ui.Views
{
    /// <summary>
    /// Interaction logic for ViewA.xaml
    /// </summary>
    public partial class ReactorControlView : UserControl
    {
        public ReactorControlView()
        {
            InitializeComponent();
            _25LMA.Source = new BitmapImage(new Uri(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Images/25LMa.png")));
            dcm.Source = new BitmapImage(new Uri(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Images/dcm.png")));
            rv.Source = new BitmapImage(new Uri(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Images/rv.png")));
        }
    }
}
