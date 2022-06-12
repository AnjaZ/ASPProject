using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBRProject.Application.Exceptions;
using TBRProject.Application.UseCases.Commands;
using TBRProject.DataAccess;
using TBRProject.Domain;

namespace TBRProject.Implementation.UseCases.Commands
{
    public class DeleteLikeCommand : EfUseCase, IDeleteLikeCommand
    {
        private readonly IApplicationUser _user;
        public DeleteLikeCommand(TBRContext context, IApplicationUser user) : base(context)
        {
            _user = user;
        }

        public int Id => 19;

        public string Name => "Unlike";

        public string Description => "Unlike review (sending review id)";

        public void Execute(int id)
        {
            try
            {
                var like = Context.Likes.FirstOrDefault(x => x.ReviewId == id && x.UserId == _user.Id);
                if (like == null)
                {
                    throw new EntityNotFoundException(nameof(Like), id);
                }
                Context.Likes.Remove(like);
                Context.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new UnauthorizedAccessException();

            }
        }
    }
}
