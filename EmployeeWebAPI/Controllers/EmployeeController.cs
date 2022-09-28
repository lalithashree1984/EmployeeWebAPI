using EmployeeWebAPI.Data;
using EmployeeWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace EmployeeWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {

        private readonly EmployeeDBContext dbContext;
        private readonly ILogger<EmployeeController> logger;
        public EmployeeController(EmployeeDBContext dbContext, ILogger<EmployeeController> logger)
        {
            this.dbContext = dbContext;
            this.logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            logger.LogInformation("This is GetEmployees Action Logging", DateTime.UtcNow);
            logger.LogWarning("This is GetEmployees Action Warning", DateTime.UtcNow);
            return Ok(await dbContext.Employees.ToListAsync());
        }
        [Route("AddEmployee")]
        [HttpPost]
        public async Task<IActionResult> AddEmployee(Employee employee)
        {
            logger.LogInformation("This is AddEmployee Action Logging" + employee.EmployeeName);
            logger.LogWarning("This is GetEmployees Action Warning", DateTime.UtcNow);
            await dbContext.Employees.AddAsync(employee);
            await dbContext.SaveChangesAsync();

            return Ok(employee);
        }

        [Route("GetDepartments")]
        [HttpGet]
        public async Task<IActionResult> GetDepartments()
        {
            return Ok(await dbContext.Departments.ToListAsync());
        }

        [Route("AddDepartment")]
        [HttpPost]
        public async Task<IActionResult> AddDepartment(Department department)
        {
            await dbContext.Departments.AddAsync(department);
            await dbContext.SaveChangesAsync();
            return Ok(department);
        }

        [HttpPut]
        [Route("UpdateDepartment/{id:int}")]
        public async Task<IActionResult> UpdateDepartment([FromRoute] int id, UpdateDepartmentRequest updatedepartment)
        {
            var department = dbContext.Departments.Find(id);
            if (department != null)
            {

                department.DepartmentId = id;
                department.DepartmentName = updatedepartment.DepartmentName;
                await dbContext.SaveChangesAsync();
                return Ok(department);
            }
            return NotFound();
        }


        [HttpPut]
        [Route("{employeeId:int}")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] int employeeId, UpdateEmployeeRequest updateEmployee)
        {
            var employee = dbContext.Employees.Find(employeeId);
            if (employee != null)
            {
                employee.DepartmentId = employeeId;
                employee.EmployeeName = updateEmployee.EmployeeName;
                await dbContext.SaveChangesAsync();

                return Ok(employee);
            }
            return NotFound();
        }

        [HttpGet]
        [Route("GetEmployeeById/{id:int}")]
        public async Task<IActionResult> GetEmployeeById([FromRoute] int id)
        {
            var employee = await dbContext.Employees.FindAsync(id);
            if (employee != null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpGet]
        [Route("GetDepartmentById/{id:int}")]
        public async Task<IActionResult> GetDepartmentById([FromRoute] int id)
        {
            var department = await dbContext.Departments.FindAsync(id);
            if (department != null)
            {
                return NotFound();
            }
            return Ok(department);
        }

        [HttpDelete]
        [Route("DeleteDepartment/{id:int}")]
        public async Task<IActionResult> DeleteDepartment([FromRoute] int id)
        {
            var department = await dbContext.Departments.FindAsync(id);
            if (department != null)
            {
                dbContext.Departments.Remove(department);
                await dbContext.SaveChangesAsync();
                return Ok(department);
            }
            return NotFound();

        }
        [HttpDelete]
        [Route("DeleteEmployee/{id:int}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] int id)
        {
            var employee = await dbContext.Employees.FindAsync(id);
            if (employee != null)
            {
                dbContext.Employees.Remove(employee);
                await dbContext.SaveChangesAsync();
                return Ok(employee);
            }
            return NotFound();

        }
        [HttpGet]
        [Route("GetEmployeeByDepartment/{id:int}")]
        public async Task<IActionResult> GetEmployeeByDepartment([FromRoute] int id)
        {
            var employee = await dbContext.Departments.FindAsync(id);
            if (employee != null)
            {
                var result = from e in dbContext.Employees
                             join d in dbContext.Departments on e.DepartmentId equals d.DepartmentId into ds
                             from d in ds.DefaultIfEmpty()
                             where e.DepartmentId == id
                             select new EmployeeDetails
                             {
                                 EmployeeId = e.EmployeeId,
                                 EmployeeName = e.EmployeeName,
                                 DepartmentName = d.DepartmentName
                             };

                return Ok(result.ToList());
            }
            return NotFound();
        }

        [HttpGet]
        [Route("GetEmployeeGroupDepartment")]
        public IActionResult GetEmployeeGroupDepartment()
        {

            List<DepartmentEmployees> departmentEmployees = new List<DepartmentEmployees>();
            var result = from dept in dbContext.Departments as IEnumerable<Department>
                         join emp in dbContext.Employees as IEnumerable<Employee>
                         on dept.DepartmentId equals emp.DepartmentId into empGroup
                         select new { dept, empGroup };
            foreach (var item in result.ToList())
            {
                //Inner Foreach loop for each employee of a department
                DepartmentEmployees departmentEmployee = new DepartmentEmployees();
                departmentEmployee.DepartmentName = item.dept.DepartmentName;
                foreach (var employee in item.empGroup)
                {
                    Employee emp = new Employee();
                    emp.EmployeeId = employee.EmployeeId;
                    emp.EmployeeName = employee.EmployeeName;
                    emp.DepartmentId = employee.DepartmentId;
                    departmentEmployee.Employees.Add(emp);
                }
                departmentEmployees.Add(departmentEmployee);
            }

            return Ok(departmentEmployees);

        }
    }
}
