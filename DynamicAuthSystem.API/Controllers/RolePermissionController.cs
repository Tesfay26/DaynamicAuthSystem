using DynamicAuthSystem.Application.Commands;
using DynamicAuthSystem.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DynamicAuthSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolePermissionController : ControllerBase
    {
        private readonly IMediator _mediator;
        public RolePermissionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET api/<RolePermissionController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _mediator.Send(new GetRolePermissionByIdQuery(id));
                if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // POST api/<RolePermissionController>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRolePermissionCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(Get), new {id = result.Id}, result);
        }
    }
}
