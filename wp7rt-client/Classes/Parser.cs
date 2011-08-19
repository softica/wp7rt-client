using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using wp7rt_client.Classes;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace wp7rt_client.Classes
{
    public static class Parser
    {

        /// <summary>
        /// Parse a JSON string representing a Movie item
        /// </summary>
        /// <param name="json">The JSON string to be parsed</param>
        /// <returns>Movie object</returns>
        public static Movie ParseMovie(string json)
        {
            JObject jObject = JObject.Parse(json);
            Movie movie = new Movie();
                        
            movie.RottenTomatoesId = ParseRottenTomatoesId(jObject["id"]);
            movie.Title = ParseTitle(jObject["title"]);
            movie.Year = ParseYear(jObject["year"]);
            movie.MpaaRating = ParseMpaaRating(jObject["mpaa_rating"]);
            movie.Runtime = ParseRunTime(jObject["runtime"]);
            movie.Synopsis = ParseSynopsis(jObject["synopsis"]);
            movie.Directors = ParseDirectors(jObject["abridged_directors"]);
            movie.Genres = ParseGenres(jObject["genres"]);
            movie.Cast = ParseCastMembers(jObject["abridged_cast"]);
            movie.Links = ParseLinks(jObject["links"]);
            movie.Posters = ParsePosters(jObject["posters"]);
            movie.Ratings = ParseRatings(jObject["ratings"]);
            movie.ReleaseDates = ParseReleaseDates(jObject["release_dates"]);
            movie.CriticsConsensus = ParseCriticsConsensus(jObject["critics_consensus"]);
            return movie;
        }

        /// <summary>
        /// Parse a JSON string representing clips related to the movie
        /// </summary>
        /// <param name="json">The JSON string to be parsed</param>
        /// <returns>Movie object</returns>
        public static Movie ParseClips(string json, Movie movie)
        {
            JObject jObject = JObject.Parse(json);
                        
            movie.Clips = ParseMovieClips(jObject["clips"]);
            return movie;
        }

        /// <summary>
        /// Parse a JSON string representing reviews related to the movie
        /// </summary>
        /// <param name="json">The JSON string to be parsed</param>
        /// <returns>Movie object</returns>
        public static Movie ParseReviews(string json, Movie movie)
        {
            JObject jObject = JObject.Parse(json);

            movie.Reviews = ParseMovieReviews(jObject["reviews"]);
            return movie;
        }

        /// <summary>
        /// Parse Search Results For Movies
        /// </summary>
        /// <param name="json">JSON string to parse</param>
        /// <returns>MovieSearchResult object containing a list of Movie objects</returns>
        public static MovieSearchResults ParseMovieSearchResults(string json)
        {
            JObject jObject = JObject.Parse(json);

            var result = new MovieSearchResults();

            if (jObject["total"] != null)
                result.ResultCount = (int)jObject["total"];

            var movies = (JArray)jObject["movies"];
            if (movies != null)
            {
                foreach (var movie in movies)
                {
                    Movie m = ParseMovie(movie.ToString());
                    result.Add(m);
                }
            }

            return result;
        }

        #region "Individual attribute parsing."
        private static List<ReleaseDate> ParseReleaseDates(JToken jToken)
        {
            List<ReleaseDate> releaseDates = new List<ReleaseDate>();
            var jsonArray = (JObject)jToken;

            if (jsonArray == null)
                return releaseDates;

            foreach (var releaseDate in jsonArray)
            {
                ReleaseDate newDate = new ReleaseDate();
                newDate.Type = (string)releaseDate.Key;

                var tmpDate = ((string)releaseDate.Value).Substring(0, ((string)releaseDate.Value).Count());
                newDate.Date = DateTime.Parse(tmpDate);

                releaseDates.Add(newDate);
            }

            return releaseDates;
        }

        private static List<Rating> ParseRatings(JToken jToken)
        {
            List<Rating> ratings = new List<Rating>();
            var jsonArray = (JObject)jToken;

            if (jsonArray == null)
                return ratings;

            foreach (var rating in jsonArray)
            {

                Rating newRating = new Rating() { Type = (string)rating.Key, Score = rating.Value.ToString() };
                ratings.Add(newRating);
            }

            return ratings;
        }

        private static List<Poster> ParsePosters(JToken jToken)
        {
            List<Poster> posters = new List<Poster>();
            var jsonArray = (JObject)jToken;

            if (jsonArray == null)
                return posters;

            foreach (var poster in jsonArray)
            {
                Poster newPoster = new Poster() { Type = (string)poster.Key, Url = (string)poster.Value };
                posters.Add(newPoster);
            }

            return posters;
        }

        private static List<Link> ParseLinks(JToken jToken)
        {
            List<Link> links = new List<Link>();
            var jsonArray = (JObject)jToken;

            if (jsonArray == null)
                return links;

            foreach (var link in jsonArray)
            {
                Link newLink = new Link { Type = (string)link.Key, Url = (string)link.Value };
                links.Add(newLink);
            }

            return links;
        }

        private static List<Clip> ParseMovieClips(JToken jToken)
        {
            List<Clip> clips = new List<Clip>();
            var jsonArray = (JArray)jToken;

            if (jsonArray == null)
                return clips;

            foreach (var clip in jsonArray)
            {
                Clip newClip = new Clip();

                newClip.Title = (string)clip["title"];
                newClip.Duration = (string)clip["duration"];
                newClip.Thumbnail = (string)clip["thumbnail"];                

                var links = (JObject)clip["links"];

                if (links != null)
                {
                    foreach (var link in links)
                    {                           
                        Link newLink = new Link { Type = (string)link.Key, Url = (string)link.Value };
                        if ((string)link.Key == "alternate")
                        {
                            newClip.AbsoluteLink = (string)link.Value;
                        }
                        newClip.Links.Add(newLink);                        
                    }
                }

                clips.Add(newClip);
            }

            return clips;
        }

        private static Reviews ParseMovieReviews(JToken jToken)
        {
            List<Review> reviews = new List<Review>();
            Reviews MovieReviews = new Reviews();

            var jsonArray = (JArray)jToken;

            if (jsonArray == null)
                return MovieReviews;

            foreach (var review in jsonArray)
            {
                Review newReview = new Review();

                newReview.Critic = (string)review["critic"];
                newReview.Date = (string)review["date"];
                newReview.Freshness = (string)review["freshness"];
                newReview.Publication = (string)review["publication"];
                newReview.Quote = (string)review["quote"];

                if ((string)review["original_score"] != null) { newReview.OrgScore = (string)review["original_score"]; }               
                
                var links = (JObject)review["links"];

                if (links != null)
                {
                    foreach (var link in links)
                    {
                        Link newLink = new Link { Type = (string)link.Key, Url = (string)link.Value };
                        if ((string)link.Key == "review")
                        {
                            newReview.AbsoluteLink = (string)link.Value;
                        }
                        newReview.Links.Add(newLink);
                    }
                }

                MovieReviews.MovieReviews.Add(newReview);
            }

            return MovieReviews;
        }


        private static List<CastMember> ParseCastMembers(JToken jToken)
        {
            List<CastMember> cast = new List<CastMember>();
            var jsonArray = (JArray)jToken;

            if (jsonArray == null)
                return cast;

            foreach (var castMember in jsonArray)
            {
                CastMember member = new CastMember();
                member.Name = (string)castMember["name"];

                var characters = (JArray)castMember["characters"];
                if (characters != null)
                {
                    foreach (var character in characters)
                    {
                        member.Characters.Add((string)character);
                    }
                }

                cast.Add(member);
            }

            return cast;
        }

        private static List<string> ParseGenres(JToken jToken)
        {
            List<string> genres = new List<string>();
            var jsonArray = (JArray)jToken;

            if (jsonArray == null)
                return genres;

            genres.AddRange(jsonArray.Select(genre => (string)genre));
            return genres;
        }

        private static List<string> ParseDirectors(JToken jToken)
        {
            List<string> directors = new List<string>();
            var jsonArray = (JArray)jToken;

            if (jsonArray == null)
                return directors;

            directors.AddRange(jsonArray.Select(director => (string)director["name"]));
            return directors;
        }

        private static string ParseSynopsis(JToken jToken)
        {
            return jToken.Value<string>();
        }

        private static int? ParseRunTime(JToken jToken)
        {
            return jToken.Value<string>() == String.Empty ? -1 : jToken.Value<int>();
        }

        private static string ParseMpaaRating(JToken jToken)
        {
            return jToken == null ? String.Empty : jToken.Value<string>();
        }

        private static int ParseYear(JToken jToken)
        {
            return jToken.Value<string>() == String.Empty ? -1 : jToken.Value<int>();
        }

        private static string ParseTitle(JToken jToken)
        {
            return jToken.Value<string>();
        }

        private static int ParseRottenTomatoesId(JToken jToken)
        {
            return jToken.Value<int>();
        }

        private static string ParseCriticsConsensus(JToken jToken)
        {
            if (jToken != null)
            {
                return jToken.Value<string>();
            }

            return "";
        }
        
        #endregion

    }
}
