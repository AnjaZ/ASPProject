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
    public class GetUsersFromRoreIdQuery : EfUseCase, IGetUsersFromRoreIdQuery
    {
        public GetUsersFromRoreIdQuery(TBRContext context) : base(context)
        {
        }

        public int Id => 7;

        public string Name => "Find users for specified role";

        public string Description => "Query for listing users for specified role";

        public GetUsersFromRoreIdQueryDto Execute(int id)
        {
            var query = Context.Roles.Find(id);

            if (query == null)
            {
                throw new EntryPointNotFoundException(nameof(Role));
            }

            return new GetUsersFromRoreIdQueryDto
            {
                Name = query.Name,
                Users = query.Users.Where(x => x.RoleId == query.Id).Select(x => x.FirstName)
            };
        }
    }
}
