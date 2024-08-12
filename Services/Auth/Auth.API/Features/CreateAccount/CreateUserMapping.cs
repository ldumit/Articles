using Auth.Domain.Models;
using AutoMapper;

namespace Auth.API.Features.CreateAccount;

public class CreateUserMapping : Profile
{
    public CreateUserMapping()
    {
        //talk - parent child relation & inheritance
        CreateMap<UserRoleDto, UserRole>()
            .ForMember(dest => dest.RoleId, opt => opt.MapFrom(s => s.Type));
        CreateMap<CreateUserCommand, User>()
						//.Include<UserRoleDto, UserRole>()
            .ForMember(d => d.UserName, opt => opt.MapFrom(s => s.Email));
    }
}
