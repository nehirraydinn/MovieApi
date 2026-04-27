using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;

namespace MovieApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MovieController : ControllerBase
{
    [HttpGet("test")]
    public IActionResult Test()
    {
        return Ok("çalışıyor");
    }


    [HttpGet("search")]
    public async Task<IActionResult> Search(string name)
    {
        var apiKey = "591a0ae5";

        var client = new HttpClient();

        var url = $"http://www.omdbapi.com/?s={name}&apikey={apiKey}";

        var response = await client.GetStringAsync(url);

        var json = JsonSerializer.Deserialize<object>(response);

        return Ok(json);
    }
}