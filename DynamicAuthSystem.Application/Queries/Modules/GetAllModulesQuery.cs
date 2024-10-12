using DynamicAuthSystem.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicAuthSystem.Application.Queries.Modules
{
    public class GetAllModulesQuery : IRequest<ICollection<ModuleDto>>
    {
    }
}
