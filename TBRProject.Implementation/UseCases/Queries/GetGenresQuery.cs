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
    public class GetGenresQuery : EfUseCase, IGetGenresQuery
    {
        public GetGenresQuery(TBRContext context) : base(context)
        {
        }

        public int Id => 5;

        public string Name => "Search genres";

        public string Description => "Search genres using Entity Framework.";

        public IEnumerable<GenreDto> Execute(BaseSearch search)
        {
            var query = Context.Genres.AsQueryable();

            if (!string.IsNullOrEmpty(search.Keyword))
            {
                query = query.Where(x => x.Name.Contains(search.Keyword));
            }

            return query.Select(x => new GenreDto
            {
                Name = x.Name,
                Id = x.Id
            }).ToList();
        }
    }
}
