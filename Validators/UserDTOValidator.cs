using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Prueba.DTOs;

namespace Prueba.Validators
{
    public class UserDTOValidator : AbstractValidator<UserDTO>
    {
        public UserDTOValidator()
        {
            RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

            RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .MaximumLength(100).WithMessage("Last name must not exceed 100 characters.");

            RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Address is required.");

            RuleFor(x => x.TellNumber)
            .Matches(@"^\d{7,15}$").WithMessage("Invalid phone number. Must be 7 to 15 digits.");

            RuleFor(x => x.DocumentID)
            .NotEmpty().WithMessage("Document ID is required.");

            RuleFor(x => x.BirthDay)
            .NotEmpty().WithMessage("Birth date is required.")
            .Must(BeAValidDate).WithMessage("Birth date must be a valid date in format yyyy-MM-dd.");
        }
        private bool BeAValidDate(string date)
        {
            return DateTime.TryParseExact(date, "yyyy-MM-dd",
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None, out _);
        }
    }
}