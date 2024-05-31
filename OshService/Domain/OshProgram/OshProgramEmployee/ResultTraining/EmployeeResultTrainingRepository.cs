using AspBoot.Data.Implementation;
using AspBoot.Repository;
using OshService.Data;

namespace OshService.Domain.OshProgram.OshProgramEmployee.ResultTraining;

[Repository]
public class EmployeeResultTrainingRepository(DatabaseContext context)
    : Repository<EmployeeResultTrainingModel>(context) {
}
