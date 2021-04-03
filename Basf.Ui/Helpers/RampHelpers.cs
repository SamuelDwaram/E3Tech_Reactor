using E3.RampManager.ViewModels;
using E3.RampManager.Views;
using System.Windows;
using System.Windows.Controls;

namespace Basf.Ui.Helpers
{
    public class RampHelpers
    {
        #region FieldPoint Id
        public static string GetFieldPointId(DependencyObject obj)
        {
            return (string)obj.GetValue(FieldPointIdProperty);
        }

        public static void SetFieldPointId(DependencyObject obj, string value)
        {
            obj.SetValue(FieldPointIdProperty, value);
        }

        // Using a DependencyProperty as the backing store for FieldPointId.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FieldPointIdProperty =
            DependencyProperty.RegisterAttached("FieldPointId", typeof(string), typeof(RampHelpers), new PropertyMetadata(FieldPointIdChanged));

        private static void FieldPointIdChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is ContentControl))
            {
                return;
            }

            (d as ContentControl).Loaded += (sender, args) => {
                ContentControl contentControl = sender as ContentControl;
                RampView rampView = (RampView)contentControl.Content;
                RampViewModel dataContext = (RampViewModel)rampView.DataContext;
                dataContext.SetFieldPointId(GetFieldPointId(contentControl));
                dataContext.LoadRampIfExists();
            };
        }
        #endregion
    }
}
