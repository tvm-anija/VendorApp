using System;

namespace WebApi
{
    /// <summary>
    /// The weather forecast model class
    /// </summary>
    public class WeatherForecast
    {
        /// <summary>
        /// The date
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// The Temperature in C
        /// </summary>
        public int TemperatureC { get; set; }

        /// <summary>
        /// The temperature in F
        /// </summary>
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        /// <summary>
        /// The summary
        /// </summary>
        public string Summary { get; set; }
    }
}
