using DynamicAuthSystem.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicAuthSystem.Application.Queries
{
    public class GetRolePermissionByIdQuery : IRequest<RolePermissionDto>
    {
        public int Id { get; set; }
        public GetRolePermissionByIdQuery(int id)
        {
            Id = id;
        }
    }
}
