using AutoMapper;

namespace OshService.Domain.User.User;

public class UserMapper : Profile
{
    public UserMapper()
    {
        CreateMap<UserModel, UserViewRead>();
    }
}
