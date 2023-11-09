using API.DTOs.Accounts;
using API.Utilities.Enum;
using FluentValidation;

namespace API.Utilities.Validations.Account
{
    public class RegieterAccountValidation : AbstractValidator<RegisterAccountDto>
    {
        public RegieterAccountValidation() 
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


            RuleFor(a => a.Password)
                   .NotEmpty()
                   .MinimumLength(8)
                   .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
                   .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
                   .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.")
                   .Matches(@"[\!\?\*\.]+").WithMessage("Your password must contain at least one (!? *.).");

            RuleFor(a => a.ConfirmPassword)
                    .NotEmpty()
                    .MinimumLength(8)
                    .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
                    .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
                    .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.")
                    .Matches(@"[\!\?\*\.]+$").WithMessage("Your password must contain at least one (!? *.).");

        }
    }
}
