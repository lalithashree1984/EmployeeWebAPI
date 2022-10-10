using AutoMapper;
using EmployeeDetailsModels.Models;
using EmployeeServices.Data;
using Microsoft.EntityFrameworkCore;


namespace EmployeeDetailsServices.Services
{
    public class DepartmentService:IDepartmentService
    {
        private readonly EmployeeDBContext dbContext;
        private readonly IMapper mapper;
        public DepartmentService(EmployeeDBContext dbContext,IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }
        public async Task<List<DepartmentDto>> GetDepartments()
        {
            // var departments = await dbContext.Departments.ToListAsync();
            var departments = mapper.Map<List<Department>, List<DepartmentDto>>(await dbContext.Departments.ToListAsync());
            return departments;
        }
        public async Task<DepartmentDto> AddDepartment(DepartmentDto department)
        {
            await dbContext.Departments.AddAsync(mapper.Map<DepartmentDto, Department>(department));
            await dbContext.SaveChangesAsync();
            return department;
        }
        public async Task<DepartmentDto> UpdateDepartment(int id, UpdateDepartmentRequestDto updatedepartment)
        {
            var department = dbContext.Departments.Find(id);
            if (department != null)
            {

                department.DepartmentId = id;
                department.DepartmentName = updatedepartment.DepartmentName;
                await dbContext.SaveChangesAsync();
                return mapper.Map<Department, DepartmentDto>(department);
            }
            return null;
        }
        public async Task<DepartmentDto> GetDepartmentById(int id)
        {
            var department = await dbContext.Departments.FindAsync(id);
            if (department != null)
            {
                return mapper.Map<Department, DepartmentDto>(department);
            }
            return null;
        }
        public async Task<DepartmentDto> DeleteDepartment(int id)
        {
            var department = await dbContext.Departments.FindAsync(id);
            if (department != null)
            {
                dbContext.Departments.Remove(department);
                await dbContext.SaveChangesAsync();
                return mapper.Map<Department, DepartmentDto>(department);
            }
            return null;
        }
    }
}
