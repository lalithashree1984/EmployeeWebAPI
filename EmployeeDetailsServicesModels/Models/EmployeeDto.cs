using System.ComponentModel.DataAnnotations;

namespace EmployeeDetailsModels.Models
{
    public class EmployeeDto
    {
        /// <summary>
        /// EmployeeId
        /// </summary>
        [Required]
        [Range(1, 100, ErrorMessage = "Please enter a valid EmployeeId(1 to 100)")]
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
        [Range(1, 100, ErrorMessage = "Please enter a valid Department(1 to 100)")]
        public int DepartmentId { get; set; }
    }
}
