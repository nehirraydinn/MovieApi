namespace MovieApi.Models
{
    public class AddToWatchedlistDto
    {
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Poster { get; set; }
        public string Genre { get; set; }
        public string Year { get; set; }
    }
}
