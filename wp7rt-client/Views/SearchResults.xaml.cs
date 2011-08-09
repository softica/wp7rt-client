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
    public partial class SearchResults : PhoneApplicationPage
    {
        public SearchResults()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            string query;
            NavigationContext.QueryString.TryGetValue("Query", out query);
            System.Diagnostics.Debug.WriteLine(query);

            if (listSearchResults.Items.Count == 0)
            {                
                var url = String.Format(APIEndpoints.MOVIE_SEARCH, query, APIEndpoints.PAGE_LIMIT);
                Uri searchUri = new Uri(url, UriKind.Absolute);
                
                WebClient client = new WebClient();
                client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(client_OpenReadCompleted);
                client.DownloadStringAsync(searchUri);

            }            
        }
                  
        void client_OpenReadCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            string jsonResponse = e.Result.ToString();
            var movies = Parser.ParseMovieSearchResults(jsonResponse);

            List<Movie> list = new List<Movie>();

            foreach (var movie in movies)
            {
                list.Add(movie);
            }

            listSearchResults.ItemsSource = list;
            
        }
        
        private void listSearchResults_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
              if (listSearchResults.SelectedItem != null)
            {
                PhoneApplicationService.Current.State["Movie"] = listSearchResults.SelectedItem;
                NavigationService.Navigate(new Uri("/MovieDetailedView/", UriKind.Relative));
            }
        }
    }
}