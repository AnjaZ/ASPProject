using Microsoft.AspNetCore.Http;
using TBRProject.Application.UseCases.DTO;

namespace TBRProject.Api.Core.Dto
{
    public class CreateUserDtoWithImage : CreateUserDto
    {
        public IFormFile Image { get; set; }
    }
}
