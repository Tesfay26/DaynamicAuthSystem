using AutoMapper;
using DynamicAuthSystem.Application.DTOs;
using DynamicAuthSystem.Domain.Entities;

namespace DynamicAuthSystem.Application.MappingProfiles
{
    public class RolePermissionProfile : Profile
    {
        public RolePermissionProfile() 
        {
            CreateMap<RolePermission, RolePermissionDto>()
                  .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.Name))
                  .ForMember(dest => dest.ModuleName, opt => opt.MapFrom(src => src.Module.Name))
                  .ForMember(dest => dest.ActionName, opt => opt.MapFrom(src => src.Action.Name));
        }
    }
}
