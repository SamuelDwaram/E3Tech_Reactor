﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace E3Tech.RecipeBuilding.Helpers
{
    /// <summary>
    /// This is an Attached Behavior and is intended for use with
    /// XAML objects to enable binding a drag and drop event to
    /// an ICommand.
    /// </summary>
    public class DataGridCellDropBehavior
    {
        #region The dependecy Property
        /// <summary>
        /// The Dependency property. To allow for Binding, a dependency
        /// property must be used.
        /// </summary>
        public static readonly DependencyProperty DropCommandProperty =
                    DependencyProperty.RegisterAttached
                    (
                        "DropCommand",
                        typeof(ICommand),
                        typeof(DataGridCellDropBehavior),
                        new PropertyMetadata(DropCommandPropertyChangedCallBack)
                    );
        #endregion

        #region The getter and setter
        /// <summary>
        /// The setter. This sets the value of the PreviewDropCommandProperty
        /// Dependency Property. It is expected that you use this only in XAML
        ///
        /// This appears in XAML with the "Set" stripped off.
        /// XAML usage:
        ///
        /// <Grid helpers:DropBehavior.DropCommand="{Binding DropCommand}" />
        ///
        /// </summary>
        /// <param name="inUIElement">A UIElement object. In XAML this is automatically passed
        /// in, so you don't have to enter anything in XAML.</param>
        /// <param name="inCommand">An object that implements ICommand.</param>
        public static void SetDropCommand(UIElement inUIElement, ICommand inCommand)
        {
            inUIElement.SetValue(DropCommandProperty, inCommand);
        }

        /// <summary>
        /// Gets the PreviewDropCommand assigned to the PreviewDropCommandProperty
        /// DependencyProperty. As this is only needed by this class, it is private.
        /// </summary>
        /// <param name="inUIElement">A UIElement object.</param>
        /// <returns>An object that implements ICommand.</returns>
        public static ICommand GetDropCommand(UIElement inUIElement)
        {
            return (ICommand)inUIElement.GetValue(DropCommandProperty);
        }
        #endregion

        #region The PropertyChangedCallBack method
        /// <summary>
        /// The OnCommandChanged method. This event handles the initial binding and future
        /// binding changes to the bound ICommand
        /// </summary>
        /// <param name="inDependencyObject">A DependencyObject</param>
        /// <param name="inEventArgs">A DependencyPropertyChangedEventArgs object.</param>
        private static void DropCommandPropertyChangedCallBack(
            DependencyObject inDependencyObject, DependencyPropertyChangedEventArgs inEventArgs)
        {
            UIElement uiElement = inDependencyObject as UIElement;
            if (null == uiElement)
            {
                return;
            }

            uiElement.Drop += (sender, args) =>
            {
                var senderUIElement = sender as FrameworkElement;
                
                DataGridCell cell = senderUIElement.GetParentOfType<DataGridCell>() as DataGridCell;

                GetDropCommand(uiElement).Execute(new DataGridCellDropCommandParameters()
                {
                    DataContext = cell.DataContext,
                    DataObject = args.Data,
                    Block = cell.Column.DisplayIndex,
                });
                args.Handled = true;
            };
        }
        #endregion
    }

    public class DataGridCellDropCommandParameters
    {
        public object DataContext { get; set; }

        public IDataObject DataObject { get; set; }

        public int Block { get; set; }
    }
}
