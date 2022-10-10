using EmployeeDetailsModels.Models;
using EmployeeDetailsServices.Services;
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
        public async Task<IActionResult> AddEmployee(EmployeeDto employee)
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

       

       

       


        [HttpPut]
        [Route("{employeeId:int}")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] int employeeId, UpdateEmployeeRequestDto updateEmployee)
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
       

       

    }
}
