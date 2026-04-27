using System.Collections.Generic;

namespace MovieApi.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Poster { get; set; }
        public string Genre { get; set; }
        public string Year { get; set; }

        public List<Watchlist> WatchLists { get; set; }
        public List<Watchedlist> WatchedLists { get; set; }
    }
}
