﻿#pragma checksum "C:\Users\wp7\Projects\wp7rt-client\wp7rt-client\Views\MovieDetailedView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "FCF56E4AB0ED77649AB541D0C49679FD"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.235
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace wp7rt_client.Views {
    
    
    public partial class MovieDetailedView : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.StackPanel TitlePanel;
        
        internal System.Windows.Controls.TextBlock movieTitile;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal System.Windows.Controls.Grid Thumbnail;
        
        internal System.Windows.Controls.TextBlock Title;
        
        internal System.Windows.Controls.TextBlock Year;
        
        internal System.Windows.Controls.TextBlock Runtime;
        
        internal System.Windows.Controls.TextBlock DVDRelease;
        
        internal System.Windows.Controls.TextBlock TheaterRelease;
        
        internal System.Windows.Controls.TextBlock CriticsScore;
        
        internal System.Windows.Controls.TextBlock AudienceScore;
        
        internal System.Windows.Controls.TextBlock Synopsis;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/wp7rt-client;component/Views/MovieDetailedView.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.TitlePanel = ((System.Windows.Controls.StackPanel)(this.FindName("TitlePanel")));
            this.movieTitile = ((System.Windows.Controls.TextBlock)(this.FindName("movieTitile")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.Thumbnail = ((System.Windows.Controls.Grid)(this.FindName("Thumbnail")));
            this.Title = ((System.Windows.Controls.TextBlock)(this.FindName("Title")));
            this.Year = ((System.Windows.Controls.TextBlock)(this.FindName("Year")));
            this.Runtime = ((System.Windows.Controls.TextBlock)(this.FindName("Runtime")));
            this.DVDRelease = ((System.Windows.Controls.TextBlock)(this.FindName("DVDRelease")));
            this.TheaterRelease = ((System.Windows.Controls.TextBlock)(this.FindName("TheaterRelease")));
            this.CriticsScore = ((System.Windows.Controls.TextBlock)(this.FindName("CriticsScore")));
            this.AudienceScore = ((System.Windows.Controls.TextBlock)(this.FindName("AudienceScore")));
            this.Synopsis = ((System.Windows.Controls.TextBlock)(this.FindName("Synopsis")));
        }
    }
}

