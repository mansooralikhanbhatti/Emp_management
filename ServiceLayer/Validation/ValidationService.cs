using Emp_management.DataLayer.DTOs;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Emp_management.ServiceLayer.Validation
{
    public class ValidationService
    {
        public static void ValidateEmployeeCreateDto(EmployeeCreateDto dto, ModelStateDictionary modelState)
        {
            if (dto == null)
            {
                modelState.AddModelError("dto", "Employee data is required");
                return;
            }

            // Validate Name
            if (dto.Name == "string" || string.IsNullOrWhiteSpace(dto.Name))
            {
                modelState.AddModelError("name", "Please provide a valid name");
            }

            // Validate Age
            if (dto.Age <= 0 || dto.Age == 100)
            {
                modelState.AddModelError("age", "Please provide a valid age");
            }

            // Validate Email
            if (dto.Email.Equals("user@example.com", StringComparison.OrdinalIgnoreCase) || string.IsNullOrWhiteSpace(dto.Email))
            {
                modelState.AddModelError("email", "Please provide a valid email address");
            }
        }
    }
}
