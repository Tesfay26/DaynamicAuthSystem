using DynamicAuthSystem.Application.Commands.Modules;
using DynamicAuthSystem.Application.DTOs;
using DynamicAuthSystem.Application.Queries.Modules;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DynamicAuthSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModuleController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ModuleController> _logger;

        public ModuleController(IMediator mediator, ILogger<ModuleController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Gets all modules.
        /// </summary>
        /// <returns>A list of modules.</returns>
        [HttpGet]
        public async Task<ActionResult<List<ModuleDto>>> GetAllModules()
        {
            _logger.LogInformation("Fetching all modules.");
            try
            {
                var query = new GetAllModulesQuery();
                var result = await _mediator.Send(query);
                if (result.Count == 0)
                {
                    _logger.LogWarning("No modules found.");
                    return NotFound("No modules available.");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching modules.");
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }

        /// <summary>
        /// Gets a module by its ID.
        /// </summary>
        /// <param name="id">The module ID.</param>
        /// <returns>The requested module.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ModuleDto>> GetModuleById(int id)
        {
            _logger.LogInformation($"Fetching module with ID: {id}.");
            try
            {
                var query = new GetModuleByIdQuery(id);
                var result = await _mediator.Send(query);
                if (result == null)
                {
                    _logger.LogWarning($"Module with ID {id} not found.");
                    return NotFound($"Module with ID {id} not found.");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while fetching the module with ID: {id}.");
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }

        /// <summary>
        /// Creates a new module.
        /// </summary>
        /// <param name="command">The module creation command.</param>
        /// <returns>The ID of the created module.</returns>
        [HttpPost]
        public async Task<ActionResult<int>> CreateModule([FromBody] CreateModuleCommand command)
        {
            _logger.LogInformation("Creating a new module.");
            try
            {
                if (command == null || string.IsNullOrWhiteSpace(command.Name))
                {
                    _logger.LogWarning("Invalid module data provided.");
                    return BadRequest("Module name is required.");
                }

                var result = await _mediator.Send(command);
                _logger.LogInformation($"Module created with ID: {result}.");
                return CreatedAtAction(nameof(GetModuleById), new { id = result }, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a new module.");
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }

        /// <summary>
        /// Updates an existing module.
        /// </summary>
        /// <param name="id">The ID of the module to update.</param>
        /// <param name="command">The module update command.</param>
        /// <returns>No content if successful.</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateModule(int id, [FromBody] UpdateModuleCommand command)
        {
            _logger.LogInformation($"Updating module with ID: {id}.");
            try
            {
                if (id != command.Id)
                {
                    _logger.LogWarning("Module ID mismatch.");
                    return BadRequest("Module ID mismatch.");
                }

                await _mediator.Send(command);
                _logger.LogInformation($"Module with ID: {id} updated successfully.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating the module with ID: {id}.");
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }

        /// <summary>
        /// Deletes a module.
        /// </summary>
        /// <param name="id">The ID of the module to delete.</param>
        /// <returns>No content if successful.</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteModule(int id)
        {
            _logger.LogInformation($"Deleting module with ID: {id}.");
            try
            {
                await _mediator.Send(new DeleteModuleCommand(id));
                _logger.LogInformation($"Module with ID: {id} deleted successfully.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting the module with ID: {id}.");
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }
    }
}
