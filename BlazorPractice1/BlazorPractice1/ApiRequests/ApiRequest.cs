using BlazorPractice1.ApiRequests.Model;
using System.Net.Http.Json;
using System.Text.Json;

namespace BlazorPractice1.ApiRequests
{
    public class ApiRequest
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ApiRequest> _logger;
        public ApiRequest(HttpClient httpClient, ILogger<ApiRequest> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        
        public async Task<UsersListResponse> GetAllUsersAsync()
        {
            var url = "GetAllUsers";
            try
            {
                var response = await _httpClient.GetAsync(url).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                if (string.IsNullOrEmpty(content))
                {
                    _logger.LogWarning("Ответ от сервера пуст.");
                    return new UsersListResponse();
                }

                var userlist = JsonSerializer.Deserialize<UsersListResponse>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                }); 

                return userlist ?? new UsersListResponse();
            }
            catch(Exception ex)
            {
                    _logger.LogError(ex, "Ошибка при запросе");
                    return new UsersListResponse();
            }
        }

        public async Task<StatusRegResponse> RegistrationAsync(AddNewUserRequest request)
        {
            var url = "Registration";

            try
            {
                var response = await _httpClient.PostAsJsonAsync(url, request);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();

                var userAdd =  JsonSerializer.Deserialize<StatusRegResponse>(content);
                
                return userAdd ?? new StatusRegResponse();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Ошибка при запросе: {ex.Message}");
                return new StatusRegResponse();
            }
        }

        public async Task<AuthorizeResponse> AuthorizeResponse(LoginRequest request)
        {
            var url = "Authorize";

            try
            {
                var response = await _httpClient.PostAsJsonAsync(url, request);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();

                var userAdd = JsonSerializer.Deserialize<AuthorizeResponse>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                
                return userAdd ?? new AuthorizeResponse();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Ошибка при запросе: {ex.Message}");
                return new AuthorizeResponse();
            }
        }
        
        public async Task<AuthorizeResponse> UpdateUser(UpdateUserRequest request)
        {
            var url = "UpdateUser";
            var response = await _httpClient.PutAsJsonAsync(url, request);
            try
            {
                var content = await response.Content.ReadAsStringAsync();
                response.EnsureSuccessStatusCode();
                var userUpdate = JsonSerializer.Deserialize<AuthorizeResponse>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return userUpdate ?? new AuthorizeResponse();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Ошибка при запросе: {ex.Message}");
                return new AuthorizeResponse();
            }
        }

        public async Task<StatusRegResponse?> DeleteUserAsync(int id)
        {
            var url = $"/DeleteUsers/?user_id={id}";
            var resp = await _httpClient.DeleteAsync(url);
            if (!resp.IsSuccessStatusCode) return null;
            return await resp.Content.ReadFromJsonAsync<StatusRegResponse>();
        }

        public async Task<StatusRegResponse?> UpdateUserAsync(User user)
        {
            var url = "/UpdateUser";
            var resp = await _httpClient.PutAsJsonAsync(url, user);
            if (!resp.IsSuccessStatusCode) return null;
            return await resp.Content.ReadFromJsonAsync<StatusRegResponse>();
        }
    }
}
