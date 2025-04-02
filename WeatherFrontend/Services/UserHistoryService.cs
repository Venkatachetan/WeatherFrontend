using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Blazored.LocalStorage;
using WeatherFrontend.Models;

namespace WeatherFrontend.Services
{
    public class UserHistoryService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthService _authService;

        public UserHistoryService(HttpClient httpClient, ILocalStorageService localStorage, AuthService authService)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _localStorage = localStorage ?? throw new ArgumentNullException(nameof(localStorage));
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
        }

        public async Task<List<UserSearchHistory>> GetUserHistoryAsync()
        {
            try
            {
                var authCheck = await _authService.CheckAuth();
                if (authCheck == null)
                {
                    return new List<UserSearchHistory>();
                }

                var userId = authCheck.Id;
                var response = await _httpClient.GetAsync($"api/searchhistory/{userId}");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<UserSearchHistory>>() ?? new List<UserSearchHistory>();
                }
                else
                {
                    Console.WriteLine($"Error fetching user history: {response.StatusCode}");
                    return new List<UserSearchHistory>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in GetUserHistoryAsync: {ex.Message}");
                return new List<UserSearchHistory>();
            }
        }

        public async Task<bool> AddSearchHistoryAsync(string cityName, string weatherCondition, string temperature)
        {
            try
            {
                var authCheck = await _authService.CheckAuth();
                if (authCheck == null)
                {
                    return false;
                }

                var history = new UserSearchHistory
                {
                    UserId = authCheck.Id,
                    CityName = cityName,
                    WeatherCondition = weatherCondition,
                    Temperature = temperature,
                    SearchDate = DateTime.UtcNow
                };

                var content = new StringContent(
                    JsonSerializer.Serialize(history),
                    Encoding.UTF8,
                    "application/json");

                var response = await _httpClient.PostAsync("api/searchhistory", content);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in AddSearchHistoryAsync: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteSearchHistoryAsync(string id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/searchhistory/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in DeleteSearchHistoryAsync: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> ClearUserSearchHistoryAsync()
        {
            try
            {
                var authCheck = await _authService.CheckAuth();
                if (authCheck == null)
                {
                    return false;
                }

                var userId = authCheck.Id;
                var response = await _httpClient.DeleteAsync($"api/searchhistory/user/{userId}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in ClearUserSearchHistoryAsync: {ex.Message}");
                return false;
            }
        }
    }
}