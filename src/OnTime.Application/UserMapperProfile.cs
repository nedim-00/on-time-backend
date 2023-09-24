using AutoMapper;
using OnTime.Application.Users;
using OnTime.Application.Users.Commands;

namespace OnTime.Application;

public class UserMapperProfile : Profile
{
    public UserMapperProfile()
    {
        _ = CreateMap<User, UserResponseDto>();
        _ = CreateMap<User, UpdateUserResponseDto>();
        _ = CreateMap<User, BasicUserInformationResposneDto>();
        _ = CreateMap<UserResponseDto, BasicUserInformationResposneDto>();
        _ = CreateMap<CreateUserCommand, User>();

        _ = CreateMap<UpdateUserCommand, User>();
    }
}
