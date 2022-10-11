using EmployeeDetailsServices.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeWebAPI.Controllers
{
    public class EmployeeDepartmentController : Controller
    {
        private readonly IEmployeeDepartmentService employeeDepartmentService;
        private readonly ILogger<EmployeeDepartmentController> logger;
        public EmployeeDepartmentController(IEmployeeDepartmentService employeeDepartmentService, ILogger<EmployeeDepartmentController> logger)
        {
            this.employeeDepartmentService = employeeDepartmentService;
            this.logger = logger;
        }

        [HttpGet]
        [Route("GetEmployeeByDepartment/{id:int}")]
        public async Task<IActionResult> GetEmployeeByDepartment([FromRoute] int id)
        {
            try
            {
                var employees = await employeeDepartmentService.GetEmployeeByDepartment(id);
                if (employees.Count > 0)
                {
                    return Ok(employees.ToList());
                }
            }
            catch (Exception ex)
            {
                logger.LogError("Error Logged", ex.Message);
                return BadRequest(ex.Message);
            }
            return Ok("No employees found int the departmemnt");
        }
        [HttpGet]
        [Route("GetEmployeeGroupDepartment")]
        public IActionResult GetEmployeeGroupDepartment()
        {
            try
            {
                var departmentEmployees = employeeDepartmentService.GetEmployeeGroupDepartment();
                if (departmentEmployees.Count > 0)
                { return Ok(departmentEmployees.ToList()); }
                else
                    return Ok("No employees found");
            }
            catch (Exception ex) { logger.LogError("Error Logged", ex.Message); return BadRequest(ex.Message); }
        }
    }
}
