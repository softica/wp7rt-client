using System;
using System.Collections.Generic;


namespace wp7rt_client.Classes
{
    public class MovieClips
    {
        public List<Clip> Clips { get; set; }

        public MovieClips()
        {
            Clips = new List<Clip>();
        }


    }

    public class Clip
    {
        public string Title { get; set; }
        public string Duration { get; set; }
        public string Thumbnail { get; set; }
        public List<Link> Links { get; set; }
        public string AbsoluteLink { get; set; }

        public Clip()
        {
            Links = new List<Link>();
            AbsoluteLink = "";
        }
    }


}
