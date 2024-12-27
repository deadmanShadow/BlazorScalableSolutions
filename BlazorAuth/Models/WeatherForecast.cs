namespace BlazorAuth.Models
{
    public class WeatherForecast
    {
        public DateOnly Date { get; set; }
        public int TemperatureC { get; set; }
        public string? Summary { get; set; }
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }

    //Seeding
    public class WeatherForecastSeed
    {
        public static WeatherForecast[] Seed()
        {
            return new WeatherForecast[]
            {
                new WeatherForecast
                {
                    Date = new DateOnly(2021, 6, 1),
                    TemperatureC = 25,
                    Summary = "Hot"
                },
                new WeatherForecast
                {
                    Date = new DateOnly(2021, 6, 2),
                    TemperatureC = 20,
                    Summary = "Warm"
                },
                new WeatherForecast
                {
                    Date = new DateOnly(2021, 6, 3),
                    TemperatureC = 15,
                    Summary = "Cool"
                },
                new WeatherForecast
                {
                    Date = new DateOnly(2021, 6, 4),
                    TemperatureC = 10,
                    Summary = "Cold"
                },
                new WeatherForecast
                {
                    Date = new DateOnly(2021, 6, 5),
                    TemperatureC = 5,
                    Summary = "Freezing"
                }
            };
        }
    }
}
