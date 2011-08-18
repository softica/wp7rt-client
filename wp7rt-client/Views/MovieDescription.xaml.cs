using System;
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
using Microsoft.Phone.Shell;
using wp7rt_client.Classes;

namespace wp7rt_client.Views
{
    public partial class MovieDescription : PhoneApplicationPage
    {
        public MovieDescription()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            Movie movie = PhoneApplicationService.Current.State["Movie"] as Movie;

            MovieTitleMedium.Text = movie.Title;
            if (movie.Synopsis == "")
            {
                MovieDescriptionText.Text = "No synopsis available...";
            }
            else
            {
                MovieDescriptionText.Text = movie.Synopsis;
            }


        }

        private void image1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainPage/", UriKind.Relative));
        }

        private void Button_Click_Clips(object sender, RoutedEventArgs e)
        {
            if (PhoneApplicationService.Current.State.ContainsKey("Movie"))
            {
                Movie movie = PhoneApplicationService.Current.State["Movie"] as Movie;
                NavigationService.Navigate(new Uri("/MovieClips/", UriKind.Relative));
            }
        }

        private void Button_Click_Reviews(object sender, RoutedEventArgs e)
        {

        }
    }
}