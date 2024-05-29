﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using OshService.Domain.Material.MaterialLearning.LearningSection;
using OshService.Domain.Material.MaterialLearning.LearningSectionFile;
using OshService.Domain.Material.MaterialTraining.TrainingQuestion;
using OshService.Domain.Material.MaterialTraining.TrainingQuestionAnswer;
using OshService.Domain.Organization;
using OshService.Domain.OshProgram.OshProgram;
using OshService.Domain.OshProgram.OshProgramAssignment;
using OshService.Domain.OshProgram.OshProgramStatus;
using OshService.Domain.OshProgram.OshProgramStatusLearning;
using OshService.Domain.OshProgram.OshProgramStatusTraining;
using OshService.Domain.OshProgram.OshProgramStatusTrainingAnswer;
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
    public DbSet<OshProgramStatusModel> OshProgramStatus { get; set; }
    public DbSet<OshProgramStatusLearningModel> OshProgramStatusLearning { get; set; }
    public DbSet<OshProgramStatusTrainingModel> OshProgramStatusTraining { get; set; }
    public DbSet<OshProgramStatusTrainingAnswerModel> OshProgramStatusTrainingAnswer { get; set; }

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
        model.Entity<OshProgramStatusModel>().Property(e => e.GlobalStatus).HasConversion<string>();
        model.Entity<OshProgramStatusLearningModel>().Property(e => e.LearningStatus).HasConversion<string>();
        model.Entity<OshProgramStatusTrainingModel>().Property(e => e.TrainingStatus).HasConversion<string>();

        model.Entity<UserModel>().UseTptMappingStrategy();
        model.Entity<UserModel>().Property(e => e.Type).HasConversion<string>().IsRequired();
    }
}
