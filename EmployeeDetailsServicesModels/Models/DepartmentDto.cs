using System.ComponentModel.DataAnnotations;

namespace EmployeeDetailsModels.Models
{
    public class DepartmentDto
    {
        /// <summary>
        /// DepartmentId
        /// </summary>
        [Required]
        [Range(1, 100, ErrorMessage = "Please enter a valid DepartmentId(1 to 100)")]
        public int DepartmentId { get; set; }

        /// <summary>
        /// Department Name
        /// </summary>
        [Required]
        [StringLength(20, ErrorMessage = "Name is too Big")]
        public string DepartmentName { get; set; }
    }
}
