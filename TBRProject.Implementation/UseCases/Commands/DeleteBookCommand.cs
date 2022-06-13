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
    public class DeleteBookCommand : EfUseCase, IDeleteBookCommand
    {
        public DeleteBookCommand(TBRContext context) : base(context)
        {
        }

        public int Id => 14;

        public string Name => "Delete book";

        public string Description => "Delete book";

        public void Execute(int request)
        {
            var book = Context.Books
                        .Include(x => x.Authors)
                        .Include(x => x.Users)
                        .Include(x => x.Reviews)
                        .Include(x => x.BookGenre)
                        .FirstOrDefault(x => x.Id == request);

            if (book == null)
            {
                throw new EntityNotFoundException(nameof(Book), request);
            }

            if (book.Authors.Any())
            {
                throw new UseCaseConflictException("Can't delete book because of it's link to book: "
                                                   + string.Join(", ", book.Authors.Select(x => x.Author.LastName)));
            }

            if (book.Reviews.Any())
            {
                var reviewes = Context.Reviews.Where(x => x.BookId == request).Select(x => x.Id).ToList();
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
            Context.ReadersList.RemoveRange(book.Users);
            Context.BookGenres.RemoveRange(book.BookGenre);
            Context.Books.Remove(book);

            Context.SaveChanges();
        }
    }
}
