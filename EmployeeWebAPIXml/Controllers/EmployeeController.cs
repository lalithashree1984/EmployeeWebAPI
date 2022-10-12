using EmployeeDetailsModels.Models;
using EmployeeDetailsServices.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeWebAPIXml.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {

        private readonly IEmployeeService employeeService;
        private readonly ILogger<EmployeeController> logger;
        public EmployeeController(IEmployeeService employeeService, ILogger<EmployeeController> logger)
        {
            this.employeeService = employeeService;
            this.logger = logger;
        }
        [HttpPost]
        public async Task<IActionResult> AddEmployee(EmployeeDto employee)
        {
            try
            {
                if (!ModelState.IsValid) { return BadRequest(ModelState); }
                return Ok(await employeeService.AddEmployee(employee));
            }
            catch (Exception ex)
            {
                logger.LogError("Error Logged", ex.Message);
                return BadRequest(ex.Message);

            }
        }


    }
}
