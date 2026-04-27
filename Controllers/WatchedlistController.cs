using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.Data;
using MovieApi.Models;

namespace MovieApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WatchedlistController : ControllerBase
    {
        private readonly AppDbContext db;

        public WatchedlistController(AppDbContext context)
        {
            db = context;
        }

        [HttpPost]
        public IActionResult Add([FromBody] AddToWatchedlistDto dto)  // DTO AYNI
        {
            // movie var mı?
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


            // VARSA WATCHLISTTEN SİL !!!!!
            var watch = db.WatchLists
                .FirstOrDefault(x => x.UserId == dto.UserId && x.MovieId == movie.Id);

            if (watch != null)
            {
                db.WatchLists.Remove(watch);
            }


            // watchedlistte var mı
            var exists = db.WatchedLists
                .Any(x => x.UserId == dto.UserId && x.MovieId == movie.Id);

            if (exists)
            {
                return Ok(new { message = "zaten izlenmiş" });
            }

            // ekle
            var watched = new Watchedlist
            {
                UserId = dto.UserId,
                MovieId = movie.Id
            };

            db.WatchedLists.Add(watched);
            db.SaveChanges();

            return Ok(new { message = "EKLENDİ" });
        }

        [HttpGet("{userId}")]
        public IActionResult GetByUser(int userId)
        {
            var list = db.WatchedLists
                .Include(x => x.Movie)
                .Where(x => x.UserId == userId)
                .Select(x => new
                {
                    x.Id,
                    x.UserId,
                    title = x.Movie.Title,
                    poster = x.Movie.Poster,
                    genre = x.Movie.Genre,
                    year = x.Movie.Year
                })
                .ToList();

            return Ok(list);
        }
    }
}