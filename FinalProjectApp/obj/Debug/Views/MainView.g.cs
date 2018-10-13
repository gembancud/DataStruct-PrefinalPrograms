﻿#pragma checksum "..\..\..\Views\MainView.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "A3B935C3417C6D665198ED052268F98CC9CABA52"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Caliburn.Micro;
using FinalProjectApp.Views;
using MahApps.Metro.Controls;
using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Transitions;
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


namespace FinalProjectApp.Views {
    
    
    /// <summary>
    /// MainView
    /// </summary>
    public partial class MainView : MahApps.Metro.Controls.MetroWindow, System.Windows.Markup.IComponentConnector {
        
        
        #line 32 "..\..\..\Views\MainView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MahApps.Metro.Controls.Flyout AddCarFlyout;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\Views\MainView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox AddCarName;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\..\Views\MainView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox CarDesignComboBox;
        
        #line default
        #line hidden
        
        
        #line 79 "..\..\..\Views\MainView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox AddCarFrom;
        
        #line default
        #line hidden
        
        
        #line 96 "..\..\..\Views\MainView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox AddCarTo;
        
        #line default
        #line hidden
        
        
        #line 110 "..\..\..\Views\MainView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox AddCarSpeed;
        
        #line default
        #line hidden
        
        
        #line 140 "..\..\..\Views\MainView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock MusicTextBlock;
        
        #line default
        #line hidden
        
        
        #line 159 "..\..\..\Views\MainView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid AppGrid;
        
        #line default
        #line hidden
        
        
        #line 190 "..\..\..\Views\MainView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas AppCanvas;
        
        #line default
        #line hidden
        
        
        #line 214 "..\..\..\Views\MainView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock TimeElapsed;
        
        #line default
        #line hidden
        
        
        #line 227 "..\..\..\Views\MainView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button LocationButton;
        
        #line default
        #line hidden
        
        
        #line 237 "..\..\..\Views\MainView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button RoadButton;
        
        #line default
        #line hidden
        
        
        #line 244 "..\..\..\Views\MainView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button StartButton;
        
        #line default
        #line hidden
        
        
        #line 289 "..\..\..\Views\MainView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView CarListView;
        
        #line default
        #line hidden
        
        
        #line 322 "..\..\..\Views\MainView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MaterialDesignThemes.Wpf.Snackbar AppSnackBar;
        
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
            System.Uri resourceLocater = new System.Uri("/FinalProjectApp;component/views/mainview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\MainView.xaml"
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
            this.AddCarFlyout = ((MahApps.Metro.Controls.Flyout)(target));
            return;
            case 2:
            this.AddCarName = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.CarDesignComboBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 4:
            this.AddCarFrom = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 5:
            this.AddCarTo = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 6:
            this.AddCarSpeed = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            
            #line 118 "..\..\..\Views\MainView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.CreateCarButtonClick);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 137 "..\..\..\Views\MainView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.PlayMusicClick);
            
            #line default
            #line hidden
            return;
            case 9:
            this.MusicTextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 10:
            
            #line 148 "..\..\..\Views\MainView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.NextMusicClick);
            
            #line default
            #line hidden
            return;
            case 11:
            this.AppGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 12:
            this.AppCanvas = ((System.Windows.Controls.Canvas)(target));
            return;
            case 13:
            this.TimeElapsed = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 14:
            this.LocationButton = ((System.Windows.Controls.Button)(target));
            
            #line 229 "..\..\..\Views\MainView.xaml"
            this.LocationButton.Click += new System.Windows.RoutedEventHandler(this.LocationButtonClick);
            
            #line default
            #line hidden
            return;
            case 15:
            this.RoadButton = ((System.Windows.Controls.Button)(target));
            
            #line 239 "..\..\..\Views\MainView.xaml"
            this.RoadButton.Click += new System.Windows.RoutedEventHandler(this.RoadButtonClick);
            
            #line default
            #line hidden
            return;
            case 16:
            this.StartButton = ((System.Windows.Controls.Button)(target));
            
            #line 245 "..\..\..\Views\MainView.xaml"
            this.StartButton.Click += new System.Windows.RoutedEventHandler(this.StartTimeClick);
            
            #line default
            #line hidden
            return;
            case 17:
            
            #line 279 "..\..\..\Views\MainView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.AddCarClick);
            
            #line default
            #line hidden
            return;
            case 18:
            
            #line 282 "..\..\..\Views\MainView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.RemoveCarClick);
            
            #line default
            #line hidden
            return;
            case 19:
            this.CarListView = ((System.Windows.Controls.ListView)(target));
            return;
            case 20:
            this.AppSnackBar = ((MaterialDesignThemes.Wpf.Snackbar)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

