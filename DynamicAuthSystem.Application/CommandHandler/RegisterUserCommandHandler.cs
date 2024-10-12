using DynamicAuthSystem.Application.Commands;
using DynamicAuthSystem.Domain.Entities;
using DynamicAuthSystem.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace DynamicAuthSystem.Application.CommandHandler
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, bool>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IUnitOfWork _unitOfWork;

        public RegisterUserCommandHandler(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var user = new ApplicationUser
            {
                UserName = request.UserName,
                Email = request.Email,
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(request.Role))
                {
                    if (!await _roleManager.RoleExistsAsync(request.Role))
                    {
                        await _roleManager.CreateAsync(new ApplicationRole { Name = request.Role });
                    }

                    await _userManager.AddToRoleAsync(user, request.Role);
                }
                await _unitOfWork.CompleteAsync();
                return true;
            }
            return false;
        }
    }
}
