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

namespace E3.FieldDevicesInfoPopulator.Views
{
    /// <summary>
    /// Interaction logic for FieldDeviceParameterInfoView.xaml
    /// </summary>
    public partial class FieldDeviceParameterInfoView : UserControl
    {
        public FieldDeviceParameterInfoView Instance { get; set; }

        public FieldDeviceParameterInfoView()
        {
            InitializeComponent();
            Instance = this;
        }
    }
}
