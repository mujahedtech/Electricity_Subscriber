﻿#pragma checksum "..\..\..\..\..\View\ManageAccounts\Definition\InventoryManage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "0ACAE9E10793880C303B64EE7C66FEACD2E1A79A501288F40D7091F8794EC4C4"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using AccountingSystem.View.ManageAccounts.Definition;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
using System.Windows.Ink;
using System.Windows.Input;
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


namespace AccountingSystem.View.ManageAccounts.Definition {
    
    
    /// <summary>
    /// InventoryManage
    /// </summary>
    public partial class InventoryManage : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 11 "..\..\..\..\..\View\ManageAccounts\Definition\InventoryManage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid PopAdd;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\..\..\View\ManageAccounts\Definition\InventoryManage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid GridAdd;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\..\..\..\View\ManageAccounts\Definition\InventoryManage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock lblHeader;
        
        #line default
        #line hidden
        
        
        #line 63 "..\..\..\..\..\View\ManageAccounts\Definition\InventoryManage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtInvName;
        
        #line default
        #line hidden
        
        
        #line 74 "..\..\..\..\..\View\ManageAccounts\Definition\InventoryManage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox ChkIsGasInv;
        
        #line default
        #line hidden
        
        
        #line 89 "..\..\..\..\..\View\ManageAccounts\Definition\InventoryManage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtMessage;
        
        #line default
        #line hidden
        
        
        #line 99 "..\..\..\..\..\View\ManageAccounts\Definition\InventoryManage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnSave;
        
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
            System.Uri resourceLocater = new System.Uri("/AccountingSystem;component/view/manageaccounts/definition/inventorymanage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\View\ManageAccounts\Definition\InventoryManage.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
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
            this.PopAdd = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            this.GridAdd = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            this.lblHeader = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.txtInvName = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.ChkIsGasInv = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 6:
            this.txtMessage = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 7:
            this.btnSave = ((System.Windows.Controls.Button)(target));
            
            #line 100 "..\..\..\..\..\View\ManageAccounts\Definition\InventoryManage.xaml"
            this.btnSave.Click += new System.Windows.RoutedEventHandler(this.btnSave_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
