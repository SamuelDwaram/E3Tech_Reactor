using E3.UserManager.Model.Data;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Eisai.ReactorManager.UI.Helpers
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
    }
}
