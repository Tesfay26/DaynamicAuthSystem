using MediatR;

namespace DynamicAuthSystem.Application.Commands
{
    public class LoginUserCommand : IRequest<string>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
