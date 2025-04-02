namespace WeatherFrontend.Services
{
    public class FavoriteCity
    {
        public string Id { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string CityName { get; set; } = string.Empty;
        public DateTime AddedDate { get; set; }
    }
}