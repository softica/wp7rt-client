﻿#pragma checksum "C:\Users\wp7\Desktop\Projects\wp7rt-client\wp7rt-client\Views\MovieDetailedView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "8B5DF6B0EBC5C592990FAA4483F87E9C"
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
using Phone.Controls;
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
        
        internal System.Windows.Controls.Image image1;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal System.Windows.Controls.TextBlock PageTitle;
        
        internal System.Windows.Controls.Image poster;
        
        internal System.Windows.Controls.TextBlock Runtime;
        
        internal System.Windows.Controls.TextBlock MPAA;
        
        internal System.Windows.Controls.TextBlock Year;
        
        internal System.Windows.Controls.TextBlock DVD;
        
        internal System.Windows.Controls.TextBlock InTheaters;
        
        internal System.Windows.Controls.TextBlock Genres;
        
        internal System.Windows.Controls.TextBlock AudiencePerCent;
        
        internal System.Windows.Controls.TextBlock CriticsPerCent;
        
        internal System.Windows.Controls.Image RatingImage;
        
        internal System.Windows.Controls.TextBlock Directors;
        
        internal System.Windows.Controls.TextBlock Cast;
        
        internal Phone.Controls.ScrollableTextBlock Consensus;
        
        internal System.Windows.Controls.Button DirectLink;
        
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
            this.image1 = ((System.Windows.Controls.Image)(this.FindName("image1")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.PageTitle = ((System.Windows.Controls.TextBlock)(this.FindName("PageTitle")));
            this.poster = ((System.Windows.Controls.Image)(this.FindName("poster")));
            this.Runtime = ((System.Windows.Controls.TextBlock)(this.FindName("Runtime")));
            this.MPAA = ((System.Windows.Controls.TextBlock)(this.FindName("MPAA")));
            this.Year = ((System.Windows.Controls.TextBlock)(this.FindName("Year")));
            this.DVD = ((System.Windows.Controls.TextBlock)(this.FindName("DVD")));
            this.InTheaters = ((System.Windows.Controls.TextBlock)(this.FindName("InTheaters")));
            this.Genres = ((System.Windows.Controls.TextBlock)(this.FindName("Genres")));
            this.AudiencePerCent = ((System.Windows.Controls.TextBlock)(this.FindName("AudiencePerCent")));
            this.CriticsPerCent = ((System.Windows.Controls.TextBlock)(this.FindName("CriticsPerCent")));
            this.RatingImage = ((System.Windows.Controls.Image)(this.FindName("RatingImage")));
            this.Directors = ((System.Windows.Controls.TextBlock)(this.FindName("Directors")));
            this.Cast = ((System.Windows.Controls.TextBlock)(this.FindName("Cast")));
            this.Consensus = ((Phone.Controls.ScrollableTextBlock)(this.FindName("Consensus")));
            this.DirectLink = ((System.Windows.Controls.Button)(this.FindName("DirectLink")));
        }
    }
}

