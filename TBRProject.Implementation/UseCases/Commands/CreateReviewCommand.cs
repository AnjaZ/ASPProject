using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBRProject.Application.UseCases.Commands;
using TBRProject.Application.UseCases.DTO;
using TBRProject.DataAccess;
using TBRProject.Domain;
using TBRProject.Implementation.Validators;

namespace TBRProject.Implementation.UseCases.Commands
{
    public class CreateReviewCommand : EfUseCase, ICreateReviewCommand
    {
        private readonly CreateReviewValidator _validator;
        private readonly IApplicationUser _user;

        public CreateReviewCommand(TBRContext context, CreateReviewValidator validator, IApplicationUser user) : base(context)
        {
            _validator = validator;
            _user = user;
        }

        public int Id => 16;

        public string Name => "Creating review";

        public string Description => "Creating review";

        public void Execute(PostReviewDto request)
        {
            _validator.ValidateAndThrow(request);

            var rewiew = new Review
            {
                UserId = _user.Id,
                BookId = request.Book,
                Content = request?.Content,
                Stars = request.Stars
            };

            Context.Reviews.Add(rewiew);
            Context.SaveChanges();
        }
    }
}
