using Emp_management.DataLayer.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Emp_management.DataLayer.DTOs
{
    public class EmployeeCreateDto
    {
        [Required(ErrorMessage = "Employee ID is required.")]
        public int EmpId { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Age is required.")]
        [Range(18, 100, ErrorMessage = "Age must be between 18 and 100.")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Date of Joining is required.")]
        [DataType(DataType.Date, ErrorMessage = "Invalid date format.")]
        public DateTime DoJ { get; set; }

        [Required(ErrorMessage = "Employee status is required.")]
        public EmployeeStatus Status { get; set; }
    }
}
