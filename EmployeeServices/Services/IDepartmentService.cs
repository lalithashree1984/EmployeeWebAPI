using EmployeeDetailsModels.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeDetailsServices.Services
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
