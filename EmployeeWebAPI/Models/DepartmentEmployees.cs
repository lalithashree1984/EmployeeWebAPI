namespace EmployeeWebAPI.Models
{
    public class DepartmentEmployees
    {
        public string DepartmentName { get; set; }
        public List<Employee> Employees { get; set; } = new List<Employee>();
    }
}
