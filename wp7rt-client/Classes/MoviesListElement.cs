using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using wp7rt_client.Classes;

namespace wp7rt_client.Classes
{
    public class MoviesListElement
    {
        public string Title { get; set; }
        public string RatingAudience { get; set; }
        public string RatingCritics { get; set; }
        public string ImageSourcePath { get; set; }
        
        public MoviesListElement()
        {
            ImageSourcePath = "/wp7rt-client;component/Images/movie.jpg";
            RatingAudience = "Audience: no rating";
            RatingCritics = "Critics: no rating";
        }
               
    }
}
