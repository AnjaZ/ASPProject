using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBRProject.Application.UseCases.Commands;
using TBRProject.Application.UseCases.DTO;
using TBRProject.DataAccess;
using TBRProject.Domain;
using TBRProject.Implementation.Validators;

namespace TBRProject.Implementation.UseCases.Commands
{
    public class CreateBookCommand : EfUseCase, ICreateBookCommand
    {
        private readonly CreateBookValidator _validator;

        public CreateBookCommand(TBRContext context, CreateBookValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 13;

        public string Name => "Create book";

        public string Description => "Create book";

        public void Execute(PostBookDto request)
        {
            _validator.ValidateAndThrow(request);

            var book = new Book
            {
                Title = request.Title,
                Description = request.Description,
            };
            var bookAuthor = new List<AuthorBook>();
            var bookGenre = new List<BookGenre>();
            foreach (var a in request.Authors)
            {

                bookAuthor.Add(new AuthorBook
                {
                    Book = book,
                    Author = Context.Users.Where(x => x.Id == a && x.RoleId == 5).FirstOrDefault()
                });
            }
            foreach (var g in request.Genres)
            {
                bookGenre.Add(new BookGenre
                {
                    Book = book,
                    Genre = Context.Genres.Where(x => x.Id == g).FirstOrDefault()
                });
            }

            Context.Books.Add(book);
            Context.AuthorBooks.AddRange(bookAuthor);
            //Context.BookGenres.AddRange(bookGenre);
            Context.SaveChanges();
        }
    }
}
