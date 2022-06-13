using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBRProject.Application.UseCases.DTO;
using TBRProject.Application.UseCases.DTO.Searches;

namespace TBRProject.Application.UseCases.Queries
{
    public interface IGetRolesQuery : IQuery<BaseSearch, IEnumerable<RoleDto>>
    {

    }
}
