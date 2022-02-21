using EmployeesApi.Models;
using Microsoft.AspNetCore.WebUtilities;

namespace EmployeesApi.Services
{
    public class ColumbusService: HttpService, IColumbusService
    {
        private readonly IConfiguration _configuration;
        private readonly string _employeesEndpoint;

        public ColumbusService(HttpClient httpClient, ILogger<HttpService> logger, IConfiguration configuration) : base(httpClient, logger)
        {
            _configuration = configuration;
            _employeesEndpoint = _configuration["EmployeesEndpoint"];
        }

        public async Task<ApiResponse<List<Employee>>> GetEmployees(int? skip, int? take)
        {
            var queryParams = new Dictionary<string, string>() { };

            if (skip.HasValue)
            {
                queryParams.Add("skip", skip.ToString());
            }

            if (take.HasValue)
            {
                queryParams.Add("take", take.ToString());
            }

            var url = QueryHelpers.AddQueryString(_employeesEndpoint, queryParams);
            var response = await HttpGet<List<Employee>>(url);
            return response;
        }

        public async Task<ApiResponse<EmployeeDetails>> GetEmployee(int id)
        {
            var endpoint = $"{_employeesEndpoint}/{id}";
            var response = await HttpGet<EmployeeDetails>(endpoint);
            return response;
        }
    }
}
