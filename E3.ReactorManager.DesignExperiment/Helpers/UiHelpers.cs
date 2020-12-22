using E3.ReactorManager.DesignExperiment.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace E3.ReactorManager.DesignExperiment.Helpers
{
    public class UiHelpers
    {
        #region Password
        public static object GetPassword(DependencyObject obj)
        {
            return obj.GetValue(PasswordProperty);
        }

        public static void SetPassword(DependencyObject obj, object value)
        {
            obj.SetValue(PasswordProperty, value);
        }

        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.RegisterAttached("Password", typeof(object), typeof(UiHelpers), new PropertyMetadata(PasswordChangedCallBack));

        private static void PasswordChangedCallBack(DependencyObject uiElement, DependencyPropertyChangedEventArgs e)
        {
            PasswordBox passwordBox = uiElement as PasswordBox;
            passwordBox.PasswordChanged += (sender, args) => {
                RunningExperimentTabViewModel dataContext = (RunningExperimentTabViewModel)GetPassword(uiElement);
                dataContext.AdminCredential.PasswordHash = (sender as PasswordBox).Password;
            };
        }
        #endregion
    }
}
