using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace WeatherFrontend.Services
{
    public class FavoriteCityService
    {
        private readonly HttpClient _httpClient;

        public FavoriteCityService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<FavoriteCity>> GetFavoriteCities(string userId)
        {
            return await _httpClient.GetFromJsonAsync<List<FavoriteCity>>($"api/favoritecities/{userId}");
        }

        public async Task<FavoriteCity> AddFavoriteCity(FavoriteCity city)
        {
            var response = await _httpClient.PostAsJsonAsync("api/favoritecities", city);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<FavoriteCity>();
        }

        // For removing by ID (used in the Favorites page)
        public async Task RemoveFavoriteCity(string cityId)
        {
            var response = await _httpClient.DeleteAsync($"api/favoritecities/{cityId}");
            response.EnsureSuccessStatusCode();
        }

        // For removing by userId and cityName (used in the Dashboard page)
        public async Task RemoveFavoriteCityByName(string userId, string cityName)
        {
            var response = await _httpClient.DeleteAsync($"api/favoritecities/byname/{userId}/{cityName}");
            response.EnsureSuccessStatusCode();
        }
    }


}