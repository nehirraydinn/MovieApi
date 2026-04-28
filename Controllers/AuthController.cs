using Google.Apis.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieApi.Data;
using MovieApi.Models;

namespace MovieApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext db;

        public AuthController(AppDbContext context)
        {
            db = context;
        }

        [HttpPost("google")]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleDto dto)
        {
            // token doğrulama
            var payload = await GoogleJsonWebSignature.ValidateAsync(dto.Token);

            var email = payload.Email;
            var name = payload.Name;

            var user = db.Users.FirstOrDefault(x => x.Email == email);

            if (user == null)
            {
                user = new User
                {
                    Email = email,
                    Username = name
                };

                db.Users.Add(user);
                db.SaveChanges();
            }

            return Ok(new
            {
                userId = user.Id,
                username = user.Username
            });
        }
    }
}
