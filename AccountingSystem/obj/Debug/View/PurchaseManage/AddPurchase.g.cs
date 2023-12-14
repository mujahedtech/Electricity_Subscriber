﻿#pragma checksum "..\..\..\..\View\PurchaseManage\AddPurchase.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "51C7E2ED2A363034901A2A92198105C65965E856DAA8970D8AA60603766B07E4"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using AccountingSystem.View.PurchaseManage;
using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Converters;
using MaterialDesignThemes.Wpf.Transitions;
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


namespace AccountingSystem.View.PurchaseManage {
    
    
    /// <summary>
    /// AddPurchase
    /// </summary>
    public partial class AddPurchase : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 67 "..\..\..\..\View\PurchaseManage\AddPurchase.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox CobCat;
        
        #line default
        #line hidden
        
        
        #line 82 "..\..\..\..\View\PurchaseManage\AddPurchase.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtQty;
        
        #line default
        #line hidden
        
        
        #line 93 "..\..\..\..\View\PurchaseManage\AddPurchase.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtPrice;
        
        #line default
        #line hidden
        
        
        #line 107 "..\..\..\..\View\PurchaseManage\AddPurchase.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnAddList;
        
        #line default
        #line hidden
        
        
        #line 157 "..\..\..\..\View\PurchaseManage\AddPurchase.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox CobVendor;
        
        #line default
        #line hidden
        
        
        #line 171 "..\..\..\..\View\PurchaseManage\AddPurchase.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox CobInv;
        
        #line default
        #line hidden
        
        
        #line 182 "..\..\..\..\View\PurchaseManage\AddPurchase.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtNote;
        
        #line default
        #line hidden
        
        
        #line 194 "..\..\..\..\View\PurchaseManage\AddPurchase.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button InsertPurchase;
        
        #line default
        #line hidden
        
        
        #line 215 "..\..\..\..\View\PurchaseManage\AddPurchase.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid ListPurchaseDataGrid;
        
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
            System.Uri resourceLocater = new System.Uri("/AccountingSystem;component/view/purchasemanage/addpurchase.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\View\PurchaseManage\AddPurchase.xaml"
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
            this.CobCat = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 2:
            this.txtQty = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.txtPrice = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.btnAddList = ((System.Windows.Controls.Button)(target));
            
            #line 109 "..\..\..\..\View\PurchaseManage\AddPurchase.xaml"
            this.btnAddList.Click += new System.Windows.RoutedEventHandler(this.btnAddList_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.CobVendor = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 6:
            this.CobInv = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 7:
            this.txtNote = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.InsertPurchase = ((System.Windows.Controls.Button)(target));
            
            #line 195 "..\..\..\..\View\PurchaseManage\AddPurchase.xaml"
            this.InsertPurchase.Click += new System.Windows.RoutedEventHandler(this.InsertPurchase_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.ListPurchaseDataGrid = ((System.Windows.Controls.DataGrid)(target));
            
            #line 218 "..\..\..\..\View\PurchaseManage\AddPurchase.xaml"
            this.ListPurchaseDataGrid.PreviewKeyUp += new System.Windows.Input.KeyEventHandler(this.ListPurchaseDataGrid_PreviewKeyUp);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
