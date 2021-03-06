#pragma checksum "..\..\..\Views\RecipeBuilderView.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "2C47CC951281D56FC7C9FCD04E0718AADDBF4B73E57BB8F6E8AECB71036DA24A"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using E3.ReactorManager.Interfaces.UI.UserControls;
using E3Tech.RecipeBuilding.Converters;
using E3Tech.RecipeBuilding.Helpers;
using E3Tech.RecipeBuilding.UserControls;
using E3Tech.RecipeBuilding.ViewModels;
using E3Tech.RecipeBuilding.Views;
using Prism.Interactivity;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Regions.Behaviors;
using Prism.Unity;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace E3Tech.RecipeBuilding.Views {
    
    
    /// <summary>
    /// RecipeBuilderView
    /// </summary>
    public partial class RecipeBuilderView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 14 "..\..\..\Views\RecipeBuilderView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal E3Tech.RecipeBuilding.Views.RecipeBuilderView SBV;
        
        #line default
        #line hidden
        
        
        #line 160 "..\..\..\Views\RecipeBuilderView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView lbOne;
        
        #line default
        #line hidden
        
        
        #line 202 "..\..\..\Views\RecipeBuilderView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.Popup BatchNameRequestPopup;
        
        #line default
        #line hidden
        
        
        #line 308 "..\..\..\Views\RecipeBuilderView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.Popup EndBatchPopup;
        
        #line default
        #line hidden
        
        
        #line 364 "..\..\..\Views\RecipeBuilderView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox AdminPassword;
        
        #line default
        #line hidden
        
        
        #line 403 "..\..\..\Views\RecipeBuilderView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dataGrid;
        
        #line default
        #line hidden
        
        
        #line 696 "..\..\..\Views\RecipeBuilderView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView ListView;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/E3Tech.RecipeBuilding;component/views/recipebuilderview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\RecipeBuilderView.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.SBV = ((E3Tech.RecipeBuilding.Views.RecipeBuilderView)(target));
            return;
            case 2:
            this.lbOne = ((System.Windows.Controls.ListView)(target));
            return;
            case 3:
            this.BatchNameRequestPopup = ((System.Windows.Controls.Primitives.Popup)(target));
            return;
            case 4:
            this.EndBatchPopup = ((System.Windows.Controls.Primitives.Popup)(target));
            return;
            case 5:
            this.AdminPassword = ((System.Windows.Controls.PasswordBox)(target));
            return;
            case 6:
            this.dataGrid = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 7:
            this.ListView = ((System.Windows.Controls.ListView)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

