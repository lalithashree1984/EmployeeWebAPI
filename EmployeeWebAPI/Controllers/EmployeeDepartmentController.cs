using DepartmentWebAPI.Controllers;
using EmployeeWebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeWebAPI.Controllers
{
    public class EmployeeDepartmentController : Controller
    {
        private readonly IEmployeeDepartmentService employeeDepartmentService;
        private readonly ILogger<DepartmentController> logger;
        public EmployeeDepartmentController(IEmployeeDepartmentService employeeDepartmentService, ILogger<DepartmentController> logger)
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
            return NotFound();
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
                    return NotFound();
            }
            catch (Exception ex) { logger.LogError("Error Logged", ex.Message); return BadRequest(ex.Message); }
        }
    }
}
