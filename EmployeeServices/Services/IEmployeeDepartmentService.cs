
using EmployeeDetailsModels.Models;

namespace EmployeeDetailsServices.Services
{
    public interface IEmployeeDepartmentService
    {
        public Task<List<EmployeeDetailsDto>> GetEmployeeByDepartment(int id);

        public List<DepartmentEmployeesDto> GetEmployeeGroupDepartment();
    }
}
