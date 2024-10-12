using DynamicAuthSystem.Application.DTOs;
using DynamicAuthSystem.Application.Queries;
using DynamicAuthSystem.Domain.Entities;
using DynamicAuthSystem.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicAuthSystem.Application.QueryHandler
{
    public class GetRolePermissionByIdHandler : IRequestHandler<GetRolePermissionByIdQuery, RolePermissionDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetRolePermissionByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<RolePermissionDto> Handle(GetRolePermissionByIdQuery request, CancellationToken cancellationToken)
        {
           var rolePermission = await _unitOfWork.Repository<RolePermission>().GetByIdAsync(request.Id);
            if (rolePermission == null)
            {
                return null; //handle the case when no entity is found.
            }

            return new RolePermissionDto
            {
                Id = rolePermission.Id,
                RoleName = rolePermission.Role.Name,
                ModuleName = rolePermission.Module.Name,
                ActionName = rolePermission.Action.Name,
            };
        }
    }
}
