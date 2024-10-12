using DynamicAuthSystem.Application.Commands.Modules;
using DynamicAuthSystem.Domain.Entities;
using DynamicAuthSystem.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DynamicAuthSystem.Application.CommandHandler
{
    public class DeleteModuleCommandHandler : IRequestHandler<DeleteModuleCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeleteModuleCommandHandler> _logger;

        public DeleteModuleCommandHandler(IUnitOfWork unitOfWork, ILogger<DeleteModuleCommandHandler> logger)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(DeleteModuleCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Deleting module with ID: {request.Id}");

            var module = await _unitOfWork.Repository<Module>().GetByIdAsync(request.Id);
            if (module == null)
            {
                _logger.LogWarning($"Module with ID: {request.Id} not found.");
                return false; // Or throw a NotFoundException
            }

            _unitOfWork.Repository<Module>().Delete(module);
            var result = await _unitOfWork.CompleteAsync();

            if (result > 0)
            {
                _logger.LogInformation($"Module with ID: {request.Id} was deleted successfully.");
                return true;
            }
            else
            {
                _logger.LogError($"Failed to delete module with ID: {request.Id}.");
                return false;
            }
        }
    }
}
