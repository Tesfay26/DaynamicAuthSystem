using DynamicAuthSystem.Application.Commands;
using DynamicAuthSystem.Application.DTOs;
using DynamicAuthSystem.Domain.Entities;
using DynamicAuthSystem.Domain.Interfaces;
using MediatR;

namespace DynamicAuthSystem.Application.CommandHandler
{
    public class CreateRolePermissionHandler : IRequestHandler<CreateRolePermissionCommand, RolePermissionDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateRolePermissionHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<RolePermissionDto> Handle(CreateRolePermissionCommand request, CancellationToken cancellationToken)
        {
            var rolePermission = new RolePermission
            {
                RoleId = request.RoleId,
                ModuleId = request.ModuleId,
                ActionId = request.ActionId
            };

            await _unitOfWork.Repository<RolePermission>().AddAsync(rolePermission);
            await _unitOfWork.CompleteAsync();

            // Optionally map to DTO
            return new RolePermissionDto
            {
                Id = rolePermission.Id,
                RoleName = rolePermission.Role.Name,
                ModuleName = rolePermission.Module.Name,
                ActionName = rolePermission.Action.Name
            };
        }
    }
}
