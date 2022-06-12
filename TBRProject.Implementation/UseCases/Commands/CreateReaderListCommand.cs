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
    public class CreateReaderListCommand : EfUseCase, ICreateReaderListCommand
    {
        private readonly IApplicationUser _user;
        public CreateReaderListCommand(TBRContext context, IApplicationUser user) : base(context)
        {
            _user = user;
        }

        public int Id => 19;

        public string Name => "ReaderList";

        public string Description => "Insert choosen book and status for user";

        public void Execute(ReaderListDto request)
        {
            var readerList = new UserBook
            {
                UserId = _user.Id,
                BookId = request.BookId,
                Action = request.Action
            };

            Context.ReadersList.Add(readerList);
            Context.SaveChanges();
        }
    }
}
