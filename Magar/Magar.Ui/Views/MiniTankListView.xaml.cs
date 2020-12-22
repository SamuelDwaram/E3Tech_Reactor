using Magar.Ui.Models;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Magar.Ui.Views
{
    /// <summary>
    /// Interaction logic for MiniTankList.xaml
    /// </summary>
    public partial class MiniTankListView : UserControl
    {
        public MiniTankListView()
        {
            InitializeComponent();
            if (DataContext == null)
            {
                DataContext = new List<Tank>();
            }
        }
    }
}
