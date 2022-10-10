using Newtonsoft.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace EmployeeDetailsModels.Models
{
    public class UpdateDepartmentRequestDto
    {
        /// <summary>
        /// Department Name
        /// </summary>
        [Required]
        [StringLength(20, ErrorMessage = "Name is too Big")]
        public string DepartmentName { get; set; }
    }
}
