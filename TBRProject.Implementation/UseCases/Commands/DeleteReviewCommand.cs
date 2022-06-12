using Microsoft.EntityFrameworkCore;
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
    public class DeleteReviewCommand : EfUseCase, IDeleteReviewCommand
    {
        private readonly IApplicationUser _user;

        public DeleteReviewCommand(TBRContext context, IApplicationUser user) : base(context)
        {
            _user = user;
        }

        public int Id => 17;

        public string Name => "Delete review";

        public string Description => "Delete review by user that create it";

        public void Execute(int id)
        {
            var review = Context.Reviews
                        .Include(x => x.Likes)
                        .FirstOrDefault(x => x.Id == id);

            if (_user.Id != review.UserId)
            {
                throw new UnauthorizedAccessException();
            }
            if (review == null)
            {
                throw new EntityNotFoundException(nameof(Review), id);
            }

            if (review.Likes.Any())
            {
                var likes = Context.Likes.Where(x => x.ReviewId == id).Select(x => x.ReviewId).ToList();
                if (likes.Any())
                {
                    foreach (var like in likes)
                    {
                        var likeEntity = Context.Likes.Find(like);
                        if (likeEntity == null)
                        {
                            throw new EntityNotFoundException(nameof(Like), id);
                        }
                        Context.Likes.Remove(likeEntity);
                    }
                }
            }
            Context.Reviews.Remove(review);

            Context.SaveChanges();

        }
    }
}
