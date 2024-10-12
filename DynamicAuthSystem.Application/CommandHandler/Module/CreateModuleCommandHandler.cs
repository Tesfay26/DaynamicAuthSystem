using DynamicAuthSystem.Application.Commands.Modules;
using DynamicAuthSystem.Domain.Entities;
using DynamicAuthSystem.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicAuthSystem.Application.CommandHandler.Modules
{
    public class CreateModuleCommandHandler : IRequestHandler<CreateModuleCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateModuleCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<int> Handle(CreateModuleCommand request, CancellationToken cancellationToken)
        {
            var module = new Module { Name = request.Name };
            await _unitOfWork.Repository<Module>().AddAsync(module);
            await _unitOfWork.CompleteAsync();
            return module.Id;
        }
    }
}
