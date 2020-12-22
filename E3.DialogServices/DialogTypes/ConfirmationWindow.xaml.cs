using E3.DialogServices.DialogDataContexts;
using System.Windows;

namespace E3.DialogServices.DialogTypes
{
    /// <summary>
    /// Interaction logic for ConfirmationWindow.xaml
    /// </summary>
    public partial class ConfirmationWindow : Window
    {
        public ConfirmationWindow(Confirmation confirmationWindowDataContext)
        {
            InitializeComponent();
            DataContext = confirmationWindowDataContext;
        }
    }
}
