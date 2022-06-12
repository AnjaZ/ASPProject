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
    public class CreateReviewValidator : AbstractValidator<PostReviewDto>
    {
        public CreateReviewValidator()
        {
            RuleFor(x => x.Stars).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Ocena knjige je obavezan podatak.");
        }
    }
}
