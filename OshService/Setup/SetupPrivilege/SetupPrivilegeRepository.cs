using AspBoot.Repository;
using OshService.Data;

namespace OshService.Setup.SetupPrivilege;

[Repository]
public class SetupPrivilegeRepository(DatabaseContext context)
{
    public bool IsSetupAvailable()
    {
        return !context.SetupPrivilege.Any();
    }

    public void SetupLock()
    {
        context.SetupPrivilege.Add(new SetupPrivilegeModel());
        context.SaveChanges();
    }
}
