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
    public class FindBookQuery : EfUseCase, IFindBookQuery
    {
        public FindBookQuery(TBRContext context) : base(context)
        {
        }

        public int Id => 12;

        public string Name => "Find book query";

        public string Description => "Query for finding single book";

        public FindBookDto Execute(int id)
        {
            var query = Context.Books.Find(id);

            if (query == null)
            {
                throw new EntryPointNotFoundException(nameof(Book));
            }

            return new FindBookDto
            {
                Id = query.Id,
                Title = query.Title,
                Description = query.Description,
                Image = query.Image.Path,
                Authors = query.Authors.Where(x => x.BookId == query.Id).Select(x => x.Author.LastName),
                Reviews = query.Reviews.Select(x => new ReviewDto
                {
                    User = x.Users.LastName,
                    Content = x.Content,
                    Stars = x.Stars
                }).ToList()
            };
        }
    }
}
