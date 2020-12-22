using E3.Bpu.Models;
using E3.UserManager.Model.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace E3.RemoteClient.Ui.Helpers
{
    public class UiHelpers
    {
        #region CommandParameter for TextBox
        public static object GetCommandParameter(DependencyObject obj)
        {
            return obj.GetValue(CommandParameterProperty);
        }

        public static void SetCommandParameter(DependencyObject obj, object value)
        {
            obj.SetValue(CommandParameterProperty, value);
        }

        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.RegisterAttached("CommandParameter", typeof(object), typeof(UiHelpers));
        #endregion

        #region TextChanged
        public static ICommand GetTextChanged(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(TextChangedProperty);
        }

        public static void SetTextChanged(DependencyObject obj, ICommand value)
        {
            obj.SetValue(TextChangedProperty, value);
        }

        public static readonly DependencyProperty TextChangedProperty =
            DependencyProperty.RegisterAttached("TextChanged", typeof(ICommand), typeof(UiHelpers), new PropertyMetadata(TextChanged));

        private static void TextChanged(DependencyObject uiElement, DependencyPropertyChangedEventArgs e)
        {
            (uiElement as TextBox).KeyDown += (sender, args) => {
                if (args.Key == Key.Enter)
                {
                    TextBox textBox = sender as TextBox;
                    GetTextChanged(textBox).Execute(GetCommandParameter(textBox));
                    Keyboard.ClearFocus();
                }
            };
        }
        #endregion

        #region ReadOnlyValidation
        public static object GetReadOnlyValidation(DependencyObject obj)
        {
            return obj.GetValue(ReadOnlyValidationProperty);
        }

        public static void SetReadOnlyValidation(DependencyObject obj, object value)
        {
            obj.SetValue(ReadOnlyValidationProperty, value);
        }

        public static readonly DependencyProperty ReadOnlyValidationProperty =
            DependencyProperty.RegisterAttached("ReadOnlyValidation", typeof(object), typeof(UiHelpers), new PropertyMetadata(ReadOnlyValidationChanged));

        private static void ReadOnlyValidationChanged(DependencyObject uiElement, DependencyPropertyChangedEventArgs e)
        {
            object[] validationObjects = GetReadOnlyValidation(uiElement) as object[];
            RegisteredDevice registeredDevice = validationObjects[0] as RegisteredDevice;
            User user = validationObjects[1] as User;
            string accessibleModule = validationObjects[2] as string;
            (uiElement as TextBox).IsReadOnly = !(registeredDevice.ModulesAccessible.Contains(accessibleModule) && user.Roles.Any(role => role.ModulesAccessable.Contains(accessibleModule)));
        }
        #endregion

        #region Loaded
        public static ICommand GetLoaded(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(LoadedProperty);
        }

        public static void SetLoaded(DependencyObject obj, ICommand value)
        {
            obj.SetValue(LoadedProperty, value);
        }

        // Using a DependencyProperty as the backing store for Loaded.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LoadedProperty =
            DependencyProperty.RegisterAttached("Loaded", typeof(ICommand), typeof(UiHelpers), new PropertyMetadata(LoadedChanged));

        private static void LoadedChanged(DependencyObject uiElement, DependencyPropertyChangedEventArgs e)
        {
            if (!(uiElement is Window))
            {
                return;
            }

            (uiElement as Window).Loaded += (sender, args) => {
                GetLoaded(uiElement).Execute(args);
            };
        }
        #endregion
    }
}
