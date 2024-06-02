using AspBoot.Data.Implementation;
using AspBoot.Repository;
using OshService.Data;

namespace OshService.Domain.OshProgram.OshProgramResult;

[Repository]
public class OshProgramResultRepository(DatabaseContext context) : Repository<OshProgramResultModel>(context) { }
