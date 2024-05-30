﻿using System.Security.Claims;
using AspBoot.Auth;
using AspBoot.Service;
using OshService.Domain.User.User;
using OshService.Domain.User.UserAdministrator;
using OshService.Domain.User.UserEmployee;

namespace OshService.Security;

[Service]
public class SecurityService(
    IHttpContextAccessor context,
    UserRepository repository,
    UserAdministratorRepository administratorRepository,
    UserEmployeeRepository employeeRepository
)
{
    public UserModel? GetCurrentUser()
    {
        var login = context.HttpContext?.User.GetPrincipal(ClaimTypes.Name);
        if (login == null)
        {
            return null;
        }
        return repository.GetByLogin(login);
    }

    public UserAdministratorModel? GetCurrentAdministrator()
    {
        var login = context.HttpContext?.User.GetPrincipal(ClaimTypes.Name);
        if (login == null)
        {
            return null;
        }
        return administratorRepository.GetByLogin(login);
    }

    public UserEmployeeModel? GetCurrentEmployee()
    {
        var login = context.HttpContext?.User.GetPrincipal(ClaimTypes.Name);
        if (login == null)
        {
            return null;
        }
        return employeeRepository.GetByLogin(login);
    }

    public bool CheckAdministratorOrganization(long organizationId)
    {
        var admin = GetCurrentAdministrator();
        return admin != null && admin.ManageOrganizationId == organizationId;
    }

    public bool CheckEmployeeScope(long employeeId)
    {
        var employee = GetCurrentEmployee();
        return employee != null && employee.Id == employeeId;
    }

    public long GetCurrentAdministratorOrganization()
    {
        var id = GetCurrentAdministrator()?.ManageOrganizationId;
        if (id != null)
        {
            return (long) id;
        }

        return 0;
    }
}
