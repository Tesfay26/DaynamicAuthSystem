using MediatR;

namespace DynamicAuthSystem.Application.Commands
{
    public class RegisterUserCommand : IRequest<bool>
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
