using E3.FieldDevicesInfoPopulator.ViewModels;
using System.Windows.Controls;

namespace E3.FieldDevicesInfoPopulator.Views
{
    /// <summary>
    /// Interaction logic for ModifyMaintenanceDateView.xaml
    /// </summary>
    public partial class ModifyMaintenanceDateView : UserControl
    {
        public ModifyMaintenanceDateView(ModifyMaintenaceDateViewModel modifyMaintenaceDateViewModel)
        {
            InitializeComponent();
            DataContext = modifyMaintenaceDateViewModel;
        }
    }
}
