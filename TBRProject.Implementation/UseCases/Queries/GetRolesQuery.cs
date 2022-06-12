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
    public class GetRolesQuery : EfUseCase, IGetRolesQuery
    {
        public GetRolesQuery(TBRContext context) : base(context)
        {
        }

        public int Id => 1;

        public string Name => "Search Roles";

        public string Description => "Search Roles using Entity Framework.";

        public IEnumerable<RoleDto> Execute(BaseSearch search)
        {
            var query = Context.Roles.AsQueryable();

            if (!string.IsNullOrEmpty(search.Keyword))
            {
                query = query.Where(x => x.Name.Contains(search.Keyword));
            }

            return query.Select(x => new RoleDto
            {
                Name = x.Name,
                Id = x.Id
            }).ToList();
        }
    }
}
