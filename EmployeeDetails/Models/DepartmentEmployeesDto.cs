﻿namespace EmployeeDetails.Models
{
    public class DepartmentEmployeesDto
    {
        public string DepartmentName { get; set; }
        public List<EmployeeDto> Employees { get; set; } = new List<EmployeeDto>();
    }
}
