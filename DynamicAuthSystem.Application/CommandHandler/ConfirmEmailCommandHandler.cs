using DynamicAuthSystem.Application.Commands;
using DynamicAuthSystem.Domain.Entities;
using DynamicAuthSystem.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace DynamicAuthSystem.Application.CommandHandler
{
    public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, bool>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public ConfirmEmailCommandHandler(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null) { return false; }
            var result = await _userManager.ConfirmEmailAsync(user, request.Token);
            return result.Succeeded;
        }
    }
}
