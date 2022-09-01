using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TestServer.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class WeatherForecastController : ControllerBase
	{
		private static readonly string[] Summaries = new[]
		{
		"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
	};

		private readonly ILogger<WeatherForecastController> _logger;

		public WeatherForecastController(ILogger<WeatherForecastController> logger)
		{
			_logger = logger;
		}

		[HttpGet(Name = "GetAuthWeather")]
		[Route("GetAuthWeather")]
		[Authorize]
		public IEnumerable<WeatherForecast> GetWeatherForecast()
		{
			return Enumerable.Range(1, 5).Select(index => new WeatherForecast
			{
				Date = DateTime.Now.AddDays(index),
				TemperatureC = Random.Shared.Next(-20, 55),
				Summary = Summaries[Random.Shared.Next(Summaries.Length)]
			})
			.ToArray();
		}

		[HttpGet(Name = "GetNoAuthWeather")]
		[Route("GetNoAuthWeather")]
		public IEnumerable<WeatherForecast> GetNoAuthWeather()
		{
			return Enumerable.Range(1, 5).Select(index => new WeatherForecast
			{
				Date = DateTime.Now.AddDays(index),
				TemperatureC = Random.Shared.Next(-20, 55),
				Summary = Summaries[Random.Shared.Next(Summaries.Length)]
			})
				.ToArray();
		}

		public class WeatherForecast
		{
			public DateTime Date { get; set; }

			public int TemperatureC { get; set; }

			public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

			public string? Summary { get; set; }
		}
	}
}