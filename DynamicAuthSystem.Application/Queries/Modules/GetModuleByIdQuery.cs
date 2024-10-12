using DynamicAuthSystem.Application.DTOs;
using MediatR;

namespace DynamicAuthSystem.Application.Queries.Modules
{
    public class GetModuleByIdQuery : IRequest<ModuleDto>
    {
        public int Id { get; set; }
        public GetModuleByIdQuery(int id)
        {
            Id = id;   
        }
    }
}
