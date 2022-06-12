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
    public class DeleteUserCommand : EfUseCase, IDeleteUserCommand
    {
        public DeleteUserCommand(TBRContext context) : base(context)
        {
        }

        public int Id => 11;

        public string Name => "Delete user";

        public string Description => "Delete user";

        public void Execute(int request)
        {
            var user = Context.Users
                        .Include(x => x.AuthorBooks)
                        .Include(x => x.UserBooks)
                        .Include(x => x.Likes)
                        .Include(x => x.UseCases)
                        .Include(x => x.Reviews)
                        .FirstOrDefault(x => x.Id == request);

            if (user == null)
            {
                throw new EntityNotFoundException(nameof(User), request);
            }

            if (user.AuthorBooks.Any())
            {
                throw new UseCaseConflictException("Can't delete user because of it's link to book: "
                                                   + string.Join(", ", user.AuthorBooks.Select(x => x.Book.Title)));
            }

            if (user.Reviews.Any())
            {
                var reviewes = Context.Reviews.Where(x => x.UserId == request).Select(x => x.Id).ToList();
                foreach (var review in reviewes)
                {
                    var likes = Context.Likes.Where(x => x.ReviewId == review).Select(x => x.ReviewId).ToList();
                    if (likes.Any())
                    {
                        foreach (var lik in likes)
                        {
                            var likeEntity = Context.Likes.Where(x => x.ReviewId == lik).FirstOrDefault();
                            if (likeEntity == null)
                            {
                                throw new EntityNotFoundException(nameof(Like), lik);
                            }
                            Context.Likes.Remove(likeEntity);
                        }
                    }
                    var reviewEntity = Context.Reviews.Find(review);
                    if (reviewEntity == null)
                    {
                        throw new EntityNotFoundException(nameof(Review), review);
                    }
                    Context.Reviews.Remove(reviewEntity);

                }
            }
            Context.ReadersList.RemoveRange(user.UserBooks);
            Context.Likes.RemoveRange(user.Likes);
            Context.UserUseCases.RemoveRange(user.UseCases);
            Context.Users.Remove(user);

            Context.SaveChanges();
        }
    }
}
