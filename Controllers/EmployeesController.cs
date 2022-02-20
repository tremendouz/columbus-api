using EmployeesApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeesApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IColumbusService _columbusService;
        private readonly ILogger<EmployeesController> _logger;

        public EmployeesController(IColumbusService columbusService, ILogger<EmployeesController> logger)
        {
            _columbusService = columbusService;
            _logger = logger;
        }

        [HttpGet()]
        public async Task<IActionResult> Get(int skip, int take)
        {
            try
            {
                var response = await _columbusService.GetEmployees(skip, take);

                if (!string.IsNullOrEmpty(response.ErrorMsg))
                {
                    _logger.LogError(response.ErrorMsg);
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }

                if (response.Result == null)
                {
                    return NotFound();
                }
                return Ok(response.Result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var response = await _columbusService.GetEmployee(id);

                if (!string.IsNullOrEmpty(response.ErrorMsg))
                {
                    _logger.LogError(response.ErrorMsg);
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }

                if (response.Result == null)
                {
                    return NotFound();
                }
                return Ok(response.Result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
