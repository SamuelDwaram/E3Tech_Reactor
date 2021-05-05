using E3.ReactorManager.Interfaces.UI.UserControls;
using E3.UserManager.Model.Data;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Anathem.Ui.Helpers
{
    public class UiHelpers
    {
        #region AllowedForUser
        public static object GetAllowedForUser(DependencyObject obj)
        {
            return obj.GetValue(AllowedForUserProperty);
        }

        public static void SetAllowedForUser(DependencyObject obj, object value)
        {
            obj.SetValue(AllowedForUserProperty, value);
        }

        public static readonly DependencyProperty AllowedForUserProperty =
            DependencyProperty.RegisterAttached("AllowedForUser", typeof(object), typeof(UiHelpers), new PropertyMetadata(AllowedForUserChangedCallBack));

        private static void AllowedForUserChangedCallBack(DependencyObject uiElement, DependencyPropertyChangedEventArgs e)
        {
            User loggedInUser = (User)e.NewValue;
            Type type = uiElement.GetType();
            string moduleToBeValidated = type.GetProperty("Tag").GetValue(uiElement).ToString();
            bool validationResult = loggedInUser.Roles.Any(role => role.ModulesAccessable.Contains(moduleToBeValidated));
            if (type == typeof(Button))
            {
                type.GetProperty("Visibility").SetValue(uiElement, validationResult ? Visibility.Visible : Visibility.Collapsed);
            }
            else if (type == typeof(TextBox))
            {
                type.GetProperty("IsReadOnly").SetValue(uiElement, !validationResult);
            }
        }
        #endregion

        #region Send Command To Device
        public static ICommand GetCommandToDevice(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(CommandToDeviceProperty);
        }

        public static void SetCommandToDevice(DependencyObject obj, ICommand value)
        {
            obj.SetValue(CommandToDeviceProperty, value);
        }

        public static readonly DependencyProperty CommandToDeviceProperty =
            DependencyProperty.RegisterAttached("CommandToDevice", typeof(ICommand), typeof(UiHelpers), new PropertyMetadata(SendCommandToDevice));

        private static void SendCommandToDevice(DependencyObject uiElement, DependencyPropertyChangedEventArgs e)
        {
            if (!(uiElement is UIElement))
            {
                return;
            }

            if (uiElement.GetType() == typeof(ButtonOnOffAnimation))
            {
                (uiElement as ButtonOnOffAnimation).MouseLeftButtonDown += (sender, args) => {
                    ButtonOnOffAnimation button = sender as ButtonOnOffAnimation;
                    string parameterInfo = GetParameterInfo(button);
                    GetCommandToDevice(button).Execute(parameterInfo + '|' + !Convert.ToBoolean(button.CurrentStatus));
                };
            }
        }
        #endregion

        #region Parameter Info
        public static string GetParameterInfo(DependencyObject obj)
        {
            return (string)obj.GetValue(ParameterInfoProperty);
        }

        public static void SetParameterInfo(DependencyObject obj, string value)
        {
            obj.SetValue(ParameterInfoProperty, value);
        }

        public static readonly DependencyProperty ParameterInfoProperty =
            DependencyProperty.RegisterAttached("ParameterInfo", typeof(string), typeof(UiHelpers), new PropertyMetadata(string.Empty));
        #endregion
    }
}
