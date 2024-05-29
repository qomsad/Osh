using AutoMapper;
using OshService.Domain.User.UserAdministrator;

namespace OshService.Setup.SuperUser;

public class SetupUserMapper : Profile
{
    public SetupUserMapper()
    {
        CreateMap<SetupUserRequest, UserAdministratorModel>();
        CreateMap<UserAdministratorModel, SetupUserResponse>();
    }
}
