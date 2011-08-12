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

        public string AudienceRating { get { return ConvertAudienceRatings("Audience: no rating.","Audience: "); }  }
        public string CriticsRating { get { return ConvertCriticsRatings("Critics: no rating.", "Critics: "); } }
        public string ImageSourcePath { get { return ConvertImageSourcePath(); } }
        public string AudienceRatingPerCent { get { return ConvertAudienceRatings("NA",""); } }
        public string CriticsRatingPerCent { get { return ConvertCriticsRatings("NA", ""); } }
        public string CastMembers { get { return ConvertCastMembers(); } }
        public string MovieDirectors { get { return ConvertDirectorsMembers(); } }
        public string MovieGenres { get { return ConvertMovieGenres(); } }
        public string TheatersReleaseDate { get { return ConvertTheaterReleaseDate(); } }
        public string DVDReleaseDate { get { return ConvertDVDReleaseDate(); } }
        public string SmallPoster { get { return ConvertPoster(); } }

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

        public string ConvertAudienceRatings(string defaultstring, string nondefault) 
        {
            string s;
            s = defaultstring;

            foreach (var elem in Ratings)
                {
                    if (elem.Type == "audience_score")
                    {
                        if (elem.Score != "-1")
                        {
                            s = nondefault + elem.Score + "%";
                        }                    
                    }
                }

            return s;

        }

        public string ConvertCastMembers()
        {
            string r = "";
            foreach (var member in Cast)
            {
                r = r + member.Name + ", ";
            }
            r = r.Remove(r.Length - 2);
            return r;
        }

        public string ConvertDirectorsMembers()
        {
            string r = "Directed by: ";
            foreach (var member in Directors)
            {
                System.Diagnostics.Debug.WriteLine(member);
                r = r + member + ", ";
            }
            r = r.Remove(r.Length - 2);
            return r;
        }

        public string ConvertMovieGenres()
        {
            System.Diagnostics.Debug.WriteLine("Genres");
            System.Diagnostics.Debug.WriteLine(Genres);
            string r = "";
            foreach (var member in Genres)
            {
                System.Diagnostics.Debug.WriteLine(member);
                r = r + member + ", ";
            }
            r = r.Remove(r.Length - 2);
            return r;
        }

        public string ConvertCriticsRatings(string defaultstring, string nondefault)
        {
            string s;
            s = defaultstring;

            foreach (var elem in Ratings)
            {
                if (elem.Type == "critics_score" && elem.Score != "\"Certified Fresh\"")
                {
                    if (elem.Score != "-1")
                    {
                        s = nondefault + elem.Score + "%";
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

        public string ConvertTheaterReleaseDate()
        {
            string s="";
            
            foreach (var elem in ReleaseDates)
            {
                if (elem.Type == "theater")
                {
                    s = "In theaters: " + elem.Date;
                }
            }

            return s;
        }

        public string ConvertDVDReleaseDate()
        {
            string s="";

            foreach (var elem in ReleaseDates)
            {
                if (elem.Type == "dvd")
                {
                    s = "DVD: " + elem.Date;
                }
            }

            return s;
        }

        public string ConvertPoster()
        {
            string s = "";

            foreach (var elem in Posters)
            {
                if (elem.Type == "profile")
                {
                    s = elem.Url;
                }
            }

            return s;
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
