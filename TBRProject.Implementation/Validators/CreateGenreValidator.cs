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
    public class CreateGenreValidator : AbstractValidator<GenreDto>
    {
        public CreateGenreValidator(TBRContext _context)
        {
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Ime zanra je obavezan podatak.")
                .Must(x => !_context.Genres.Any(u => u.Name == x)).WithMessage("Ime zanra - {PropertyValue} je već u upotrebi.");
        }
    }
}
