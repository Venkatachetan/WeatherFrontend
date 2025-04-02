using System.Net.Http.Json;
using System.Text.Json;
using Blazored.LocalStorage;
using WeatherFrontend.Models;

namespace WeatherFrontend.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;

        public AuthService(HttpClient httpClient, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
        }

        public async Task<LoginResponse> Login(string email, string password)
        {
            var response = await _httpClient.PostAsync(
                $"api/auth/login?email={Uri.EscapeDataString(email)}&password={Uri.EscapeDataString(password)}",
                null);

            if (response.IsSuccessStatusCode)
            {
                var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();
                if (loginResponse != null)
                {
                    await _localStorage.SetItemAsync("authToken", loginResponse.AccessToken);
                    return loginResponse;
                }
            }
            return null;
        }

        public async Task<bool> Register(string email, string password)
        {
            var response = await _httpClient.PostAsync(
                $"api/auth/register?email={Uri.EscapeDataString(email)}&password={Uri.EscapeDataString(password)}",
                null);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Logout()
        {
            var response = await _httpClient.PostAsync($"api/auth/logout", null);
            if (response.IsSuccessStatusCode)
            {
                await _localStorage.RemoveItemAsync("authToken");
                return true;
            }
            return false;
        }

        public async Task<AuthCheckResponse> CheckAuth()
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");
            if (string.IsNullOrEmpty(token))
                return null;

            var response = await _httpClient.GetAsync($"api/auth/check?accessToken={Uri.EscapeDataString(token)}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<AuthCheckResponse>();
            }
            return null;
        }

        public async Task<bool> ForgotPassword(string email)
        {
            var url = $"api/auth/forgot-password?email={Uri.EscapeDataString(email)}";
            Console.WriteLine(_httpClient.BaseAddress + url);
            var response = await _httpClient.PostAsync(url, null);

            if (response.IsSuccessStatusCode)
            {
                var messageResponse = await response.Content.ReadFromJsonAsync<MessageResponse>();
                return messageResponse?.Message.Contains("sent") == true;
            }
            return false;
        }

        public async Task<bool> ResetPassword(string accessToken, string newPassword, string confirmPassword)
        {
            var response = await _httpClient.PostAsync(
                $"api/auth/reset-password?accessToken={Uri.EscapeDataString(accessToken)}&newPassword={Uri.EscapeDataString(newPassword)}&confirmPassword={Uri.EscapeDataString(confirmPassword)}",
                null);

            if (response.IsSuccessStatusCode)
            {
                var messageResponse = await response.Content.ReadFromJsonAsync<MessageResponse>();
                return messageResponse?.Message.Contains("successfully") == true;
            }
            return false;
        }
    }
}