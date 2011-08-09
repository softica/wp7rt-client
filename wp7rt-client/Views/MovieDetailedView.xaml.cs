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

            Title.Text = movie.Title;
            Year.Text = movie.Year.ToString();
            Runtime.Text = movie.Runtime.ToString();
            Synopsis.Text = movie.Synopsis;

            System.Diagnostics.Debug.WriteLine(movie.Title);
            System.Diagnostics.Debug.WriteLine("End of debug!");                     

        }



    }
}