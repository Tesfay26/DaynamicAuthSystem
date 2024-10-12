using MediatR;

namespace DynamicAuthSystem.Application.Commands.Modules
{
    public class DeleteModuleCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public DeleteModuleCommand(int id)
        {
            Id = id;
        }
    }
}
