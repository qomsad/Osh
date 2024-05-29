using AutoMapper;
using OshService.Domain.Organization;

namespace OshService.Setup.SetupOrganization;

public class SetupOrganizationMapper : Profile
{
    public SetupOrganizationMapper()
    {
        CreateMap<OrganizationModel, SetupOrganizationResponse>();
    }
}
