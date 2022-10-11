using EmployeeDetailsModels.Models;
using EmployeeDetailsServices.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace EmployeeWebAPI.Controllers
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
                if (!ModelState.IsValid) { return BadRequest(ModelState); }
                return Ok(await employeeService.AddEmployee(employee));
            }
            catch (Exception ex)
            {
                logger.LogError("Error Logged", ex.Message);
                return BadRequest(ex.Message);

            }
        }


        [HttpPut]
        [Route("UpdateEmployee")]
        public async Task<IActionResult> UpdateEmployee(UpdateEmployeeRequestDto updateEmployee)
        {
            try
            {
                if (!ModelState.IsValid) { return BadRequest(ModelState); }
                var employee = await employeeService.UpdateEmployee(updateEmployee.EmployeeId, updateEmployee);
                if (employee != null)
                {
                    return Ok(employee);
                }
                else
                {
                    return Ok("Employee not found");

                }
            }
            catch (Exception ex) { logger.LogError("Error Logged", ex.Message); return BadRequest(ex.Message); }
        }

        [HttpGet]
        [Route("GetEmployeeById/{id:int}")]
        public async Task<IActionResult> GetEmployeeById([FromRoute] int id)
        {
            try
            {
                if (id > 100 || id < 0)
                {

                    ModelState.AddModelError("EmployeeId", "Employee Id is required and should be with 1 to 100.");
                    return BadRequest(ModelState);
                }
                var employee = await employeeService.GetEmployeeById(id);
                if (employee != null)
                {
                    return Ok(employee);

                }
                else
                {
                    return Ok("Employee not found");

                }
            }
            catch (Exception ex) { logger.LogError("Error Logged", ex.Message); return BadRequest(ex.Message); }
           
        }

       

       
        [HttpDelete]
        [Route("DeleteEmployee/{id:int}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] int id)
        {
            try
            {
                if (id > 100 || id < 0)
                {

                    ModelState.AddModelError("EmployeeId", "Employee Id is required and should be with 1 to 100.");
                    return BadRequest(ModelState);
                }
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
