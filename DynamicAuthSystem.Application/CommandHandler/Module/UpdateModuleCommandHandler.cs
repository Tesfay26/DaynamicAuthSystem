using DynamicAuthSystem.Application.Commands.Modules;
using DynamicAuthSystem.Domain.Entities;
using DynamicAuthSystem.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DynamicAuthSystem.Application.CommandHandler
{
    public class UpdateModuleCommandHandler : IRequestHandler<UpdateModuleCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UpdateModuleCommandHandler> _logger;

        public UpdateModuleCommandHandler(IUnitOfWork unitOfWork, ILogger<UpdateModuleCommandHandler> logger)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(UpdateModuleCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Editing module with ID: {request.Id}");

            var module = await _unitOfWork.Repository<Module>().GetByIdAsync(request.Id);
            if (module == null)
            {
                _logger.LogWarning($"Module with ID: {request.Id} not found.");
                return false; // Or throw a NotFoundException
            }

            module.Name = request.Name;

            _unitOfWork.Repository<Module>().Update(module);
            var result = await _unitOfWork.CompleteAsync();

            if (result > 0)
            {
                _logger.LogInformation($"Module with ID: {request.Id} was updated successfully.");
                return true;
            }
            else
            {
                _logger.LogError($"Failed to update module with ID: {request.Id}.");
                return false;
            }
        }
    }
}
