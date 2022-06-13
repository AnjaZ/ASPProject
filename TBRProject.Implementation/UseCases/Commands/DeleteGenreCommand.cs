using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBRProject.Application.Exceptions;
using TBRProject.Application.UseCases.Commands;
using TBRProject.DataAccess;
using TBRProject.Domain;

namespace TBRProject.Implementation.UseCases.Commands
{
    public class DeleteGenreCommand : EfUseCase, IDeleteGenreCommand
    {
        public DeleteGenreCommand(TBRContext context) : base(context)
        {
        }

        public int Id => 10;

        public string Name => "Delete genre";

        public string Description => "Delete genre";

        public void Execute(int request)
        {
            var genre = Context.Genres
                        .Include(x => x.BookGenre)
                        .FirstOrDefault(x => x.Id == request);

            if (genre == null)
            {
                throw new EntityNotFoundException(nameof(Genre), request);
            }

            if (genre.BookGenre.Any())
            {
                throw new UseCaseConflictException("Can't delete genre because of it's link to books genre: "
                                                   + string.Join(", ", genre.BookGenre.Select(x => x.Book.Title)));
            }

            Context.BookGenres.RemoveRange(genre.BookGenre);
            Context.Genres.Remove(genre);

            Context.SaveChanges();
        }
    }
}
