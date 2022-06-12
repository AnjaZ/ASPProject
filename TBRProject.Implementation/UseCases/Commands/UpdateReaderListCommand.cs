using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBRProject.Application.Exceptions;
using TBRProject.Application.UseCases.Commands;
using TBRProject.Application.UseCases.DTO;
using TBRProject.DataAccess;
using TBRProject.Domain;

namespace TBRProject.Implementation.UseCases.Commands
{
    public class UpdateReaderListCommand : EfUseCase, IUpdateReaderListCommand
    {
        private readonly IApplicationUser _user;

        public UpdateReaderListCommand(TBRContext context, IApplicationUser user) : base(context)
        {
            _user = user;
        }

        public int Id => 20;

        public string Name => "Change redar list";

        public string Description => "User can' change status of book";

        public void Execute(ReaderListDto request)
        {
            var list = Context.ReadersList.FirstOrDefault(x => x.UserId == _user.Id && x.BookId == request.BookId);
            if (list == null)
            {
                throw new EntityNotFoundException(nameof(UserBook), request.BookId);
            }
            if (list.Action != request.Action)
            {
                Context.ReadersList.Remove(list);
                var newlist = new UserBook
                {
                    BookId = request.BookId,
                    UserId = _user.Id,
                    Action = request.Action
                };
                Context.ReadersList.Add(newlist);
                Context.SaveChanges();

            }
        }
    }
}
