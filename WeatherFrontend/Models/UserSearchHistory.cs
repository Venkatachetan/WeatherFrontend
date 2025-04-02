using System.Text.Json.Serialization;

namespace WeatherFrontend.Models
{
    public class UserSearchHistory
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("userId")]
        public string UserId { get; set; } = string.Empty;

        [JsonPropertyName("cityName")]
        public string CityName { get; set; } = string.Empty;

        [JsonPropertyName("weatherCondition")]
        public string WeatherCondition { get; set; } = string.Empty;

        [JsonPropertyName("temperature")]
        public string Temperature { get; set; } = string.Empty;

        [JsonPropertyName("searchDate")]
        public DateTime SearchDate { get; set; } = DateTime.UtcNow;
    }
}