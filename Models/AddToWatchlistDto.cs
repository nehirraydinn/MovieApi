namespace MovieApi.Models
{
    public class AddToWatchlistDto
    {
        public int UserId { get; set; }

        public string Title { get; set; }
        public string Poster { get; set; }
        public string Genre { get; set; }
        public string Year { get; set; }
    }
}
