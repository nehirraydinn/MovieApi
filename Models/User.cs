using System.Collections.Generic;

namespace MovieApi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }

        public List<Watchlist> WatchLists { get; set; }
        public List<Watchedlist> WatchedLists { get; set; }
    }
}