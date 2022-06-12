using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBRProject.Application.UseCases.DTO;
using TBRProject.Application.UseCases.Queries;
using TBRProject.DataAccess;
using TBRProject.Domain;

namespace TBRProject.Implementation.UseCases.Queries
{
    public class FindBooksFromGenreIdQuery : EfUseCase, IFindBooksFromGenreIdQuery
    {
        public FindBooksFromGenreIdQuery(TBRContext context) : base(context)
        {
        }

        public int Id => 8;

        public string Name => "Find books for specified genre";

        public string Description => "Query for listing books for specified genre";

        public BooksGenresDto Execute(int id)
        {
            var query = Context.BookGenres.Include(x => x.Book).Where(x => x.GenreId == id);

            if (query == null)
            {
                throw new EntryPointNotFoundException(nameof(Genre));
            }
            return new BooksGenresDto
            {
                Books = query.Select(x => new TitleBookDto
                {
                    Title = x.Book.Title,
                    Description = x.Book.Description,
                    Authors = x.Book.Authors.Select(z => z.Author.LastName)
                }).ToList()
            };
        }
    }
}
