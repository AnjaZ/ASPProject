using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBRProject.DataAccess;

namespace TBRProject.Implementation.UseCases
{
    public abstract class EfUseCase
    {
        protected EfUseCase(TBRContext context)
        {
            Context = context;
        }

        protected TBRContext Context { get; }
    }
}
