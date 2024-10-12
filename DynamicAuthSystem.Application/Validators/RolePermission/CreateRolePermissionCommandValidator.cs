using DynamicAuthSystem.Application.Commands;
using FluentValidation;

namespace DynamicAuthSystem.Application.Validators.RolePermission
{
    public class CreateRolePermissionCommandValidator : AbstractValidator<CreateRolePermissionCommand>
    {
        public CreateRolePermissionCommandValidator() 
        {
            RuleFor(x => x.RoleId).NotEmpty();
            RuleFor(x => x.ModuleId).GreaterThan(0);
            RuleFor(x => x.ActionId).GreaterThan(1);
        }
    }
}
