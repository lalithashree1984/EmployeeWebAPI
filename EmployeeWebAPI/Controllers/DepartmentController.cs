using EmployeeDetailsModels.Models;
using EmployeeDetailsServices.Services;
using Microsoft.AspNetCore.Mvc;

namespace DepartmentWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService departmentService;
        private readonly ILogger<DepartmentController> logger;
        public DepartmentController(IDepartmentService DepartmentService, ILogger<DepartmentController> logger)
        {
            this.departmentService = DepartmentService;
            this.logger = logger;
        }
        [Route("GetDepartments")]
        [HttpGet]
        public async Task<IActionResult> GetDepartments()
        {
            try
            {
                return Ok(await departmentService.GetDepartments());
            }
            catch (Exception ex) { logger.LogError("Error Logged", ex.Message); return BadRequest(ex.Message); }
        }
        [Route("AddDepartment")]
        [HttpPost]
        public async Task<IActionResult> AddDepartment(DepartmentDto department)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                return Ok(await departmentService.AddDepartment(department));
            }
            catch (Exception ex) { logger.LogError("Error Logged", ex.Message); return BadRequest(ex.Message); }

        }
        [HttpPut]
        [Route("UpdateDepartment")]
        public async Task<IActionResult> UpdateDepartment(UpdateDepartmentRequestDto updatedepartment)
        {
            try
            {
                if (!ModelState.IsValid)
                { 
                    return BadRequest(ModelState); 
                }
                var department = await departmentService.UpdateDepartment(updatedepartment.DepartmentId, updatedepartment);
                if (department != null)
                {
                    return Ok(department);
                }
                else
                {
                    return Ok("Department not found");
                }
            }
            catch (Exception ex) { logger.LogError("Error Logged", ex.Message); return BadRequest(ex.Message); }
        }

        [HttpGet]
        [Route("GetDepartmentById/{id:int}")]
        public async Task<IActionResult> GetDepartmentById([FromRoute] int id)
        {
            try
            {
                if (id > 100 || id < 0)
                {

                    ModelState.AddModelError("DepartmentId", "Department Id is required and should be with 1 to 100.");
                    return BadRequest(ModelState);
                }
                var department = await departmentService.GetDepartmentById(id);
                if (department != null)
                {
                    return Ok(department);
                }
                else
                {

                    return Ok("Department not found");
                }
            }
            catch (Exception ex) { logger.LogError("Error Logged", ex.Message); return BadRequest(ex.Message); }
        }
        [HttpDelete]
        [Route("DeleteDepartment/{id:int}")]
        public async Task<IActionResult> DeleteDepartment([FromRoute] int id)
        {
            try
            {
                if (id > 100 || id < 0)
                {

                    ModelState.AddModelError("DepartmentId", "Department Id is required and should be with 1 to 100.");
                    return BadRequest(ModelState);
                }
                var department = await departmentService.DeleteDepartment(id);
                if (department != null)
                {
                    return Ok(department);
                }
                {

                    return Ok("Department not found");
                }
            }
            catch (Exception ex) { logger.LogError("Error Logged", ex.Message); return BadRequest(ex.Message); }
        }
    }
}
