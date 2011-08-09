﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

using wp7rt_client.Classes;

namespace wp7rt_client
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MoviesList/BoxOffice/", UriKind.Relative));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MoviesList/Query/" + QueryText.Text, UriKind.Relative));
        }

        private void Theaters_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MoviesList/Theaters/" , UriKind.Relative));
        }

        private void Openings_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MoviesList/Openings/", UriKind.Relative));
        }

        private void Upcoming_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MoviesList/Upcoming/", UriKind.Relative));
        }

        private void QueryText_GotFocus(object sender, RoutedEventArgs e)
        {
            QueryText.Text = "";
        }
    }
}