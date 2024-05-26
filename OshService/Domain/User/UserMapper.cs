using AutoMapper;

namespace OshService.Domain.User;

public class UserMapper : Profile
{
    public UserMapper()
    {
        CreateMap<UserViewCreate, UserModel>();
        CreateMap<UserModel, UserViewRead>();
    }
}
