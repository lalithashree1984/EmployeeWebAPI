using EmployeeDetailsModels.Models;
using EmployeeServices.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EmployeeDetailsServices.Services
{
    public class EmployeeDepartmentService : IEmployeeDepartmentService
    {
        private readonly EmployeeDBContext dbContext;
        public EmployeeDepartmentService(EmployeeDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<EmployeeDetailsDto>> GetEmployeeByDepartment(int id)
        {
            var employee = await dbContext.Departments.FindAsync(id);
            if (employee != null)
            {
                var result = from e in dbContext.Employees
                             join d in dbContext.Departments on e.DepartmentId equals d.DepartmentId into ds
                             from d in ds.DefaultIfEmpty()
                             where e.DepartmentId == id
                             select new EmployeeDetailsDto
                             {
                                 EmployeeId = e.EmployeeId,
                                 EmployeeName = e.EmployeeName,
                                 DepartmentName = d.DepartmentName
                             };

                return result.ToList();
            }
            return new List<EmployeeDetailsDto>();
        }
        public List<DepartmentEmployeesDto> GetEmployeeGroupDepartment()
        {

            List<DepartmentEmployeesDto> departmentEmployees = new List<DepartmentEmployeesDto>();
            var result = from dept in dbContext.Departments as IEnumerable<Department>
                         join emp in dbContext.Employees as IEnumerable<Employee>
                         on dept.DepartmentId equals emp.DepartmentId into empGroup
                         select new { dept, empGroup };
            foreach (var item in result.ToList())
            {
                //Inner Foreach loop for each employee of a department
                DepartmentEmployeesDto departmentEmployee = new DepartmentEmployeesDto();
                departmentEmployee.DepartmentName = item.dept.DepartmentName;
                foreach (var employee in item.empGroup)
                {
                    EmployeeDto emp = new EmployeeDto();
                    emp.EmployeeId = employee.EmployeeId;
                    emp.EmployeeName = employee.EmployeeName;
                    emp.DepartmentId = employee.DepartmentId;
                    departmentEmployee.Employees.Add(emp);
                }
                departmentEmployees.Add(departmentEmployee);
            }

            return departmentEmployees;

        }
    }
}
