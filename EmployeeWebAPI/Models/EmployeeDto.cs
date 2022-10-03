using System.ComponentModel.DataAnnotations;

namespace EmployeeWebAPI.Models
{
    public class EmployeeDto
    {
        /// <summary>
        /// EmployeeId
        /// </summary>
        [Required]
        public int EmployeeId { get; set; }

        /// <summary>
        /// Department Name
        /// </summary>
        [Required]
        [StringLength(20, ErrorMessage = "Name is too Big")]
        public string EmployeeName { get; set; }
        /// <summary>
        /// EmployeeId
        /// </summary>
        [Required]
        public int DepartmentId { get; set; }
    }
}
