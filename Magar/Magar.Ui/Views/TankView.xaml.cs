using Magar.Ui.Models;
using System.Windows.Controls;

namespace Magar.Ui.Views
{
    /// <summary>
    /// Interaction logic for TankView.xaml
    /// </summary>
    public partial class TankView : UserControl
    {
        public TankView()
        {
            InitializeComponent();
            if (DataContext == null)
            {
                DataContext = new Tank();
            }
        }
    }
}
