using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TBRProject.Application.UseCases.DTO.Searches;
using TBRProject.Application.UseCases.Queries;
using TBRProject.Implementation;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TBRProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private UseCaseHandler _handler;

        public RolesController(UseCaseHandler handler)
        {
            _handler = handler;
        }

        // GET: api/<RolesController>
        [HttpGet]
        public IActionResult Get([FromQuery]BaseSearch search, [FromServices] IGetRolesQuery query)
        {
            return Ok(_handler.HandleQuery(query, search));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] IGetUsersFromRoreIdQuery query)
        {
            return Ok(_handler.HandleQuery(query, id));
        }
    }
}
