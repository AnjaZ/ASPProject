using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBRProject.Application.UseCases.Commands;
using TBRProject.Application.UseCases.DTO;
using TBRProject.DataAccess;
using TBRProject.Domain;

namespace TBRProject.Implementation.UseCases.Commands
{
    public class CreateLikeCommand : EfUseCase, ICreateLikeCommand
    {
        private readonly IApplicationUser _user;
        public CreateLikeCommand(TBRContext context, IApplicationUser user) : base(context)
        {
            _user = user;
        }

        public int Id => 18;

        public string Name => "Create like";

        public string Description => "Create like for review";

        public void Execute(LikeDto request)
        {
            var like = new Like
            {
                UserId = _user.Id,
                ReviewId = request.ReviewId
            };

            Context.Likes.Add(like);
            Context.SaveChanges();
        }
    }
}
