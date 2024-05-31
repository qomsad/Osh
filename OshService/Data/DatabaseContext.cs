using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using OshService.Domain.Material.MaterialLearning.LearningSection;
using OshService.Domain.Material.MaterialLearning.LearningSectionFile;
using OshService.Domain.Material.MaterialTraining.TrainingQuestion;
using OshService.Domain.Material.MaterialTraining.TrainingQuestionAnswer;
using OshService.Domain.Organization;
using OshService.Domain.OshProgram.OshProgram;
using OshService.Domain.OshProgram.OshProgramAssignment;
using OshService.Domain.OshProgram.OshProgramEmployee.StatusLearning;
using OshService.Domain.OshProgram.OshProgramEmployee.StatusTraining;
using OshService.Domain.OshProgram.OshProgramResult;
using OshService.Domain.Specialty;
using OshService.Domain.User.User;
using OshService.Domain.User.UserAdministrator;
using OshService.Domain.User.UserEmployee;
using OshService.Setup.SetupPrivilege;

namespace OshService.Data;

public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
{
    public DbSet<LearningSectionModel> LearningSection { get; set; }
    public DbSet<LearningSectionFileModel> LearningSectionFile { get; set; }
    public DbSet<TrainingQuestionModel> TrainingQuestionModel { get; set; }
    public DbSet<TrainingQuestionAnswerModel> TrainingQuestionAnswer { get; set; }

    public DbSet<OrganizationModel> Organization { get; set; }

    public DbSet<OshProgramModel> OshProgramModel { get; set; }
    public DbSet<OshProgramAssignmentModel> OshProgramAssignment { get; set; }
    public DbSet<OshProgramResultModel> OshProgramStatus { get; set; }

    public DbSet<EmployeeStatusLearningModel> OshProgramStatusLearning { get; set; }
    public DbSet<EmployeeStatusTrainingModel> OshProgramStatusTraining { get; set; }
    public DbSet<EmployeeStatusTrainingAnswerModel> OshProgramStatusTrainingAnswer { get; set; }

    public DbSet<SpecialtyModel> Specialty { get; set; }

    public DbSet<UserModel> User { get; init; }
    public DbSet<UserAdministratorModel> UserAdministrator { get; init; }
    public DbSet<UserEmployeeModel> UserEmployee { get; set; }

    public DbSet<SetupPrivilegeModel> SetupPrivilege { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.ConfigureWarnings(delegate(WarningsConfigurationBuilder warnings)
        {
            warnings.Ignore(CoreEventId.ForeignKeyAttributesOnBothNavigationsWarning);
        });
    }

    protected override void OnModelCreating(ModelBuilder model)
    {
        model.Entity<LearningSectionFileModel>().Property(e => e.FileType).HasConversion<string>();
        model.Entity<TrainingQuestionModel>().Property(e => e.QuestionType).HasConversion<string>();

        model.Entity<OshProgramModel>().Property(e => e.AutoAssignmentType).HasConversion<string>();

        model.Entity<UserModel>().UseTptMappingStrategy();
        model.Entity<UserModel>().Property(e => e.Type).HasConversion<string>().IsRequired();
    }
}
