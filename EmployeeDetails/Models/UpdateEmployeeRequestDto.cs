using System.ComponentModel.DataAnnotations;

namespace EmployeeDetails.Models
{
    public class UpdateEmployeeRequestDto
    {
        /// <summary>
        /// Employee Name
        /// </summary>
        [Required]
        [StringLength(20, ErrorMessage = "Name is too Big")]
        public string EmployeeName { get; set; }
        /// <summary>
        /// Department Id
        /// </summary>
        public int DepartmentId { get; set; }
    }
}
