using DynamicAuthSystem.Application.Commands;
using DynamicAuthSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace DynamicAuthSystem.Application.CommandHandler
{
    public class EnableTwoFactorAuthCommandHandler : IRequestHandler<EnableTwoFactorAuthCommand, bool>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public EnableTwoFactorAuthCommandHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<bool> Handle(EnableTwoFactorAuthCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null) { return false; }
            await _userManager.SetTwoFactorEnabledAsync(user, true);
            return true;
        }
    }
}
