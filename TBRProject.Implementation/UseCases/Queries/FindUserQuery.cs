using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBRProject.Application.UseCases.DTO;
using TBRProject.Application.UseCases.Queries;
using TBRProject.DataAccess;
using TBRProject.Domain;

namespace TBRProject.Implementation.UseCases.Queries
{
    public class FindUserQuery : EfUseCase, IFindUserQuery
    {
        public int Id => 6;

        public string Name => "Find User query";

        public string Description => "Query for finding single user";

        public FindUserQuery(TBRContext context) : base(context)
        {

        }
        public FindUserDto Execute(int id)
        {
            var query = Context.Users.Find(id);

            if (query == null)
            {
                throw new EntryPointNotFoundException(nameof(User));
            }

            return new FindUserDto
            {
                Id = query.Id,
                FirstName = query.FirstName,
                LastName = query.LastName,
                Email = query.Email,
                Password = query.Password,
                Role = query.Role.Name,
                Image = query.Image?.Path,
                CurrentlyReading = query.UserBooks.Where(x => x.Action == 6).Select(x => x.Books.Title),
                WantToRead = query.UserBooks.Where(x => x.Action == 4).Select(x => x.Books.Title),
                ReadBooks = query.UserBooks.Where(x => x.Action == 5).Select(x => x.Books.Title)
            };
        }
    }
}
