using AutoMapper;

namespace OshService.Domain.User.UserEmployee;

public class UserEmployeeMapper : Profile
{
    public UserEmployeeMapper()
    {
        CreateMap<UserEmployeeViewCreate, UserEmployeeModel>();
        CreateMap<UserEmployeeViewUpdate, UserEmployeeModel>();
        CreateMap<UserEmployeeModel, UserEmployeeViewRead>();
    }
}
