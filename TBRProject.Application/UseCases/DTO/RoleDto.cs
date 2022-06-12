using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBRProject.Application.UseCases.DTO
{
    public class RoleDto : BaseDto
    {
        public string Name { get; set; }
    }

    public class GetUsersFromRoreIdQueryDto : BaseDto
    {
        public string Name { get; set; }
        public IEnumerable<string> Users { get; set; }

    }
}
