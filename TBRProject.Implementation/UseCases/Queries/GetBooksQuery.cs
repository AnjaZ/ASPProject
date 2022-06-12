using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBRProject.Application.UseCases.DTO;
using TBRProject.Application.UseCases.DTO.Searches;
using TBRProject.Application.UseCases.Queries;
using TBRProject.DataAccess;

namespace TBRProject.Implementation.UseCases.Queries
{
    public class GetBooksQuery : EfUseCase, IGetBooksQuery
    {
        public GetBooksQuery(TBRContext context) : base(context)
        {
        }
        public int Id => 3; 

        public string Name => "Search books";

        public string Description => "Search Users using Entity Framework.";
        public PagedResponse<BookDto> Execute(BasePagedSearch search)
        {
            var query = Context.Books
                .Include(a => a.Authors)
                .ThenInclude(a => a.Author)
                .Include(bg => bg.BookGenre)
                .ThenInclude(g => g.Genre)
                .AsQueryable();

            if (!string.IsNullOrEmpty(search.Keyword))
            {
                //query = query.Where(x => (x.Authors.Author.LastName).ToLower().Contains(search.Keyword.ToLower()));
                query = query.Where(x => x.Title.Contains(search.Keyword));
            }

            if (search.PerPage == null || search.PerPage < 1)
            {
                search.PerPage = 10;
            }

            if (search.Page == null || search.Page < 1)
            {
                search.PerPage = 1;
            }

            var toSkip = (search.Page.Value - 1) * search.PerPage.Value;

            var response = new PagedResponse<BookDto>();
            response.TotalCount = query.Count();
            response.Data = query.Skip(toSkip).Take(search.PerPage.Value).Select(x => new BookDto
            {
                Title = x.Title,
                Description = x.Description,
                Image = x.Image.Path,
                Genres = x.BookGenre.Select(y => y.Genre.Name),
                Authors = x.Authors.Select(z => z.Author.LastName),
                Id = x.Id
            }).ToList();
            response.CurrentPage = search.Page.Value;
            response.ItemsPerPage = search.PerPage.Value;
            //return query.Select(

            return response;
        }
    }
}
