using EmployeeWebAPI.Controllers;
using EmployeeWebAPI.Data;
using EmployeeWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeWebAPI.Services
{
    public class EmployeeService: IEmployeeService
    {
        private readonly EmployeeDBContext dbContext;
        public EmployeeService(EmployeeDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Employee>> GetEmployees()
        {
            var employees = await dbContext.Employees.ToListAsync();
            return employees;
        }

        public async Task<Employee> AddEmployee(Employee employee)
        {
            await dbContext.Employees.AddAsync(employee);
            await dbContext.SaveChangesAsync();
            return employee;
        }
        public async Task<List<Department>> GetDepartments()
        {
            var departments = await dbContext.Departments.ToListAsync();
            return departments;
        }
        public async Task<Department> AddDepartment(Department department)
        {
            await dbContext.Departments.AddAsync(department);
            await dbContext.SaveChangesAsync();
            return department;
        }
        public async Task<Department> UpdateDepartment( int id, UpdateDepartmentRequest updatedepartment)
        {
            var department = dbContext.Departments.Find(id);
            if (department != null)
            {

                department.DepartmentId = id;
                department.DepartmentName = updatedepartment.DepartmentName;
                await dbContext.SaveChangesAsync();
                return department;
            }
            return null;
        }
        public async Task<Employee> UpdateEmployee( int id, UpdateEmployeeRequest updateemployee)
        {
            var employee = dbContext.Employees.Find(id);
            if (employee != null)
            {

                employee.EmployeeId = id;
                employee.EmployeeName = updateemployee.EmployeeName;
                await dbContext.SaveChangesAsync();
                return employee;
            }
            return null;
        }
        public async Task<Employee> GetEmployeeById( int id)
        {
            var employee = await dbContext.Employees.FindAsync(id);
            if (employee != null)
            {
                return employee;
            }
            return null;
        }
        public async Task<Department> GetDepartmentById( int id)
        {
            var department = await dbContext.Departments.FindAsync(id);
            if (department != null)
            {
                return department;
            }
            return null;
        }
        public async Task<Department> DeleteDepartment( int id)
        {
            var department = await dbContext.Departments.FindAsync(id);
            if (department != null)
            {
                dbContext.Departments.Remove(department);
                await dbContext.SaveChangesAsync();
                return department;
            }
            return null;
        }
        public async Task<Employee> DeleteEmployee( int id)
        {
            var employee = await dbContext.Employees.FindAsync(id);
            if (employee != null)
            {
                dbContext.Employees.Remove(employee);
                await dbContext.SaveChangesAsync();
                return employee;
            }
            return null;

        }
        public async Task<List<EmployeeDetails>> GetEmployeeByDepartment(int id)
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

                return result.ToList();
            }
            return new List<EmployeeDetails>();
        }
        public List<DepartmentEmployees> GetEmployeeGroupDepartment()
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

            return departmentEmployees;

        }
    }

}
