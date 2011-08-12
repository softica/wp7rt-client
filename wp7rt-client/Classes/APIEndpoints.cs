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

namespace wp7rt_client.Classes
{
    public class APIEndpoints
    {
        //For future reference, this website lists all of the available endpoints:
        //http://developer.rottentomatoes.com/docs/read/JSON

        /// <summary>
        /// Application API key.
        /// </summary>
        public const string API_KEY = "u4c9z4ewwk2x7dxd6skss4yw";

        /// <summary>
        /// Results per page (default 10).
        /// </summary>
        public const string PAGE_LIMIT = "10";

        /// <summary>
        /// Results per page (default 10).
        /// </summary>
        public const string QUERY = "";

        /// <summary>
        /// Endpoint for searching for movies via a text query.
        /// </summary>
        public const string MOVIE_SEARCH = @"http://api.rottentomatoes.com/api/public/v1.0/movies.json?apikey=" + API_KEY + "&q={0}&page_limit={1}";

        /// <summary>
        /// Endpoint for searching for an individual movie via it's RottenTomatoes ID number.
        /// </summary>
        public const string MOVIE_INDIVIDUAL_INFORMATION = @"http://api.rottentomatoes.com/api/public/v1.0/movies/{0}.json?apikey=" + API_KEY;

        /// <summary>
        /// Endpoint for searching for reviews for an individual movie.
        /// </summary>
        public const string MOVIE_INDIVIDUAL_REVIEWS = @"http://api.rottentomatoes.com/api/public/v1.0/movies/{1}/reviews.json?apikey={0}";

        /// <summary>
        /// Endpoint for searching for the cast of an individual movie.
        /// </summary>
        public const string MOVIE_INDIVIDUAL_CAST = @"http://api.rottentomatoes.com/api/public/v1.0/movies/{1}/cast.json?apikey={0}";

        /// <summary>
        /// Endpoint for listing the current box office.
        /// </summary>
        public const string LIST_BOX_OFFICE = @"http://api.rottentomatoes.com/api/public/v1.0/lists/movies/box_office.json?apikey=" + API_KEY;

        /// <summary>
        /// Endpoint for listing movies that are in theaters.
        /// </summary>
        public const string LIST_IN_THEATERS = @"http://api.rottentomatoes.com/api/public/v1.0/lists/movies/in_theaters.json?apikey=" + API_KEY;

        /// <summary>
        /// Endpoint for listing opening movies.
        /// </summary>
        public const string LIST_OPENING_SOON = @"http://api.rottentomatoes.com/api/public/v1.0/lists/movies/opening.json?apikey=" + API_KEY;

        /// <summary>
        /// Endpoint for listing upcoming movies.
        /// </summary>
        public const string LIST_UPCOMING = @"http://api.rottentomatoes.com/api/public/v1.0/lists/movies/upcoming.json?apikey=" + API_KEY;

        /// <summary>
        /// Endpoint for DVD top rentals
        /// </summary>
        public const string TOP_DVD_RENTALS = @"http://api.rottentomatoes.com/api/public/v1.0/lists/dvds/top_rentals.json?apikey=" + API_KEY;

        /// <summary>
        /// Endpoint for DVD current releases
        /// </summary>
        public const string DVD_CURRENT_RELEASES = @"http://api.rottentomatoes.com/api/public/v1.0/lists/dvds/current_releases.json?apikey=" + API_KEY;
    
        /// <summary>
        /// Endpoint for DVD new releases
        /// </summary>
        public const string DVD_NEW_RELEASES = @"http://api.rottentomatoes.com/api/public/v1.0/lists/dvds/new_releases.json?apikey=" + API_KEY;
    
        /// <summary>
        /// Endpoint for DVD upcoming
        /// </summary>
        public const string DVD_UPCOMING = @"http://api.rottentomatoes.com/api/public/v1.0/lists/dvds/upcoming.json?apikey=" + API_KEY;

        /// <summary>
        /// Endpoint for DVD upcoming
        /// </summary>
        public const string SIMILAR = @"http://api.rottentomatoes.com/api/public/v1.0/movies/{0}/similar.json?apikey=" + API_KEY;  
    }
}
