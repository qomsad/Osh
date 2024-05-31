using AspBoot.Data.Implementation;
using AspBoot.Repository;
using OshService.Data;

namespace OshService.Domain.OshProgram.OshProgramEmployee.ResultLearning;

[Repository]
public class EmployeeResultLearningRepository(DatabaseContext context)
    : Repository<EmployeeResultLearningModel>(context) { }
