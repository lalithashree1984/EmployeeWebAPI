﻿using System.ComponentModel.DataAnnotations;

namespace EmployeeDetailsModels.Models
{
    public class DepartmentDto
    {
        /// <summary>
        /// DepartmentId
        /// </summary>
        [Required]
        public int DepartmentId { get; set; }

        /// <summary>
        /// Department Name
        /// </summary>
        [Required]
        [StringLength(20, ErrorMessage = "Name is too Big")]
        public string DepartmentName { get; set; }
    }
}
