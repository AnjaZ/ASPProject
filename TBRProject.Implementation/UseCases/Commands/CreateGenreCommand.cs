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
    public class CreateGenreCommand : EfUseCase, ICreateGenreCommand
    {
        private readonly CreateGenreValidator _validator;
        public CreateGenreCommand(TBRContext context, CreateGenreValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 9;

        public string Name => "Create genre";

        public string Description => "Create new genre";

        public void Execute(GenreDto request)
        {
            _validator.ValidateAndThrow(request);

            var genre = new Genre
            {
                Name = request.Name
            };

            Context.Genres.Add(genre);

            Context.SaveChanges();
        }
    }
}
