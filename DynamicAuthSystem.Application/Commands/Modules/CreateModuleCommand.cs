using MediatR;

namespace DynamicAuthSystem.Application.Commands.Modules
{
    public class CreateModuleCommand : IRequest<int>
    {
        public string Name { get; set; }
    }
}
