using AutoMapper;
using EmployeeWebAPI.Data;
using EmployeeWebAPI.Models;
using DepartmentDto = EmployeeWebAPI.Models.DepartmentDto;

namespace EmployeeWebAPI.Helpers
{
    public class AutoMappingProfiles:Profile
    {
        public  AutoMappingProfiles()
        {

            CreateMap<EmployeeDto, Employee>().ReverseMap();
            CreateMap<DepartmentDto, Department>().ReverseMap();
        }
    }
}
