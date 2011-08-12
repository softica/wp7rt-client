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

namespace wp7rt_client.Views
{
    public partial class MoviesList : PhoneApplicationPage
    {
        public MoviesList()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            string query;
            NavigationContext.QueryString.TryGetValue("Query", out query);

            string type;
            NavigationContext.QueryString.TryGetValue("Type", out type);          

            // Movies
            if (type == "Theaters")
            {
                ListMoviesInTheatres();
            }
            else if (type == "BoxOffice")
            {
                ListMoviesInBoxOffice();
            }
            else if (type == "Query")
            {
                ListMoviesSearch(query);
            }
            else if (type == "Openings")
            {
                ListMoviesOpenings();
            }
            else if (type == "Upcoming")
            {
                ListMoviesUpcoming();
            } 
            //DVD
            else if (type == "DVDTop")
            {
                ListMoviesDVDTop();
            }
            else if (type == "DVDCurrentReleases")
            {
                ListMoviesDVDCurrentReleases();
            }
            else if (type == "DVDNewReleases")
            {
                ListMoviesDVDNewReleases();
            }
            else if (type == "DVDUpcoming")
            {
                ListMoviesDVDUpcoming();
            }
            else if (type == "Similar")
            {
                ListMoviesSimilar(query);
            }
        }

        private void ListMoviesInTheatres()
        {
            PageTitle.Text = "In Theaters";

            if (moviesList.Items.Count == 0)
            {

                Uri uri = new Uri(APIEndpoints.LIST_IN_THEATERS, UriKind.Absolute);

                WebClient client = new WebClient();
                client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(client_OpenReadCompleted);
                client.DownloadStringAsync(uri);

            }
        }

        private void ListMoviesInBoxOffice()
        {
            PageTitle.Text = "Box Office";

            if (moviesList.Items.Count == 0)
            {

                Uri uri = new Uri(APIEndpoints.LIST_BOX_OFFICE, UriKind.Absolute);

                WebClient client = new WebClient();
                client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(client_OpenReadCompleted);
                client.DownloadStringAsync(uri);

            }
        }

        private void ListMoviesSearch(string query)
        {
            PageTitle.Text = "Search Results";

            if (moviesList.Items.Count == 0)
            {

                var url = String.Format(APIEndpoints.MOVIE_SEARCH, query, APIEndpoints.PAGE_LIMIT);
                Uri uri = new Uri(url, UriKind.Absolute);

                WebClient client = new WebClient();
                client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(client_OpenReadCompleted);
                client.DownloadStringAsync(uri);

            }
        }

        private void ListMoviesOpenings()
        {
            PageTitle.Text = "Openings";

            if (moviesList.Items.Count == 0)
            {

                Uri uri = new Uri(APIEndpoints.LIST_OPENING_SOON, UriKind.Absolute);

                WebClient client = new WebClient();
                client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(client_OpenReadCompleted);
                client.DownloadStringAsync(uri);

            }
        }

        private void ListMoviesUpcoming()
        {
            PageTitle.Text = "Upcoming";

            if (moviesList.Items.Count == 0)
            {

                Uri uri = new Uri(APIEndpoints.LIST_UPCOMING, UriKind.Absolute);

                WebClient client = new WebClient();
                client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(client_OpenReadCompleted);
                client.DownloadStringAsync(uri);

            }
        }

        private void ListMoviesDVDNewReleases()
        {
            PageTitle.Text = "DVD New Releases";

            if (moviesList.Items.Count == 0)
            {

                Uri uri = new Uri(APIEndpoints.DVD_NEW_RELEASES, UriKind.Absolute);

                WebClient client = new WebClient();
                client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(client_OpenReadCompleted);
                client.DownloadStringAsync(uri);

            }
        }

        private void ListMoviesDVDCurrentReleases()
        {
            PageTitle.Text = "DVD Current Releases";

            if (moviesList.Items.Count == 0)
            {

                Uri uri = new Uri(APIEndpoints.DVD_CURRENT_RELEASES, UriKind.Absolute);

                WebClient client = new WebClient();
                client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(client_OpenReadCompleted);
                client.DownloadStringAsync(uri);

            }
        }

        private void ListMoviesDVDTop()
        {
            PageTitle.Text = "DVD Top Rentals";

            if (moviesList.Items.Count == 0)
            {

                Uri uri = new Uri(APIEndpoints.TOP_DVD_RENTALS, UriKind.Absolute);

                WebClient client = new WebClient();
                client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(client_OpenReadCompleted);
                client.DownloadStringAsync(uri);

            }
        }

        private void ListMoviesDVDUpcoming()
        {
            PageTitle.Text = "DVD Upcoming";

            if (moviesList.Items.Count == 0)
            {

                Uri uri = new Uri(APIEndpoints.DVD_UPCOMING, UriKind.Absolute);

                WebClient client = new WebClient();
                client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(client_OpenReadCompleted);
                client.DownloadStringAsync(uri);

            }
        }

        private void ListMoviesSimilar(string query)
        {
            PageTitle.Text = "Similar Movies";

            if (moviesList.Items.Count == 0)
            {

                var url = String.Format(APIEndpoints.SIMILAR, query, APIEndpoints.PAGE_LIMIT);
                Uri uri = new Uri(url, UriKind.Absolute);

                WebClient client = new WebClient();
                client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(client_OpenReadCompleted);
                client.DownloadStringAsync(uri);

            }
        }

        private void client_OpenReadCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            string jsonResponse = e.Result.ToString();
            var movies = Parser.ParseMovieSearchResults(jsonResponse);

            List<Movie> list = new List<Movie>();

            foreach (var movie in movies)
            {
                list.Add(movie);
            }

            moviesList.ItemsSource = list;

        }      


        private void moviesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (moviesList.SelectedItem != null)
            {
                PhoneApplicationService.Current.State["Movie"] = moviesList.SelectedItem;
                NavigationService.Navigate(new Uri("/MovieDetailedView/", UriKind.Relative));
            }
        }
    }
}