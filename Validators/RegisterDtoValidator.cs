using EmployeeLeaveManagement.API.DTOs;
using FluentValidation;

namespace EmployeeLeaveManagement.API.Validators
{
    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty()
                .MinimumLength(4)
                .MaximumLength(50);

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(8);

            RuleFor(x => x.FullName)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Role)
                .NotEmpty()
                .Must(role => role == "Admin" ||
                              role == "Manager" ||
                              role == "Employee")
                .WithMessage("Role must be Admin, Manager or Employee.");
        }
    }
}