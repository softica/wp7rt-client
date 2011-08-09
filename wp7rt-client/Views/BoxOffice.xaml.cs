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
    public partial class BoxOffice : PhoneApplicationPage
    {
        public BoxOffice()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (listBoxOffice.Items.Count == 0)
            {
                
                Uri boxOfficeUri = new Uri(APIEndpoints.LIST_BOX_OFFICE, UriKind.Absolute);
                
                WebClient client = new WebClient();
                client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(client_OpenReadCompleted);
                client.DownloadStringAsync(boxOfficeUri);

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
                System.Diagnostics.Debug.WriteLine(movie.Title);
            }

            listBoxOffice.ItemsSource = list;
            
        }

        private void listBoxOffice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBoxOffice.SelectedItem != null)
            {
                Movie mv = listBoxOffice.SelectedItem as Movie;
                PhoneApplicationService.Current.State["Movie"] = listBoxOffice.SelectedItem;
                NavigationService.Navigate(new Uri("/MovieDetailedView/" + mv.RottenTomatoesId, UriKind.Relative));
            }
        }
    }
}