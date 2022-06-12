using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TBRProject.Api.Extensions;
using TBRProject.Application.Logging;
using TBRProject.Application.UseCases.Commands;
using TBRProject.Application.UseCases.DTO;
using TBRProject.Application.UseCases.DTO.Searches;
using TBRProject.Application.UseCases.Queries;
using TBRProject.Implementation;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TBRProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BooksController : ControllerBase
    {
        private UseCaseHandler _handler;
        private readonly IExceptionLogger _logger;

        public BooksController(UseCaseHandler handler, IExceptionLogger logger)
        {
            _handler = handler;
            _logger = logger;
        }
        // GET: api/<BooksController>
        [HttpGet]
        
        public IActionResult Get([FromQuery] BasePagedSearch search, [FromServices] IGetBooksQuery query)
        {
            return Ok(_handler.HandleQuery(query, search));
        }

        // GET api/<BooksController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] IFindBookQuery query)
        {
            return Ok(_handler.HandleQuery(query, id));
        }

        // POST api/<BooksController>
        [HttpPost]
        public IActionResult Post(
            [FromBody] PostBookDto dto,
            [FromServices] ICreateBookCommand command)
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


        // DELETE api/<BooksController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteBookCommand command)
        {
            _handler.HandleCommand(command, id);
            return NoContent();
        }
    }
}
