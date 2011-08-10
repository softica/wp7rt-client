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
            
            System.Diagnostics.Debug.WriteLine(query);
            System.Diagnostics.Debug.WriteLine(type);

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


        private void client_OpenReadCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            string jsonResponse = e.Result.ToString();
            var movies = Parser.ParseMovieSearchResults(jsonResponse);

            List<MoviesListElement> list = new List<MoviesListElement>();
            
            foreach (var movie in movies)
            {
                MoviesListElement element = new MoviesListElement();
                element.Title = movie.Title;

                bool certiiedFreshSet = false;

                foreach (var elem in movie.Ratings)
                {
                    if (elem.Type == "critics_score")
                    {
                        if (elem.Score != "-1")
                        {
                            element.RatingCritics = "Critics: " + elem.Score + "%";
                        }                        
                    }
                    else if (elem.Type == "audience_score")
                    {
                        if (elem.Score != "-1")
                        {
                            element.RatingAudience = "Audience: " + elem.Score + "%";
                        }                        
                    }

                    System.Diagnostics.Debug.WriteLine(elem.Type);
                    System.Diagnostics.Debug.WriteLine(elem.Score);

                    if (elem.Type == "critics_rating" && elem.Score == "\"Certified Fresh\"")
                    {
                        System.Diagnostics.Debug.WriteLine(elem.Type);
                        System.Diagnostics.Debug.WriteLine(elem.Score);
                        element.ImageSourcePath = "/wp7rt-client;component/Images/CertifiedFresh_logo.png";
                        certiiedFreshSet = true;
                    }
                    else if (elem.Type == "critics_score" && !certiiedFreshSet)
                    {
                        int score;
                        bool result = Int32.TryParse(elem.Score, out score);
                        if (result)
                        {
                            if (score < 60 && score != -1)
                            {
                                element.ImageSourcePath = "/wp7rt-client;component/Images/rotten_logo.png";
                            }
                            else if (score >= 60)
                            {
                                element.ImageSourcePath = "/wp7rt-client;component/Images/fresh.png";
                            }                            
                        }

                    }
 
                }
                list.Add(element);
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