using AutoMapper;

namespace OshService.Domain.User.UserAdministrator;

public class UserAdministratorMapper : Profile
{
    public UserAdministratorMapper()
    {
        CreateMap<UserAdministratorModel, UserAdministratorViewRead>();
    }
}
