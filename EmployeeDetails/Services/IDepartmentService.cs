using EmployeeWebAPI.Data;
using EmployeeWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeDetails.Services
{
    public interface IDepartmentService
    {
        public Task<List<DepartmentDto>> GetDepartments();

        public Task<DepartmentDto> AddDepartment(DepartmentDto department);
        public Task<DepartmentDto> UpdateDepartment(int id, UpdateDepartmentRequestDto updatedepartment);
        public Task<DepartmentDto> GetDepartmentById(int id);
        public Task<DepartmentDto> DeleteDepartment([FromRoute] int id);
    }
}
