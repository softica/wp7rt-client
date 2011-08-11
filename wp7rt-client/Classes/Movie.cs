using System;
using System.Collections.Generic;


namespace wp7rt_client.Classes
{
    public class Movie : IComparable
    {
        public int RottenTomatoesId { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public string MpaaRating { get; set; }
        public int? Runtime { get; set; }
        public string Synopsis { get; set; }
        public List<ReleaseDate> ReleaseDates { get; set; }
        public List<Rating> Ratings { get; set; }
        public List<string> Genres { get; set; }
        public List<string> Directors { get; set; }
        public List<CastMember> Cast { get; set; }
        public List<Link> Links { get; set; }
        public List<Poster> Posters { get; set; }

        public string AudienceRating { get { return ConvertAudienceRatings(); }  }
        public string CriticsRating { get { return ConvertCriticsRatings(); } }
        public string ImageSourcePath { get { return ConvertImageSourcePath(); } }

        public Movie()
        {
            Directors = new List<string>();
            Genres = new List<string>();
            Cast = new List<CastMember>();
            Links = new List<Link>();
            Posters = new List<Poster>();
            ReleaseDates = new List<ReleaseDate>();
            Ratings = new List<Rating>();
        }

        #region LayoutAccessors

        public string ConvertAudienceRatings() 
        {
            string s;
            s = "Audience: no rating.";

            foreach (var elem in Ratings)
                {
                    if (elem.Type == "audience_score")
                    {
                        if (elem.Score != "-1")
                        {
                            s = "Audience: " + elem.Score + "%";
                        }                    
                    }
                }

            return s;

        }

        public string ConvertCriticsRatings()
        {
            string s;
            s = "Critics: no rating.";

            foreach (var elem in Ratings)
            {
                if (elem.Type == "critics_score" && elem.Score != "\"Certified Fresh\"")
                {
                    if (elem.Score != "-1")
                    {
                        s = "Critics: " + elem.Score + "%";
                    }
                }
            }

            return s;

        }

        public string ConvertImageSourcePath()
        {
            string path = "/wp7rt-client;component/Images/movie.jpg";

            bool certiiedFreshSet = false;

            foreach (var elem in Ratings)
            {

                if (elem.Type == "critics_rating" && elem.Score == "\"Certified Fresh\"")
                {
                    System.Diagnostics.Debug.WriteLine(elem.Type);
                    System.Diagnostics.Debug.WriteLine(elem.Score);
                    path = "/wp7rt-client;component/Images/CertifiedFresh_logo.png";
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
                            path = "/wp7rt-client;component/Images/rotten_logo.png";
                        }
                        else if (score >= 60)
                        {
                            path = "/wp7rt-client;component/Images/fresh.png";
                        }
                    }

                }

            }

            return path;

        }

        #endregion

        #region IComparable Members

        public int CompareTo(object obj)
        {
            if (obj == null || obj.GetType() != typeof(Movie))
            {
                throw new ArgumentException("Object is not a Movie type");
            }

            Movie temp = (Movie)obj;
            return (temp.RottenTomatoesId.CompareTo(this.RottenTomatoesId));
        }

        #endregion
    }

    public class ReleaseDate
    {
        public string Type { get; set; }
        public DateTime Date { get; set; }
    }

    public class CastMember
    {
        public string Name { get; set; }
        public List<string> Characters { get; set; }

        public CastMember()
        {
            Characters = new List<string>();
        }
    }

    public class Link
    {
        public string Type { get; set; }
        public string Url { get; set; }
    }

    public class Poster
    {
        public string Type { get; set; }
        public string Url { get; set; }
    }

    public class Rating
    {
        public string Type { get; set; }
        public string Score { get; set; }
    } 
}
