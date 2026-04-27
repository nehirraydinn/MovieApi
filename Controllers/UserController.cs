using Microsoft.AspNetCore.Mvc;
using MovieApi.Data;
using MovieApi.Models;

namespace MovieApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext db;

        public UserController(AppDbContext context)
        {
            db = context;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterDto dto)
        {
            // boş mu
            if (string.IsNullOrEmpty(dto.Username) || string.IsNullOrEmpty(dto.Password))
            {
                return BadRequest("username ve password boş olamaz");
            }

            // kullanıcı var mı
            var exists = db.Users.Any(x => x.Username == dto.Username);

            if (exists) 
            {
                return BadRequest("kullanıcı zaten var");
            }

            // yeni user oluştur
            var user = new User
            {
                Username = dto.Username,
                Password = dto.Password
            };

            // KAYDET
            db.Users.Add(user);
            db.SaveChanges();

            return Ok(new { message = "kayıt başarılı" });
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto dto)
        {
            // boş kontrol
            if (string.IsNullOrEmpty(dto.Username) || string.IsNullOrEmpty(dto.Password))
            {
                return BadRequest("boş bırakılamaz!");
            }

            // kullanıcı var mı
            var user = db.Users.FirstOrDefault(x =>
                x.Username == dto.Username &&
                x.Password == dto.Password);

            if (user == null)
            {
                return BadRequest("kullanıcı adı veya şifre yanlış");
            }

            return Ok(new { message = "giriş başarılı",
                userId = user.Id,
                username = user.Username
            });
        }
    }
}