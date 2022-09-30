using EmployeeWebAPI.Data;
using EmployeeWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeWebAPI.Services
{
    public interface IEmployeeService
    {
        public Task<List<Employee>> GetEmployees();


        public Task<Employee> AddEmployee(Employee employee);

        public Task<List<Department>> GetDepartments();

        public  Task<Department> AddDepartment(Department department);
        public Task<Department> UpdateDepartment(int id, UpdateDepartmentRequest updatedepartment);


        public Task<Employee> UpdateEmployee(int id, UpdateEmployeeRequest updateemployee);
        public  Task<Employee> GetEmployeeById( int id);

        public  Task<Department> GetDepartmentById(int id);
        public Task<Department> DeleteDepartment([FromRoute] int id);
        public Task<Employee> DeleteEmployee(int id);
        public Task<List<EmployeeDetails>> GetEmployeeByDepartment(int id);

        public List<DepartmentEmployees> GetEmployeeGroupDepartment();
    
}
}
