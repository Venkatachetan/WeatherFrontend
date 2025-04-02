namespace WeatherFrontend.Models
{
    public class WeatherData
    {
        public string City { get; set; } = string.Empty;
        public string Temperature { get; set; } = string.Empty;
        public string Weather { get; set; } = string.Empty;
        public int Humidity { get; set; }
        public double WindSpeed { get; set; }
    }

    public class ForecastData
    {
        public string Date { get; set; } = string.Empty;
        public string Temperature { get; set; } = string.Empty;
        public string Weather { get; set; } = string.Empty;
    }
}