using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieApi.Data;
using MovieApi.Models;

namespace MovieApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WatchlistController : ControllerBase
    {
        private readonly AppDbContext db;

        public WatchlistController(AppDbContext context)
        {
            db = context;
        }

        
        [HttpPost]
        public IActionResult Add([FromBody] AddToWatchlistDto dto)
        {
            var movie = db.Movies.FirstOrDefault(x => x.Title == dto.Title);

            if (movie == null)
            {
                movie = new Movie
                {
                    Title = dto.Title,
                    Poster = dto.Poster,
                    Genre = dto.Genre,
                    Year = dto.Year
                };

                db.Movies.Add(movie);
                db.SaveChanges();
            }


            // WATCHEDLISTTE VARSA SİL !!1
            var watched = db.WatchedLists
                .FirstOrDefault(x => x.UserId == dto.UserId && x.MovieId == movie.Id);

            if (watched != null)
            {
                db.WatchedLists.Remove(watched);
            }


            //  watchlistte var mı?
            var exists = db.WatchLists
                .Any(x => x.UserId == dto.UserId && x.MovieId == movie.Id);

            if (exists) 
            {
                return Ok(new { message = "zaten ekli" });
            }

            // ekle
            var watch = new Watchlist
            {
                UserId = dto.UserId,
                MovieId = movie.Id
            };

            db.WatchLists.Add(watch);
            db.SaveChanges();

            // return Ok("EKLENDİ");
            return Ok(new { message = "EKLENDİ" });
        }


        [HttpGet("{userId}")]
        public IActionResult GetByUser(int userId)
        {
            var list = db.WatchLists
                .Where(x => x.UserId == userId)
                .Select(x => new
                {
                    x.Id,
                    x.UserId,
                    x.Movie.Title,
                    x.Movie.Poster,
                    x.Movie.Genre,
                    x.Movie.Year
                })
                .ToList();

            return Ok(list);
        }
    }
}
