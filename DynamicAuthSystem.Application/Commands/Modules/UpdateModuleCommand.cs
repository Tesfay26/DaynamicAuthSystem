using DynamicAuthSystem.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicAuthSystem.Application.Commands.Modules
{
    public class UpdateModuleCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
