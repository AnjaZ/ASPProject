using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBRProject.Application.UseCases.DTO;

namespace TBRProject.Application.UseCases.Queries
{
    public interface IFindBookQuery : IQuery<int, FindBookDto>
    {
    }
}
