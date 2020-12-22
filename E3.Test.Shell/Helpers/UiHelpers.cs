using System.Windows;
using System.Windows.Input;

namespace E3.Test.Shell.Helpers
{
    public class UiHelpers
    {
        public static ICommand GetWindowLoaded(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(WindowLoadedProperty);
        }

        public static void SetWindowLoaded(DependencyObject obj, ICommand value)
        {
            obj.SetValue(WindowLoadedProperty, value);
        }

        // Using a DependencyProperty as the backing store for WindowLoaded.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WindowLoadedProperty =
            DependencyProperty.RegisterAttached("WindowLoaded", typeof(ICommand), typeof(UiHelpers), new PropertyMetadata(PropChanged));

        private static void PropChanged(DependencyObject uiElement, DependencyPropertyChangedEventArgs e)
        {
            if (!(uiElement is UIElement))
            {
                return;
            }

            (uiElement as Window).Loaded += (sender, args) =>
            {
                GetWindowLoaded(uiElement).Execute(args);
            };
        }
    }
}
