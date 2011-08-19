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
using wp7rt_client.Classes;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;

namespace wp7rt_client.Views
{
    public partial class MovieClips : PhoneApplicationPage
    {
        public MovieClips()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            Movie movie = PhoneApplicationService.Current.State["Movie"] as Movie;

            var url = String.Format(APIEndpoints.MOVIE_CLIPS, movie.RottenTomatoesId);
            Uri uri = new Uri(url, UriKind.Absolute);
                        
            WebClient client = new WebClient();
            client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(client_OpenReadCompleted);
            client.DownloadStringAsync(uri);            

        }

        private void client_OpenReadCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            string jsonResponse = e.Result.ToString();
            Movie movie = PhoneApplicationService.Current.State["Movie"] as Movie;

            movie = Parser.ParseClips(jsonResponse, movie);

            foreach (var clip in movie.MovieClips.Clips)
            {
                System.Diagnostics.Debug.WriteLine(clip.Title);
            }            

            DisplayClips(movie);
        }

        private void DisplayClips(Movie movie)
        {
            
            List<Clip> list = new List<Clip>();

            foreach (var clip in movie.MovieClips.Clips)
            {
                if (clip.AbsoluteLink != "")
                {
                    list.Add(clip);
                }
            }

            ClipsList.ItemsSource = list;

        }

        
        private void image1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainPage/", UriKind.Relative));
        }

        private void ClipsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ClipsList.SelectedItem != null)
            {
                Clip clip = ClipsList.SelectedItem as Clip;                
                WebBrowserTask task = new WebBrowserTask();
                task.Uri = new Uri(clip.AbsoluteLink, UriKind.Absolute);
                task.Show();
            }
        }

    }
}