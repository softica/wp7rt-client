using System;
using System.Collections.Generic;

namespace wp7rt_client.Classes
{
    public class Reviews
    {
        public List<Review> MovieReviews { get; set; }

        public Reviews()
        {
            MovieReviews = new List<Review>();

        }

    }

    public class Review
    {
        public string Critic { get; set; }
        public string Date { get; set; }
        public string Freshness { get; set; }
        public string Publication { get; set; }
        public string Quote { get; set; }
        public List<Link> Links { get; set; }
        public string AbsoluteLink { get; set; }
        public string OrgScore { get; set; }
                
        public Review()
        {
            Links = new List<Link>();
            AbsoluteLink = "";
            OrgScore = "NA";
        }

        
    }

}
