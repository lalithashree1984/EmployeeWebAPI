using AutoMapper;
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
        private readonly IMapper mapper;
        public EmployeeService(EmployeeDBContext dbContext,IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<List<EmployeeDto>> GetEmployees()
        {
            var employees = await dbContext.Employees.ToListAsync();
            return mapper.Map<List<Employee>, List<EmployeeDto>>(employees); 
        }

        public async Task<EmployeeDto> AddEmployee(EmployeeDto employee)
        {
            await dbContext.Employees.AddAsync(mapper.Map<EmployeeDto, Employee>(employee));
            await dbContext.SaveChangesAsync();
            return employee;
        }
      
        public async Task<EmployeeDto> UpdateEmployee( int id, UpdateEmployeeRequestDto updateemployee)
        {
            var employee = dbContext.Employees.Find(id);
            if (employee != null)
            {
                employee.EmployeeId = id;
                employee.EmployeeName = updateemployee.EmployeeName;
                await dbContext.SaveChangesAsync();
                return mapper.Map<Employee, EmployeeDto>(employee);
            }
            return null;
        }
        public async Task<EmployeeDto> GetEmployeeById( int id)
        {
            var employee = await dbContext.Employees.FindAsync(id);
            if (employee != null)
            {
                return mapper.Map<Employee, EmployeeDto>(employee);
            }
            return null;
        }
      
        public async Task<EmployeeDto> DeleteEmployee(int id)
        {
            var employee = await dbContext.Employees.FindAsync(id);
            if (employee != null)
            {
                dbContext.Employees.Remove(employee);
                await dbContext.SaveChangesAsync();
                return mapper.Map<Employee, EmployeeDto>(employee); ;
            }
            return null;

        }
    }

}
