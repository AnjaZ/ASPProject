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
    public class CreateBookValidator : AbstractValidator<PostBookDto>
    {
        public CreateBookValidator(TBRContext _context)
        {
            var titleRegex = @"^[A-Z][a-z]{2,}(\s[a-z]{2,})?$";

            RuleFor(x => x.Title)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Naziv knjige je obavezan podatak.")
                .Matches(titleRegex).WithMessage("Ime knjige nije u ispravnom formatu.");

            RuleFor(x => x.Description).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Opis knjige je obavezan podatak.");

        }
    }
}
