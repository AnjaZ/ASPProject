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
    public class ReaderListController : ControllerBase
    {
        private UseCaseHandler _handler;
        private readonly IExceptionLogger _logger;
        public ReaderListController(UseCaseHandler handler, IExceptionLogger logger)
        {
            _handler = handler;
            _logger = logger;
        }
        // POST api/<ReaderListController>
        [HttpPost]
        public IActionResult Post(
            [FromBody] ReaderListDto dto,
            [FromServices] ICreateReaderListCommand command)
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

        // PUT api/<ReaderListController>/5
        [HttpPatch]
        public IActionResult Patch([FromBody] ReaderListDto request, [FromServices] IUpdateReaderListCommand command)
        {
            _handler.HandleCommand(command, request);
            return StatusCode(201);
        }

    }
}
