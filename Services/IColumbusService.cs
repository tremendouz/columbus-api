using EmployeesApi.Models;

namespace EmployeesApi.Services
{
    public interface IColumbusService
    {
        Task<ApiResponse<EmployeeDetails>> GetEmployee(int id);
        Task<ApiResponse<List<Employee>>> GetEmployees(int? skip, int? take);
    }
}
