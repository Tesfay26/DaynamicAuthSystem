using DynamicAuthSystem.Application.DTOs;
using MediatR;

namespace DynamicAuthSystem.Application.Commands
{
    public class CreateRolePermissionCommand : IRequest<RolePermissionDto>
    {
        public string RoleId { get; set; }
        public int ModuleId { get; set; }
        public int ActionId { get; set; }
    }
}
