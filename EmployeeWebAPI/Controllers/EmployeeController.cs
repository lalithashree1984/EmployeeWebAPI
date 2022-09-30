using EmployeeWebAPI.Data;
using EmployeeWebAPI.Models;
using EmployeeWebAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace EmployeeWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {

        private readonly IEmployeeService employeeService;
        private readonly ILogger<EmployeeController> logger;
        public EmployeeController(IEmployeeService employeeService, ILogger<EmployeeController> logger)
        {
            this.employeeService = employeeService;
            this.logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            try
            {
                return Ok(await employeeService.GetEmployees());
            }
            catch (Exception ex)
            {
                logger.LogError("Error Logged", ex.Message);
                return BadRequest();
            }
        }
        [Route("AddEmployee")]
        [HttpPost]
        public async Task<IActionResult> AddEmployee(Employee employee)
        {
            try
            {
                return Ok(await employeeService.AddEmployee(employee));
            }
            catch (Exception ex)
            {
                logger.LogError("Error Logged", ex.Message);
                return BadRequest(ex.Message);

            }
        }

        [Route("GetDepartments")]
        [HttpGet]
        public async Task<IActionResult> GetDepartments()
        {
            try
            {
                return Ok(await employeeService.GetDepartments());
            }
            catch (Exception ex) { logger.LogError("Error Logged", ex.Message); return BadRequest(ex.Message); }
        }

        [Route("AddDepartment")]
        [HttpPost]
        public async Task<IActionResult> AddDepartment(Department department)
        {
            try
            {
                return Ok(await employeeService.AddDepartment(department));
            }
            catch (Exception ex) { logger.LogError("Error Logged", ex.Message); return BadRequest(ex.Message); }

        }

        [HttpPut]
        [Route("UpdateDepartment/{id:int}")]
        public async Task<IActionResult> UpdateDepartment([FromRoute] int id, UpdateDepartmentRequest updatedepartment)
        {
            try
            {
                var department = await employeeService.UpdateDepartment(id, updatedepartment);
                if (department != null)
                {
                    return Ok(department);
                }
            }
            catch (Exception ex) { logger.LogError("Error Logged", ex.Message); return BadRequest(ex.Message); }
            return NotFound();
        }


        [HttpPut]
        [Route("{employeeId:int}")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] int employeeId, UpdateEmployeeRequest updateEmployee)
        {
            try
            {
                var employee = await employeeService.UpdateEmployee(employeeId, updateEmployee);
                if (employee != null)
                {
                    return Ok(employee);
                }
            }
            catch (Exception ex) { logger.LogError("Error Logged", ex.Message); return BadRequest(ex.Message); }
            return NotFound();
        }

        [HttpGet]
        [Route("GetEmployeeById/{id:int}")]
        public async Task<IActionResult> GetEmployeeById([FromRoute] int id)
        {
            try
            {
                var employee = await employeeService.GetEmployeeById(id);
                if (employee != null)
                {
                    return Ok(employee);

                }
            }
            catch (Exception ex) { logger.LogError("Error Logged", ex.Message); return BadRequest(ex.Message); }
            return NotFound();
        }

        [HttpGet]
        [Route("GetDepartmentById/{id:int}")]
        public async Task<IActionResult> GetDepartmentById([FromRoute] int id)
        {
            try
            {
                var department = await employeeService.GetDepartmentById(id);
                if (department != null)
                {
                    return Ok(department);
                }
            }
            catch (Exception ex) { logger.LogError("Error Logged", ex.Message); return BadRequest(ex.Message); }
            return NotFound();
        }

        [HttpDelete]
        [Route("DeleteDepartment/{id:int}")]
        public async Task<IActionResult> DeleteDepartment([FromRoute] int id)
        {
            try
            {
                var department = await employeeService.DeleteDepartment(id);
                if (department != null)
                {
                    return Ok(department);
                }
            }
            catch (Exception ex) { logger.LogError("Error Logged", ex.Message); return BadRequest(ex.Message); }
            return NotFound();

        }
        [HttpDelete]
        [Route("DeleteEmployee/{id:int}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] int id)
        {
            try
            {
                var employee = await employeeService.DeleteEmployee(id);
                if (employee != null)
                {
                    return Ok(employee);
                }
            }
            catch (Exception ex) { logger.LogError("Error Logged", ex.Message); return BadRequest(ex.Message); }
            return NotFound();

        }
        [HttpGet]
        [Route("GetEmployeeByDepartment/{id:int}")]
        public async Task<IActionResult> GetEmployeeByDepartment([FromRoute] int id)
        {
            try
            {
                var employees = await employeeService.GetEmployeeByDepartment(id);
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
                var departmentEmployees = employeeService.GetEmployeeGroupDepartment();
                if (departmentEmployees.Count > 0)
                { return Ok(departmentEmployees.ToList()); }
                else
                    return NotFound();
            }
            catch (Exception ex) { logger.LogError("Error Logged", ex.Message); return BadRequest(ex.Message); }
        }

    }
}
