using EmployeeDetailsModels.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeDetailsServices.Services
{
    public interface IEmployeeService
    {
        public Task<List<EmployeeDto>> GetEmployees();

        public Task<EmployeeDto> AddEmployee(EmployeeDto employee);

        public Task<EmployeeDto> UpdateEmployee(int id, UpdateEmployeeRequestDto updateemployee);

        public Task<EmployeeDto> GetEmployeeById(int id);

        public Task<EmployeeDto> DeleteEmployee(int id);
       

    }
}
