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
    public class GetUsersQuery : EfUseCase, IGetUsersQuery
    {
        public int Id => 2;

        public string Name => "Search Users";

        public string Description => "Search Users using Entity Framework.";

        public GetUsersQuery(TBRContext context) : base(context)
        {

        }
        public PagedResponse<UserDto> Execute(BasePagedSearch search)
        {
            var query = Context.Users.AsQueryable();

            if (!string.IsNullOrEmpty(search.Keyword))
            {
                query = query.Where(x => x.FirstName.Contains(search.Keyword) || x.LastName.Contains(search.Keyword) || x.Email.Contains(search.Keyword));
            }

            if (search.PerPage == null || search.PerPage < 1)
            {
                search.PerPage = 15;
            }

            if (search.Page == null || search.Page < 1)
            {
                search.PerPage = 1;
            }

            var toSkip = (search.Page.Value - 1) * search.PerPage.Value;

            var response = new PagedResponse<UserDto>();
            response.TotalCount = query.Count();
            response.Data = query.Skip(toSkip).Take(search.PerPage.Value).Select(x => new UserDto
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                Password = x.Password,
                Role = x.Role.Name,
                Image = x.Image.Path
            }).ToList();
            response.CurrentPage = search.Page.Value;
            response.ItemsPerPage = search.PerPage.Value;
            return response;

        }

    }
}
