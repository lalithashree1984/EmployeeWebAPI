using System.ComponentModel.DataAnnotations;

namespace EmployeeServices.Data
{
    /// <summary>
    /// Department
    /// </summary>
    public class Department
    {

        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; }
    }
}
