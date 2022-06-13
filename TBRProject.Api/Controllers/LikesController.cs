using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TBRProject.Api.Extensions;
using TBRProject.Application.Logging;
using TBRProject.Application.UseCases.Commands;
using TBRProject.Application.UseCases.DTO;
using TBRProject.Implementation;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TBRProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikesController : ControllerBase
    {
        private UseCaseHandler _handler;
        private readonly IExceptionLogger _logger;
        public LikesController(UseCaseHandler handler, IExceptionLogger logger)
        {
            _handler = handler;
            _logger = logger;
        }

        // POST api/<LikesController>
        [HttpPost]
        public IActionResult Post(
            [FromBody] LikeDto dto,
            [FromServices] ICreateLikeCommand command)
        {
            try
            {
                _handler.HandleCommand(command, dto);

                return StatusCode(StatusCodes.Status201Created);
            }
            catch (ValidationException ex)
            {
                return ex.Errors.AsUnprocessableEntity();
            }
            catch (System.Exception ex)
            {
                _logger.Log(ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        // DELETE api/<LikesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteLikeCommand command)
        {
            _handler.HandleCommand(command, id);
            return NoContent();
        }
    }
}
