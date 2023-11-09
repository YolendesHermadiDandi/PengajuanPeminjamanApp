using API.DTOs.Employees;
using FluentValidation;

namespace API.Utilities.Validations.Employee
{
    public class EmployeeValidation : AbstractValidator<EmployeeDto>
    {

        public EmployeeValidation()
        {
            RuleFor(e => e.FirstName)
                   .NotEmpty()
                   .MaximumLength(100)
                   .Matches(@"^[A-Za-z\s]*$").WithMessage("'{PropertyName}' should only contain letters.");

            RuleFor(e => e.LastName)
               .MaximumLength(100)
               .Matches(@"^[A-Za-z\s]*$").WithMessage("'{PropertyName}' should only contain letters.");

            RuleFor(e => e.BirthDate)
               .NotEmpty()
               .LessThanOrEqualTo(DateTime.Now.AddYears(-18));

            RuleFor(e => e.Gender)
                .NotNull()
                .IsInEnum();

            RuleFor(e => e.HiringDate)
               .NotEmpty();

            RuleFor(e => e.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(100);

            RuleFor(e => e.PhoneNumber)
                .NotEmpty()
                .Length(10, 20);

        }
    }
}
