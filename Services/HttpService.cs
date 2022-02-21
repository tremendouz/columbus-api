using EmployeesApi.Models;
using System.Text.Json;

namespace EmployeesApi.Services
{
    public class HttpService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<HttpService> _logger;

        public HttpService(HttpClient httpClient, ILogger<HttpService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<ApiResponse<T>> HttpGet<T>(string endpoint)
        {
            var apiReponse = new ApiResponse<T>();
            try
            {
                var response = await _httpClient.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                try
                {
                    var result = JsonSerializer.Deserialize<T>(content, options);
                    apiReponse.Result = result;
                }
                catch (Exception ex)
                {
                    apiReponse.ErrorMsg = ex.Message;
                    _logger.LogError(ex.Message);
                }
            }
            catch (HttpRequestException ex)
            {
                apiReponse.ErrorMsg = ex.Message;
                _logger.LogError(ex, ex.Message);
            }

            return apiReponse;
        }
    }
}
