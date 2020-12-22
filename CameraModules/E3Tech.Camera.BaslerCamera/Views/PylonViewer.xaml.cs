using E3Tech.Camera.BaslerCamera.ViewModels;
using System.Windows.Controls;

namespace E3Tech.Camera.BaslerCamera.Views
{
    /// <summary>
    /// Interaction logic for ViewA.xaml
    /// </summary>
    public partial class PylonViewer : UserControl
    {
        public PylonViewer(PylonViewerModel viewAModel)
        {
            InitializeComponent();
            viewAModel.Initialize(this.Dispatcher);
            this.DataContext = viewAModel;
        }
    }
}
