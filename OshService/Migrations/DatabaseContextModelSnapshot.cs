﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using OshService.Data;

#nullable disable

namespace OshService.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("OshService.Domain.Material.MaterialLearning.LearningSection.LearningSectionModel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("name");

                    b.Property<long>("OshProgramId")
                        .HasColumnType("bigint")
                        .HasColumnName("program_id");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("text");

                    b.HasKey("Id");

                    b.HasIndex("OshProgramId");

                    b.ToTable("learning_section");
                });

            modelBuilder.Entity("OshService.Domain.Material.MaterialLearning.LearningSectionFile.LearningSectionFileModel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("file_path");

                    b.Property<string>("FileType")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("file_type");

                    b.Property<long>("LearningSectionId")
                        .HasColumnType("bigint")
                        .HasColumnName("learning_section_id");

                    b.HasKey("Id");

                    b.HasIndex("LearningSectionId")
                        .IsUnique();

                    b.ToTable("learning_section_file");
                });

            modelBuilder.Entity("OshService.Domain.Material.MaterialTraining.TrainingQuestion.TrainingQuestionModel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("OshProgramId")
                        .HasColumnType("bigint")
                        .HasColumnName("program_id");

                    b.Property<string>("Question")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("question");

                    b.Property<string>("QuestionType")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("type");

                    b.Property<int>("Rate")
                        .HasColumnType("integer")
                        .HasColumnName("rate");

                    b.HasKey("Id");

                    b.HasIndex("OshProgramId");

                    b.ToTable("training_question");
                });

            modelBuilder.Entity("OshService.Domain.Material.MaterialTraining.TrainingQuestionAnswer.TrainingQuestionAnswerModel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<int>("Index")
                        .HasColumnType("integer")
                        .HasColumnName("index");

                    b.Property<bool>("IsRight")
                        .HasColumnType("boolean")
                        .HasColumnName("is_right");

                    b.Property<long>("TrainingQuestionId")
                        .HasColumnType("bigint")
                        .HasColumnName("training_question_id");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("value");

                    b.HasKey("Id");

                    b.HasIndex("TrainingQuestionId");

                    b.ToTable("training_question_answer");
                });

            modelBuilder.Entity("OshService.Domain.Organization.OrganizationModel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("name");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("url");

                    b.Property<long>("UserAdministratorId")
                        .HasColumnType("bigint")
                        .HasColumnName("user_administrator_id");

                    b.HasKey("Id");

                    b.HasIndex("Url")
                        .IsUnique();

                    b.HasIndex("UserAdministratorId");

                    b.ToTable("organization");
                });

            modelBuilder.Entity("OshService.Domain.OshProgram.OshProgram.OshProgramModel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("AutoAssignmentType")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("auto_assignment_type");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<int>("LearningMinutesDuration")
                        .HasColumnType("integer")
                        .HasColumnName("learning_minutes_duration");

                    b.Property<int?>("MaxAutoAssignments")
                        .HasColumnType("integer")
                        .HasColumnName("max_auto_assignments");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<long>("OrganizationId")
                        .HasColumnType("bigint")
                        .HasColumnName("organization_id");

                    b.Property<long?>("SpecialityId")
                        .HasColumnType("bigint")
                        .HasColumnName("speciality_id");

                    b.Property<int>("TrainingMinutesDuration")
                        .HasColumnType("integer")
                        .HasColumnName("training_minutes_duration");

                    b.Property<decimal>("TrainingSuccessRate")
                        .HasPrecision(4, 2)
                        .HasColumnType("numeric(4,2)")
                        .HasColumnName("training_success_rate");

                    b.HasKey("Id");

                    b.HasIndex("OrganizationId");

                    b.HasIndex("SpecialityId");

                    b.ToTable("program");
                });

            modelBuilder.Entity("OshService.Domain.OshProgram.OshProgramAssignment.OshProgramAssignmentModel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("AssignmentDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("assignment_date");

                    b.Property<long>("OshProgramId")
                        .HasColumnType("bigint")
                        .HasColumnName("program_id");

                    b.Property<long?>("OshProgramResultId")
                        .HasColumnType("bigint")
                        .HasColumnName("program_result_id");

                    b.Property<DateTime?>("StartLearning")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("start_learning");

                    b.Property<DateTime?>("StartTraining")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("start_training");

                    b.Property<long>("UserEmployeeId")
                        .HasColumnType("bigint")
                        .HasColumnName("user_employee_id");

                    b.HasKey("Id");

                    b.HasIndex("OshProgramId");

                    b.HasIndex("OshProgramResultId");

                    b.HasIndex("UserEmployeeId");

                    b.ToTable("program_assigment");
                });

            modelBuilder.Entity("OshService.Domain.OshProgram.OshProgramEmployee.ResultLearning.EmployeeResultLearningModel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("LearningSectionId")
                        .HasColumnType("bigint")
                        .HasColumnName("learning_section_id");

                    b.Property<long>("OshProgramAssignmentId")
                        .HasColumnType("bigint")
                        .HasColumnName("program_assignment_id");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("timestamp");

                    b.HasKey("Id");

                    b.HasIndex("LearningSectionId");

                    b.HasIndex("OshProgramAssignmentId");

                    b.ToTable("result_learning");
                });

            modelBuilder.Entity("OshService.Domain.OshProgram.OshProgramEmployee.ResultTraining.EmployeeResultTrainingAnswerModel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("OshProgramStatusTrainingId")
                        .HasColumnType("bigint")
                        .HasColumnName("program_status_training_id");

                    b.Property<long>("TrainingQuestionAnswerId")
                        .HasColumnType("bigint")
                        .HasColumnName("training_question_answer_id");

                    b.HasKey("Id");

                    b.HasIndex("OshProgramStatusTrainingId");

                    b.HasIndex("TrainingQuestionAnswerId");

                    b.ToTable("result_training_answer");
                });

            modelBuilder.Entity("OshService.Domain.OshProgram.OshProgramEmployee.ResultTraining.EmployeeResultTrainingModel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("OshProgramAssignmentId")
                        .HasColumnType("bigint")
                        .HasColumnName("program_assignment_id");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("timestamp");

                    b.Property<long>("TrainingQuestionId")
                        .HasColumnType("bigint")
                        .HasColumnName("training_section_id");

                    b.HasKey("Id");

                    b.HasIndex("OshProgramAssignmentId");

                    b.HasIndex("TrainingQuestionId");

                    b.ToTable("result_training");
                });

            modelBuilder.Entity("OshService.Domain.OshProgram.OshProgramResult.OshProgramResultModel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<decimal>("LearningResult")
                        .HasPrecision(4, 2)
                        .HasColumnType("numeric(4,2)")
                        .HasColumnName("learning_result");

                    b.Property<long>("OshProgramAssignmentId")
                        .HasColumnType("bigint")
                        .HasColumnName("program_assignment_id");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("timestamp");

                    b.Property<decimal>("TrainingResult")
                        .HasPrecision(4, 2)
                        .HasColumnType("numeric(4,2)")
                        .HasColumnName("training_result");

                    b.HasKey("Id");

                    b.HasIndex("OshProgramAssignmentId");

                    b.ToTable("program_result");
                });

            modelBuilder.Entity("OshService.Domain.Specialty.SpecialtyModel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("name");

                    b.Property<long>("OrganizationId")
                        .HasColumnType("bigint")
                        .HasColumnName("organization_id");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated");

                    b.HasKey("Id");

                    b.HasIndex("OrganizationId");

                    b.ToTable("specialty");
                });

            modelBuilder.Entity("OshService.Domain.User.User.UserModel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("FirstName")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("last_name");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("login");

                    b.Property<string>("MiddleName")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("middle_name");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("password_hash");

                    b.Property<string>("PasswordSalt")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("password_salt");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("type");

                    b.HasKey("Id");

                    b.HasIndex("Login")
                        .IsUnique();

                    b.ToTable("user");

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("OshService.Setup.SetupPrivilege.SetupPrivilegeModel", b =>
                {
                    b.Property<long>("Locked")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("locked");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Locked"));

                    b.HasKey("Locked");

                    b.ToTable("setup_privilege");
                });

            modelBuilder.Entity("OshService.Domain.User.UserAdministrator.UserAdministratorModel", b =>
                {
                    b.HasBaseType("OshService.Domain.User.User.UserModel");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("email");

                    b.Property<long?>("ManageOrganizationId")
                        .HasColumnType("bigint")
                        .HasColumnName("manage_organization_id");

                    b.HasIndex("ManageOrganizationId");

                    b.ToTable("user_administrator");
                });

            modelBuilder.Entity("OshService.Domain.User.UserEmployee.UserEmployeeModel", b =>
                {
                    b.HasBaseType("OshService.Domain.User.User.UserModel");

                    b.Property<long>("OrganizationId")
                        .HasColumnType("bigint")
                        .HasColumnName("organization_id");

                    b.Property<string>("ServiceNumber")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("service_number");

                    b.Property<long>("SpecialityId")
                        .HasColumnType("bigint")
                        .HasColumnName("speciality_id");

                    b.HasIndex("OrganizationId");

                    b.HasIndex("SpecialityId");

                    b.ToTable("user_employee");
                });

            modelBuilder.Entity("OshService.Domain.Material.MaterialLearning.LearningSection.LearningSectionModel", b =>
                {
                    b.HasOne("OshService.Domain.OshProgram.OshProgram.OshProgramModel", "OshProgram")
                        .WithMany("LearningSections")
                        .HasForeignKey("OshProgramId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OshProgram");
                });

            modelBuilder.Entity("OshService.Domain.Material.MaterialLearning.LearningSectionFile.LearningSectionFileModel", b =>
                {
                    b.HasOne("OshService.Domain.Material.MaterialLearning.LearningSection.LearningSectionModel", "LearningSection")
                        .WithOne("LearningSectionFile")
                        .HasForeignKey("OshService.Domain.Material.MaterialLearning.LearningSectionFile.LearningSectionFileModel", "LearningSectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LearningSection");
                });

            modelBuilder.Entity("OshService.Domain.Material.MaterialTraining.TrainingQuestion.TrainingQuestionModel", b =>
                {
                    b.HasOne("OshService.Domain.OshProgram.OshProgram.OshProgramModel", "OshProgram")
                        .WithMany("TrainingQuestions")
                        .HasForeignKey("OshProgramId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OshProgram");
                });

            modelBuilder.Entity("OshService.Domain.Material.MaterialTraining.TrainingQuestionAnswer.TrainingQuestionAnswerModel", b =>
                {
                    b.HasOne("OshService.Domain.Material.MaterialTraining.TrainingQuestion.TrainingQuestionModel", "TrainingQuestion")
                        .WithMany("Answers")
                        .HasForeignKey("TrainingQuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TrainingQuestion");
                });

            modelBuilder.Entity("OshService.Domain.Organization.OrganizationModel", b =>
                {
                    b.HasOne("OshService.Domain.User.UserAdministrator.UserAdministratorModel", "Administrator")
                        .WithMany()
                        .HasForeignKey("UserAdministratorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Administrator");
                });

            modelBuilder.Entity("OshService.Domain.OshProgram.OshProgram.OshProgramModel", b =>
                {
                    b.HasOne("OshService.Domain.Organization.OrganizationModel", "Organization")
                        .WithMany()
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OshService.Domain.Specialty.SpecialtyModel", "Specialty")
                        .WithMany()
                        .HasForeignKey("SpecialityId");

                    b.Navigation("Organization");

                    b.Navigation("Specialty");
                });

            modelBuilder.Entity("OshService.Domain.OshProgram.OshProgramAssignment.OshProgramAssignmentModel", b =>
                {
                    b.HasOne("OshService.Domain.OshProgram.OshProgram.OshProgramModel", "OshProgram")
                        .WithMany()
                        .HasForeignKey("OshProgramId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OshService.Domain.OshProgram.OshProgramResult.OshProgramResultModel", "Result")
                        .WithMany()
                        .HasForeignKey("OshProgramResultId");

                    b.HasOne("OshService.Domain.User.UserEmployee.UserEmployeeModel", "Employee")
                        .WithMany()
                        .HasForeignKey("UserEmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("OshProgram");

                    b.Navigation("Result");
                });

            modelBuilder.Entity("OshService.Domain.OshProgram.OshProgramEmployee.ResultLearning.EmployeeResultLearningModel", b =>
                {
                    b.HasOne("OshService.Domain.Material.MaterialLearning.LearningSection.LearningSectionModel", "LearningSection")
                        .WithMany()
                        .HasForeignKey("LearningSectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OshService.Domain.OshProgram.OshProgramAssignment.OshProgramAssignmentModel", "OshProgramAssignment")
                        .WithMany()
                        .HasForeignKey("OshProgramAssignmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LearningSection");

                    b.Navigation("OshProgramAssignment");
                });

            modelBuilder.Entity("OshService.Domain.OshProgram.OshProgramEmployee.ResultTraining.EmployeeResultTrainingAnswerModel", b =>
                {
                    b.HasOne("OshService.Domain.OshProgram.OshProgramEmployee.ResultTraining.EmployeeResultTrainingModel", "Training")
                        .WithMany("Answers")
                        .HasForeignKey("OshProgramStatusTrainingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OshService.Domain.Material.MaterialTraining.TrainingQuestionAnswer.TrainingQuestionAnswerModel", "ActualAnswer")
                        .WithMany()
                        .HasForeignKey("TrainingQuestionAnswerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ActualAnswer");

                    b.Navigation("Training");
                });

            modelBuilder.Entity("OshService.Domain.OshProgram.OshProgramEmployee.ResultTraining.EmployeeResultTrainingModel", b =>
                {
                    b.HasOne("OshService.Domain.OshProgram.OshProgramAssignment.OshProgramAssignmentModel", "OshProgramAssignment")
                        .WithMany()
                        .HasForeignKey("OshProgramAssignmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OshService.Domain.Material.MaterialTraining.TrainingQuestion.TrainingQuestionModel", "TrainingQuestion")
                        .WithMany()
                        .HasForeignKey("TrainingQuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OshProgramAssignment");

                    b.Navigation("TrainingQuestion");
                });

            modelBuilder.Entity("OshService.Domain.OshProgram.OshProgramResult.OshProgramResultModel", b =>
                {
                    b.HasOne("OshService.Domain.OshProgram.OshProgramAssignment.OshProgramAssignmentModel", "OshProgramAssignment")
                        .WithMany()
                        .HasForeignKey("OshProgramAssignmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OshProgramAssignment");
                });

            modelBuilder.Entity("OshService.Domain.Specialty.SpecialtyModel", b =>
                {
                    b.HasOne("OshService.Domain.Organization.OrganizationModel", "Organization")
                        .WithMany()
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Organization");
                });

            modelBuilder.Entity("OshService.Domain.User.UserAdministrator.UserAdministratorModel", b =>
                {
                    b.HasOne("OshService.Domain.User.User.UserModel", null)
                        .WithOne()
                        .HasForeignKey("OshService.Domain.User.UserAdministrator.UserAdministratorModel", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OshService.Domain.Organization.OrganizationModel", "ManageOrganization")
                        .WithMany()
                        .HasForeignKey("ManageOrganizationId");

                    b.Navigation("ManageOrganization");
                });

            modelBuilder.Entity("OshService.Domain.User.UserEmployee.UserEmployeeModel", b =>
                {
                    b.HasOne("OshService.Domain.User.User.UserModel", null)
                        .WithOne()
                        .HasForeignKey("OshService.Domain.User.UserEmployee.UserEmployeeModel", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OshService.Domain.Organization.OrganizationModel", "Organization")
                        .WithMany()
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OshService.Domain.Specialty.SpecialtyModel", "Specialty")
                        .WithMany("Employees")
                        .HasForeignKey("SpecialityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Organization");

                    b.Navigation("Specialty");
                });

            modelBuilder.Entity("OshService.Domain.Material.MaterialLearning.LearningSection.LearningSectionModel", b =>
                {
                    b.Navigation("LearningSectionFile");
                });

            modelBuilder.Entity("OshService.Domain.Material.MaterialTraining.TrainingQuestion.TrainingQuestionModel", b =>
                {
                    b.Navigation("Answers");
                });

            modelBuilder.Entity("OshService.Domain.OshProgram.OshProgram.OshProgramModel", b =>
                {
                    b.Navigation("LearningSections");

                    b.Navigation("TrainingQuestions");
                });

            modelBuilder.Entity("OshService.Domain.OshProgram.OshProgramEmployee.ResultTraining.EmployeeResultTrainingModel", b =>
                {
                    b.Navigation("Answers");
                });

            modelBuilder.Entity("OshService.Domain.Specialty.SpecialtyModel", b =>
                {
                    b.Navigation("Employees");
                });
#pragma warning restore 612, 618
        }
    }
}
