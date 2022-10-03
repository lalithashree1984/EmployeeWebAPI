using EmployeeWebAPI.Models;
using EmployeeWebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace DepartmentWebAPI.Controllers
{
    public class DepartmentController : Controller
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
                return Ok(await departmentService.AddDepartment(department));
            }
            catch (Exception ex) { logger.LogError("Error Logged", ex.Message); return BadRequest(ex.Message); }

        }
        [HttpPut]
        [Route("UpdateDepartment/{id:int}")]
        public async Task<IActionResult> UpdateDepartment([FromRoute] int id, UpdateDepartmentRequestDto updatedepartment)
        {
            try
            {
                var department = await departmentService.UpdateDepartment(id, updatedepartment);
                if (department != null)
                {
                    return Ok(department);
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
                var department = await departmentService.GetDepartmentById(id);
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
                var department = await departmentService.DeleteDepartment(id);
                if (department != null)
                {
                    return Ok(department);
                }
            }
            catch (Exception ex) { logger.LogError("Error Logged", ex.Message); return BadRequest(ex.Message); }
            return NotFound();

        }

       

    }
}
