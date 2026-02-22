// using Microsoft.AspNetCore.Mvc;

// namespace khaoduan_api.Controllers;

// [ApiController]
// [Route("[controller]")]
// public class AuthController : ControllerBase
// {
//     private static readonly string[] Summaries =
//     [
//         "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
//     ];

//     [HttpGet(Name = "Login")]
//     public ActionResult<IEnumerable<WeatherForecast>> Get()
//     {
//         var result = Enumerable.Range(1, 5).Select(index => new WeatherForecast
//         {
//             Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//             TemperatureC = Random.Shared.Next(-20, 55),
//             Summary = Summaries[Random.Shared.Next(Summaries.Length)]
//         })
//         .ToArray();

//         return NotFound(result);
//     }
// }
