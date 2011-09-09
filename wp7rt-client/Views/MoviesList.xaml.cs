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
using System.Net.NetworkInformation;
using Phone.Controls;

namespace wp7rt_client.Views
{
    public partial class MoviesList : PhoneApplicationPage
    {        
        bool isNetworkAvailable = NetworkInterface.GetIsNetworkAvailable();
        private Phone.Controls.ProgressIndicator progress; 
  
        public MoviesList()
        {
            InitializeComponent();            
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (!isNetworkAvailable)
            {
                MessageBox.Show("Network is not available!");
                NavigationService.Navigate(new Uri("/MainPage/", UriKind.Relative));
            }

            if (this.progress == null)
            {
                this.progress = new Phone.Controls.ProgressIndicator();
            }

            string query;
            NavigationContext.QueryString.TryGetValue("Query", out query);

            string type;
            NavigationContext.QueryString.TryGetValue("Type", out type);

            //set page title
            if (type == "Theaters")
            {
                PageTitle.Text = "In Theaters";
            }
            else if (type == "BoxOffice")
            {
                PageTitle.Text = "Box Office";
            }
            else if (type == "Query")
            {
                PageTitle.Text = "Search Results";
            }
            else if (type == "Openings")
            {
                PageTitle.Text = "Openings";
            }
            else if (type == "Upcoming")
            {
                PageTitle.Text = "Upcoming";
            }
            //DVD
            else if (type == "DVDTop")
            {
                PageTitle.Text = "DVD Top Rentals";
            }
            else if (type == "DVDCurrentReleases")
            {
                PageTitle.Text = "DVD Current Releases";
            }
            else if (type == "DVDNewReleases")
            {
                PageTitle.Text = "DVD New Releases";
            }
            else if (type == "DVDUpcoming")
            {
                PageTitle.Text = "DVD Upcoming";
            }
            else if (type == "Similar")
            {
                PageTitle.Text = "Similar Movies";
            }

            Dictionary<string, List<Movie>> MoviesDict = PhoneApplicationService.Current.State["MoviesDict"] as Dictionary<string, List<Movie>>;
            PhoneApplicationService.Current.State["Type"] = type;

            MoviesDict = CheckMoviesDictionary(MoviesDict, type);

            //check if movies list is already cached
            //caching should not be active for custom movie search query and similar movies functionality
            if (MoviesDict.ContainsKey(type) && MoviesDict[type].Count != 0 && type != "Similar" && type != "Query")
            {
                DisplayMovies(MoviesDict, type);
            }
            //list is not cached - query rottentomatoes API then
            else
            {
                // Movies
                if (type == "Theaters")
                {
                    QueryRottenTomatoes(APIEndpoints.LIST_IN_THEATERS);                    
                }
                else if (type == "BoxOffice")
                {
                    QueryRottenTomatoes(APIEndpoints.LIST_BOX_OFFICE);                                        
                }
                else if (type == "Query")
                {
                    string url = String.Format(APIEndpoints.MOVIE_SEARCH, query, APIEndpoints.PAGE_LIMIT);
                    QueryRottenTomatoes(url);                        
                }
                else if (type == "Openings")
                {
                    QueryRottenTomatoes(APIEndpoints.LIST_OPENING_SOON);                         
                }
                else if (type == "Upcoming")
                {
                    QueryRottenTomatoes(APIEndpoints.LIST_UPCOMING);                                                                 
                }
                //DVD
                else if (type == "DVDTop")
                {
                    QueryRottenTomatoes(APIEndpoints.TOP_DVD_RENTALS);                                                                                     
                }
                else if (type == "DVDCurrentReleases")
                {
                    QueryRottenTomatoes(APIEndpoints.DVD_CURRENT_RELEASES);                                                                                                         
                }
                else if (type == "DVDNewReleases")
                {
                    QueryRottenTomatoes(APIEndpoints.DVD_NEW_RELEASES);                             
                }
                else if (type == "DVDUpcoming")
                {
                    QueryRottenTomatoes(APIEndpoints.DVD_UPCOMING);                                                 
                }
                else if (type == "Similar")
                {
                    string url = String.Format(APIEndpoints.SIMILAR, query, APIEndpoints.PAGE_LIMIT);
                    QueryRottenTomatoes(url);            
                }
            }
        }

        private Dictionary<string, List<Movie>> CheckMoviesDictionary(Dictionary<string, List<Movie>> d, string type)
        {
            if (!d.ContainsKey(type))
            {
                List<Movie> MoviesList = new List<Movie>();
                d.Add(type, MoviesList);
            }

            return d;
        }

        private void QueryRottenTomatoes(string query)
        {
            if (moviesList.Items.Count == 0)
            {
                Uri uri = new Uri(query, UriKind.Absolute);

                WebClient client = new WebClient();
                client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(client_OpenReadCompleted);
                client.DownloadStringAsync(uri);

                this.progress.ProgressType = ProgressTypes.WaitCursor;
                this.progress.Show();
            }
        }

        private void client_OpenReadCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            Dictionary<string, List<Movie>> MoviesDict = PhoneApplicationService.Current.State["MoviesDict"] as Dictionary<string, List<Movie>>;
            string Type = PhoneApplicationService.Current.State["Type"] as string;

            List<Movie> ListOfMovies = new List<Movie>();

            string jsonResponse = e.Result.ToString();
            var movies = Parser.ParseMovieSearchResults(jsonResponse);
                      
            foreach (var movie in movies)
            {
                ListOfMovies.Add(movie);
            }

            SimilarNotFound.Text = "";
            MoviesDict[Type] = ListOfMovies;

            DisplayMovies(MoviesDict, Type);

            this.progress.Hide();

        }

        private void DisplayMovies(Dictionary<string, List<Movie>> d, string Type)
        {
            if ((Type == "Similar" || Type == "Query") && d[Type].Count == 0)
            {
                SimilarNotFound.Text = "No movies found...";
            }
            else
            {
                moviesList.ItemsSource = d[Type];
            }
        }


        private void moviesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (moviesList.SelectedItem != null)
            {                
                PhoneApplicationService.Current.State["Movie"] = moviesList.SelectedItem;
                
                moviesList.SelectedIndex = -1; //clear the selection index 

                NavigationService.Navigate(new Uri("/MovieDetailedView/", UriKind.Relative));
                
            }
        }

        private void image1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainPage/", UriKind.Relative));
        }


    }
}