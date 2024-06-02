using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace OshService.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "setup_privilege",
                columns: table => new
                {
                    locked = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_setup_privilege", x => x.locked);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    type = table.Column<string>(type: "text", nullable: false),
                    login = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    password_hash = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    password_salt = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    first_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    middle_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    last_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "learning_section",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    text = table.Column<string>(type: "text", nullable: false),
                    program_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_learning_section", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "learning_section_file",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    file_path = table.Column<string>(type: "text", nullable: false),
                    file_type = table.Column<string>(type: "text", nullable: false),
                    learning_section_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_learning_section_file", x => x.id);
                    table.ForeignKey(
                        name: "FK_learning_section_file_learning_section_learning_section_id",
                        column: x => x.learning_section_id,
                        principalTable: "learning_section",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "organization",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    url = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    user_administrator_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_organization", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "specialty",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    organization_id = table.Column<long>(type: "bigint", nullable: false),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_specialty", x => x.id);
                    table.ForeignKey(
                        name: "FK_specialty_organization_organization_id",
                        column: x => x.organization_id,
                        principalTable: "organization",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_administrator",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false),
                    email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    manage_organization_id = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_administrator", x => x.id);
                    table.ForeignKey(
                        name: "FK_user_administrator_organization_manage_organization_id",
                        column: x => x.manage_organization_id,
                        principalTable: "organization",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_user_administrator_user_id",
                        column: x => x.id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "program",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    organization_id = table.Column<long>(type: "bigint", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    learning_minutes_duration = table.Column<int>(type: "integer", nullable: false),
                    training_minutes_duration = table.Column<int>(type: "integer", nullable: false),
                    speciality_id = table.Column<long>(type: "bigint", nullable: true),
                    auto_assignment_type = table.Column<string>(type: "text", nullable: false),
                    max_auto_assignments = table.Column<int>(type: "integer", nullable: true),
                    training_success_rate = table.Column<decimal>(type: "numeric(4,2)", precision: 4, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_program", x => x.id);
                    table.ForeignKey(
                        name: "FK_program_organization_organization_id",
                        column: x => x.organization_id,
                        principalTable: "organization",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_program_specialty_speciality_id",
                        column: x => x.speciality_id,
                        principalTable: "specialty",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "user_employee",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false),
                    service_number = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    organization_id = table.Column<long>(type: "bigint", nullable: false),
                    speciality_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_employee", x => x.id);
                    table.ForeignKey(
                        name: "FK_user_employee_organization_organization_id",
                        column: x => x.organization_id,
                        principalTable: "organization",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_employee_specialty_speciality_id",
                        column: x => x.speciality_id,
                        principalTable: "specialty",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_employee_user_id",
                        column: x => x.id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "training_question",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    type = table.Column<string>(type: "text", nullable: false),
                    question = table.Column<string>(type: "text", nullable: false),
                    rate = table.Column<int>(type: "integer", nullable: false),
                    program_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_training_question", x => x.id);
                    table.ForeignKey(
                        name: "FK_training_question_program_program_id",
                        column: x => x.program_id,
                        principalTable: "program",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "training_question_answer",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    index = table.Column<int>(type: "integer", nullable: false),
                    value = table.Column<string>(type: "text", nullable: false),
                    is_right = table.Column<bool>(type: "boolean", nullable: false),
                    training_question_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_training_question_answer", x => x.id);
                    table.ForeignKey(
                        name: "FK_training_question_answer_training_question_training_questio~",
                        column: x => x.training_question_id,
                        principalTable: "training_question",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "program_assigment",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_employee_id = table.Column<long>(type: "bigint", nullable: false),
                    program_id = table.Column<long>(type: "bigint", nullable: false),
                    assignment_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    start_learning = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    start_training = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    program_result_id = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_program_assigment", x => x.id);
                    table.ForeignKey(
                        name: "FK_program_assigment_program_program_id",
                        column: x => x.program_id,
                        principalTable: "program",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_program_assigment_user_employee_user_employee_id",
                        column: x => x.user_employee_id,
                        principalTable: "user_employee",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "program_result",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    program_assignment_id = table.Column<long>(type: "bigint", nullable: false),
                    learning_result = table.Column<decimal>(type: "numeric(4,2)", precision: 4, scale: 2, nullable: false),
                    training_result = table.Column<decimal>(type: "numeric(4,2)", precision: 4, scale: 2, nullable: false),
                    timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_program_result", x => x.id);
                    table.ForeignKey(
                        name: "FK_program_result_program_assigment_program_assignment_id",
                        column: x => x.program_assignment_id,
                        principalTable: "program_assigment",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "result_learning",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    program_assignment_id = table.Column<long>(type: "bigint", nullable: false),
                    learning_section_id = table.Column<long>(type: "bigint", nullable: false),
                    timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_result_learning", x => x.id);
                    table.ForeignKey(
                        name: "FK_result_learning_learning_section_learning_section_id",
                        column: x => x.learning_section_id,
                        principalTable: "learning_section",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_result_learning_program_assigment_program_assignment_id",
                        column: x => x.program_assignment_id,
                        principalTable: "program_assigment",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "result_training",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    program_assignment_id = table.Column<long>(type: "bigint", nullable: false),
                    training_section_id = table.Column<long>(type: "bigint", nullable: false),
                    timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_result_training", x => x.id);
                    table.ForeignKey(
                        name: "FK_result_training_program_assigment_program_assignment_id",
                        column: x => x.program_assignment_id,
                        principalTable: "program_assigment",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_result_training_training_question_training_section_id",
                        column: x => x.training_section_id,
                        principalTable: "training_question",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "result_training_answer",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    program_status_training_id = table.Column<long>(type: "bigint", nullable: false),
                    training_question_answer_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_result_training_answer", x => x.id);
                    table.ForeignKey(
                        name: "FK_result_training_answer_result_training_program_status_train~",
                        column: x => x.program_status_training_id,
                        principalTable: "result_training",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_result_training_answer_training_question_answer_training_qu~",
                        column: x => x.training_question_answer_id,
                        principalTable: "training_question_answer",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_learning_section_program_id",
                table: "learning_section",
                column: "program_id");

            migrationBuilder.CreateIndex(
                name: "IX_learning_section_file_learning_section_id",
                table: "learning_section_file",
                column: "learning_section_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_organization_url",
                table: "organization",
                column: "url",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_organization_user_administrator_id",
                table: "organization",
                column: "user_administrator_id");

            migrationBuilder.CreateIndex(
                name: "IX_program_organization_id",
                table: "program",
                column: "organization_id");

            migrationBuilder.CreateIndex(
                name: "IX_program_speciality_id",
                table: "program",
                column: "speciality_id");

            migrationBuilder.CreateIndex(
                name: "IX_program_assigment_program_id",
                table: "program_assigment",
                column: "program_id");

            migrationBuilder.CreateIndex(
                name: "IX_program_assigment_program_result_id",
                table: "program_assigment",
                column: "program_result_id");

            migrationBuilder.CreateIndex(
                name: "IX_program_assigment_user_employee_id",
                table: "program_assigment",
                column: "user_employee_id");

            migrationBuilder.CreateIndex(
                name: "IX_program_result_program_assignment_id",
                table: "program_result",
                column: "program_assignment_id");

            migrationBuilder.CreateIndex(
                name: "IX_result_learning_learning_section_id",
                table: "result_learning",
                column: "learning_section_id");

            migrationBuilder.CreateIndex(
                name: "IX_result_learning_program_assignment_id",
                table: "result_learning",
                column: "program_assignment_id");

            migrationBuilder.CreateIndex(
                name: "IX_result_training_program_assignment_id",
                table: "result_training",
                column: "program_assignment_id");

            migrationBuilder.CreateIndex(
                name: "IX_result_training_training_section_id",
                table: "result_training",
                column: "training_section_id");

            migrationBuilder.CreateIndex(
                name: "IX_result_training_answer_program_status_training_id",
                table: "result_training_answer",
                column: "program_status_training_id");

            migrationBuilder.CreateIndex(
                name: "IX_result_training_answer_training_question_answer_id",
                table: "result_training_answer",
                column: "training_question_answer_id");

            migrationBuilder.CreateIndex(
                name: "IX_specialty_organization_id",
                table: "specialty",
                column: "organization_id");

            migrationBuilder.CreateIndex(
                name: "IX_training_question_program_id",
                table: "training_question",
                column: "program_id");

            migrationBuilder.CreateIndex(
                name: "IX_training_question_answer_training_question_id",
                table: "training_question_answer",
                column: "training_question_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_login",
                table: "user",
                column: "login",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_administrator_manage_organization_id",
                table: "user_administrator",
                column: "manage_organization_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_employee_organization_id",
                table: "user_employee",
                column: "organization_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_employee_speciality_id",
                table: "user_employee",
                column: "speciality_id");

            migrationBuilder.AddForeignKey(
                name: "FK_learning_section_program_program_id",
                table: "learning_section",
                column: "program_id",
                principalTable: "program",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_organization_user_administrator_user_administrator_id",
                table: "organization",
                column: "user_administrator_id",
                principalTable: "user_administrator",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_program_assigment_program_result_program_result_id",
                table: "program_assigment",
                column: "program_result_id",
                principalTable: "program_result",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_program_assigment_program_program_id",
                table: "program_assigment");

            migrationBuilder.DropForeignKey(
                name: "FK_organization_user_administrator_user_administrator_id",
                table: "organization");

            migrationBuilder.DropForeignKey(
                name: "FK_specialty_organization_organization_id",
                table: "specialty");

            migrationBuilder.DropForeignKey(
                name: "FK_user_employee_organization_organization_id",
                table: "user_employee");

            migrationBuilder.DropForeignKey(
                name: "FK_user_employee_specialty_speciality_id",
                table: "user_employee");

            migrationBuilder.DropForeignKey(
                name: "FK_program_assigment_program_result_program_result_id",
                table: "program_assigment");

            migrationBuilder.DropTable(
                name: "learning_section_file");

            migrationBuilder.DropTable(
                name: "result_learning");

            migrationBuilder.DropTable(
                name: "result_training_answer");

            migrationBuilder.DropTable(
                name: "setup_privilege");

            migrationBuilder.DropTable(
                name: "learning_section");

            migrationBuilder.DropTable(
                name: "result_training");

            migrationBuilder.DropTable(
                name: "training_question_answer");

            migrationBuilder.DropTable(
                name: "training_question");

            migrationBuilder.DropTable(
                name: "program");

            migrationBuilder.DropTable(
                name: "user_administrator");

            migrationBuilder.DropTable(
                name: "organization");

            migrationBuilder.DropTable(
                name: "specialty");

            migrationBuilder.DropTable(
                name: "program_result");

            migrationBuilder.DropTable(
                name: "program_assigment");

            migrationBuilder.DropTable(
                name: "user_employee");

            migrationBuilder.DropTable(
                name: "user");
        }
    }
}
