using Microsoft.AspNetCore.Authentication;
using NoticeBoard.Web.Models;
using System.Net.Http.Headers;
using System.Text.Json;

namespace NoticeBoard.Web.Services
{
    public class AnnouncementApiService : IAnnouncementApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AnnouncementApiService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }

        private async Task SetBearerTokenAsync()
        {
            var context = _httpContextAccessor.HttpContext;
            if (context != null)
            {
                var token = await context.GetTokenAsync("id_token");

                if (!string.IsNullOrEmpty(token))
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }
            }
        }

        public async Task<IEnumerable<AnnouncementViewModel>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("api/announcements");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            return JsonSerializer.Deserialize<IEnumerable<AnnouncementViewModel>>(content, options)
                   ?? new List<AnnouncementViewModel>();
        }

        public async Task<AnnouncementViewModel?> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/announcements/{id}");
            if (!response.IsSuccessStatusCode) return null;

            var content = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            return JsonSerializer.Deserialize<AnnouncementViewModel>(content, options);
        }

        public async Task<bool> CreateAsync(AnnouncementCreateViewModel model)
        {
            await SetBearerTokenAsync();
            var response = await _httpClient.PostAsJsonAsync("api/announcements", model);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(int id, AnnouncementUpdateViewModel model)
        {
            await SetBearerTokenAsync();
            var response = await _httpClient.PutAsJsonAsync($"api/announcements/{id}", model);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            await SetBearerTokenAsync();
            var response = await _httpClient.DeleteAsync($"api/announcements/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
