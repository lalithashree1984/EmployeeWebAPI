using AutoMapper;
using EmployeeDetailsModels.Models;
using EmployeeServices.Data;

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
