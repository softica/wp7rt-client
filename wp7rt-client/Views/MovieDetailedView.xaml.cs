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
using Microsoft.Phone.Shell;
using System.Windows.Media.Imaging;
using Microsoft.Phone.Tasks;

namespace wp7rt_client.Views
{
    public partial class MovieDetailedView : PhoneApplicationPage
    {
        public MovieDetailedView()
        {
            InitializeComponent();            
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            Movie movie = PhoneApplicationService.Current.State["Movie"] as Movie;

            var url = String.Format(APIEndpoints.MOVIE_INDIVIDUAL_INFORMATION, movie.RottenTomatoesId);
            Uri uri = new Uri(url, UriKind.Absolute);

            WebClient client = new WebClient();
            client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(client_OpenReadCompleted);
            client.DownloadStringAsync(uri);

            //System.Diagnostics.Debug.WriteLine(movie.Genres);
            //System.Diagnostics.Debug.WriteLine(movie.Cast);

        }

        private void client_OpenReadCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            string jsonResponse = e.Result.ToString();
            Movie movie = Parser.ParseMovie(jsonResponse);

            

                PageTitle.Text = movie.Title;
                if (PageTitle.ActualWidth > PageTitle.Width)
                {
                    PageTitle.FontSize = PageTitle.FontSize * ((double)PageTitle.Width / (double)PageTitle.ActualWidth);
                }
                AudiencePerCent.Text = movie.AudienceRatingPerCent;
                CriticsPerCent.Text = movie.CriticsRatingPerCent;
                Cast.Text = "Cast: " + movie.CastMembers;
                Directors.Text = movie.MovieDirectors;
                //Synopsis.Text = movie.Synopsis;
                Genres.Text = movie.MovieGenres;
                DVD.Text = movie.DVDReleaseDate;
                InTheaters.Text = movie.TheatersReleaseDate;
                Year.Text = "Year: " + movie.Year.ToString();
                MPAA.Text = "MPAA Rating: " + movie.MpaaRating;
                Runtime.Text = "Runtime: " + movie.Runtime.ToString();
                if (movie.CriticsConsensus == "")
                {
                    Consensus.Text = "No critics consensus available yet...";
                }
                else
                {
                    Consensus.Text = movie.CriticsConsensus;
                }

                Uri u;
                u = new Uri(movie.ImageSourcePath, UriKind.Relative);
                
                RatingImage.Source = new BitmapImage(u);

                u = new Uri(movie.SmallPoster, UriKind.Absolute);
                poster.Source = new BitmapImage(u);

                //DirectLink.NavigateUri = new Uri(movie.rtDirectLink, UriKind.Absolute);

                PhoneApplicationService.Current.State["Movie"] = movie;

        }      


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (PhoneApplicationService.Current.State.ContainsKey("Movie"))
            {                
                Movie movie = PhoneApplicationService.Current.State["Movie"] as Movie;
                NavigationService.Navigate(new Uri("/MoviesList/Similar/" + movie.RottenTomatoesId.ToString(), UriKind.Relative));
            }
            
        }


        private void Button_Click_Direct_Link(object sender, RoutedEventArgs e)
        {
            if (PhoneApplicationService.Current.State.ContainsKey("Movie"))
            {
                Movie movie = PhoneApplicationService.Current.State["Movie"] as Movie;
                WebBrowserTask task = new WebBrowserTask();
                task.URL = movie.rtDirectLink;
                task.Show();
            }
        }

        private void Button_Click_Movie_Desc(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MovieDescription/", UriKind.Relative));
        }

        private void TitlePanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainPage/", UriKind.Relative));
        }

    }
}