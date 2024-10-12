using DynamicAuthSystem.Application.Commands;
using DynamicAuthSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicAuthSystem.Application.CommandHandler
{
    public class LogoutUserCommandHandler : IRequestHandler<LogoutUserCommand, Unit>
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        public LogoutUserCommandHandler(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }
        public async Task<Unit> Handle(LogoutUserCommand request, CancellationToken cancellationToken)
        {
            await _signInManager.SignOutAsync();
            return Unit.Value;
        }
    }
}
