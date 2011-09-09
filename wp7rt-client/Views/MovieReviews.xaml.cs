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
using Microsoft.Phone.Tasks;
using System.Net.NetworkInformation;

namespace wp7rt_client.Views
{
    public partial class MovieReviews : PhoneApplicationPage
    {
        bool isNetworkAvailable = NetworkInterface.GetIsNetworkAvailable();
        private Phone.Controls.ProgressIndicator progress;

        public MovieReviews()
        {
            InitializeComponent();
        }

        private void image1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainPage/", UriKind.Relative));
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            Movie movie = PhoneApplicationService.Current.State["Movie"] as Movie;
            
            if (this.progress == null)
            {
                this.progress = new Phone.Controls.ProgressIndicator();
            }
            
            if (!isNetworkAvailable)
            {
                MessageBox.Show("Network is not available!");
                NavigationService.Navigate(new Uri("/MainPage/", UriKind.Relative));
            }

            
            Dictionary<string, List<Movie>> MoviesDict = PhoneApplicationService.Current.State["MoviesDict"] as Dictionary<string, List<Movie>>;
            string Type = PhoneApplicationService.Current.State["Type"] as string;

            bool MovieCached = false;

            foreach (var MovieItem in MoviesDict[Type])
            {
                if (MovieItem.RottenTomatoesId == movie.RottenTomatoesId && movie.Reviews != null)
                {
                    DisplayReviews(MovieItem);
                    MovieCached = true;
                    break;
                }
            }

            if (!MovieCached)
            {

                var url = String.Format(APIEndpoints.MOVIE_INDIVIDUAL_REVIEWS, movie.RottenTomatoesId);
                Uri uri = new Uri(url, UriKind.Absolute);

                WebClient client = new WebClient();
                client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(client_OpenReadCompleted);
                client.DownloadStringAsync(uri);

                this.progress.ProgressType = Phone.Controls.ProgressTypes.WaitCursor;
                this.progress.Show();

            }

        }

        private void client_OpenReadCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            string jsonResponse = e.Result.ToString();
            Movie movie = PhoneApplicationService.Current.State["Movie"] as Movie;

            movie = Parser.ParseReviews(jsonResponse, movie);

            this.progress.Hide();

            Dictionary<string, List<Movie>> MoviesDict = PhoneApplicationService.Current.State["MoviesDict"] as Dictionary<string, List<Movie>>;
            string Type = PhoneApplicationService.Current.State["Type"] as string;

            for (int i = 0; i < MoviesDict[Type].Count; i++)
            {
                if (MoviesDict[Type][i].RottenTomatoesId == movie.RottenTomatoesId)
                {
                    MoviesDict[Type][i].Reviews = movie.Reviews;
                    break;
                }
            } 

            DisplayReviews(movie);
        }

        private void DisplayReviews(Movie movie)
        {

            List<Review> list = new List<Review>();

            foreach (var review in movie.Reviews.MovieReviews)
            {
                if (review.AbsoluteLink != "")
                {
                    list.Add(review);
                }
            }

            ReviewsList.ItemsSource = list;

        }

        private void ReviewsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ReviewsList.SelectedItem != null)
            {
                Review review = ReviewsList.SelectedItem as Review;
                ReviewsList.SelectedIndex = -1;
                WebBrowserTask task = new WebBrowserTask();
                task.Uri = new Uri(review.AbsoluteLink, UriKind.Absolute);
                task.Show();
            }
        }

    }
}