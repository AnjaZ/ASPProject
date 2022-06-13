using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBRProject.Application.UseCases.DTO;
using TBRProject.DataAccess;

namespace TBRProject.Implementation.Validators
{
    public class CreateUserValidator : AbstractValidator<CreateUserDto>
    {
        public CreateUserValidator(TBRContext _context)
        {
            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Email je obavezan podatak.")
                .EmailAddress().WithMessage("Email nije ispravnog formata.")
                .Must(x => !_context.Users.Any(u => u.Email == x)).WithMessage("Email adresa {PropertyValue} je već u upotrebi.");

            var imePrezimeRegex = @"^[A-Z][a-z]{2,}(\s[A-Z][a-z]{2,})?$";
            RuleFor(x => x.FirstName).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Ime je obavezan podatak.")
                .Matches(imePrezimeRegex).WithMessage("Ime nije u ispravnom formatu.");

            RuleFor(x => x.LastName).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Prezime je obavezan podatak.")
                .Matches(imePrezimeRegex).WithMessage("Prezime nije u ispravnom formatu.");

            RuleFor(x => x.Password).NotEmpty().WithMessage("Lozinka je obavezan podatak.")
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{5,}$").WithMessage("Lozinka mora da sadrži minimalno 5 karaktera, jedno veliko, jedno malo slovo, broj i specijalni karakter.");

        }
    }
}
