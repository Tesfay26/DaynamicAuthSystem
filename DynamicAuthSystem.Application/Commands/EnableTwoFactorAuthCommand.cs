using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicAuthSystem.Application.Commands
{
    public class EnableTwoFactorAuthCommand : IRequest<bool>
    {
        public string UserId { get; set; }
    }
}
