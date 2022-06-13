using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBRProject.Application.Emails;
using TBRProject.Application.UseCases;
using TBRProject.Application.UseCases.Commands;
using TBRProject.Application.UseCases.DTO;
using TBRProject.DataAccess;
using TBRProject.Domain;
using TBRProject.Implementation.Validators;

namespace TBRProject.Implementation.UseCases.Commands
{
    public class CreateUserCommand : EfUseCase, ICreateUserCommand
    {
        private readonly CreateUserValidator _validator;
        private readonly IEmailSender _sender;

        public CreateUserCommand(TBRContext context, CreateUserValidator validator, IEmailSender sender) : base(context)
        {
            _validator = validator;
            _sender = sender;
        }
        public int Id => 4;

        public string Name => "Create user";

        public string Description => "User reigstration.";

        public void Execute(CreateUserDto request)
        {
            _validator.ValidateAndThrow(request);

            var hash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Password = hash,
                RoleId = 4
            };

            if (!string.IsNullOrEmpty(request.ImageFileName))
            {
                var image = new Image
                {
                    Path = request.ImageFileName
                };
                user.Image = image;
            }
            Context.Users.Add(user);
            var usecases = new List<UserUseCase>
            {
                new UserUseCase { UseCaseId = 16, User = user },
                new UserUseCase { UseCaseId = 17, User = user },
                new UserUseCase { UseCaseId = 18, User = user },
                new UserUseCase { UseCaseId = 20, User = user },
                new UserUseCase { UseCaseId = 19, User = user },
                new UserUseCase { UseCaseId = 2, User = user},
                new UserUseCase { UseCaseId = 5, User = user },
                new UserUseCase { UseCaseId = 3, User = user },
                new UserUseCase { UseCaseId = 6, User = user },
                new UserUseCase { UseCaseId = 8, User = user },
                new UserUseCase { UseCaseId = 12, User = user },
            };
            Context.UserUseCases.AddRange(usecases);
            Context.SaveChanges();

            //_sender.Send(new EmailDto
            //{
            //    To = request.Email,
            //    Title = "Successfull registration!",
            //    Body = "Dear " + request.FirstName + "\n Please activate your account...."
            //});
        }
    }
}
