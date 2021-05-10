﻿#pragma checksum "..\..\..\..\UserInterface\Industry\BlueprintsControl.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9C8CA8BBBF615AC4B46E962A77DBB7BAD72D2CD1"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using EveOnlineTool.UserInterface.Industry;
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


namespace EveOnlineTool.UserInterface.Industry {
    
    
    /// <summary>
    /// BlueprintsControl
    /// </summary>
    public partial class BlueprintsControl : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 33 "..\..\..\..\UserInterface\Industry\BlueprintsControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox BuyableBlueprintsCheckBox;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\..\UserInterface\Industry\BlueprintsControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox OwnedBlueprintsCheckBox;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\..\UserInterface\Industry\BlueprintsControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox CorporationOwnedBlueprintsCheckBox;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\..\UserInterface\Industry\BlueprintsControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox InventableBlueprintsCheckBox;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\..\UserInterface\Industry\BlueprintsControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox SearchTextBox;
        
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
            System.Uri resourceLocater = new System.Uri("/EveOnlineTool;component/userinterface/industry/blueprintscontrol.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\UserInterface\Industry\BlueprintsControl.xaml"
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
            this.BuyableBlueprintsCheckBox = ((System.Windows.Controls.CheckBox)(target));
            
            #line 33 "..\..\..\..\UserInterface\Industry\BlueprintsControl.xaml"
            this.BuyableBlueprintsCheckBox.Checked += new System.Windows.RoutedEventHandler(this.BuyableChanged);
            
            #line default
            #line hidden
            
            #line 33 "..\..\..\..\UserInterface\Industry\BlueprintsControl.xaml"
            this.BuyableBlueprintsCheckBox.Unchecked += new System.Windows.RoutedEventHandler(this.BuyableChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.OwnedBlueprintsCheckBox = ((System.Windows.Controls.CheckBox)(target));
            
            #line 35 "..\..\..\..\UserInterface\Industry\BlueprintsControl.xaml"
            this.OwnedBlueprintsCheckBox.Checked += new System.Windows.RoutedEventHandler(this.OwnedChanged);
            
            #line default
            #line hidden
            
            #line 35 "..\..\..\..\UserInterface\Industry\BlueprintsControl.xaml"
            this.OwnedBlueprintsCheckBox.Unchecked += new System.Windows.RoutedEventHandler(this.OwnedChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.CorporationOwnedBlueprintsCheckBox = ((System.Windows.Controls.CheckBox)(target));
            
            #line 37 "..\..\..\..\UserInterface\Industry\BlueprintsControl.xaml"
            this.CorporationOwnedBlueprintsCheckBox.Checked += new System.Windows.RoutedEventHandler(this.CorporationOwnedChanged);
            
            #line default
            #line hidden
            
            #line 37 "..\..\..\..\UserInterface\Industry\BlueprintsControl.xaml"
            this.CorporationOwnedBlueprintsCheckBox.Unchecked += new System.Windows.RoutedEventHandler(this.CorporationOwnedChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.InventableBlueprintsCheckBox = ((System.Windows.Controls.CheckBox)(target));
            
            #line 39 "..\..\..\..\UserInterface\Industry\BlueprintsControl.xaml"
            this.InventableBlueprintsCheckBox.Checked += new System.Windows.RoutedEventHandler(this.InventableChanged);
            
            #line default
            #line hidden
            
            #line 39 "..\..\..\..\UserInterface\Industry\BlueprintsControl.xaml"
            this.InventableBlueprintsCheckBox.Unchecked += new System.Windows.RoutedEventHandler(this.InventableChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.SearchTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 43 "..\..\..\..\UserInterface\Industry\BlueprintsControl.xaml"
            this.SearchTextBox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.SearchTextChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 57 "..\..\..\..\UserInterface\Industry\BlueprintsControl.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.CreateContract);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
