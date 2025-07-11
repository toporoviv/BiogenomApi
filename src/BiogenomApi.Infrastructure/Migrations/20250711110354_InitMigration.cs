using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BiogenomApi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DietarySupplements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    Application = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DietarySupplements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Foods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Foods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Birthday = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Gender = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vitamins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    MeasurementUnit = table.Column<int>(type: "integer", nullable: false),
                    LowerLimit = table.Column<double>(type: "double precision", nullable: false),
                    UpperLimit = table.Column<double>(type: "double precision", nullable: true),
                    ImportanceForHealth = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    ScarcityManifestation = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    Prevention = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vitamins", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DietarySupplementImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Data = table.Column<byte[]>(type: "bytea", nullable: false),
                    DietarySupplementId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DietarySupplementImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DietarySupplementImages_DietarySupplements_DietarySupplemen~",
                        column: x => x.DietarySupplementId,
                        principalTable: "DietarySupplements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserVitaminSurvey",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    SurveyAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserVitaminSurvey", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserVitaminSurvey_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FoodVitamin",
                columns: table => new
                {
                    FoodId = table.Column<int>(type: "integer", nullable: false),
                    VitaminId = table.Column<int>(type: "integer", nullable: false),
                    Amount = table.Column<double>(type: "double precision", nullable: false),
                    AmountUnit = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodVitamin", x => new { x.FoodId, x.VitaminId });
                    table.ForeignKey(
                        name: "FK_FoodVitamin_Foods_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Foods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FoodVitamin_Vitamins_VitaminId",
                        column: x => x.VitaminId,
                        principalTable: "Vitamins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VitaminDietarySupplements",
                columns: table => new
                {
                    VitaminId = table.Column<int>(type: "integer", nullable: false),
                    DietarySupplementId = table.Column<int>(type: "integer", nullable: false),
                    Amount = table.Column<double>(type: "double precision", nullable: false),
                    AmountUnit = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VitaminDietarySupplements", x => new { x.VitaminId, x.DietarySupplementId });
                    table.ForeignKey(
                        name: "FK_VitaminDietarySupplements_DietarySupplements_DietarySupplem~",
                        column: x => x.DietarySupplementId,
                        principalTable: "DietarySupplements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VitaminDietarySupplements_Vitamins_VitaminId",
                        column: x => x.VitaminId,
                        principalTable: "Vitamins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserVitaminSurveyResult",
                columns: table => new
                {
                    VitaminId = table.Column<int>(type: "integer", nullable: false),
                    UserVitaminSurveyId = table.Column<int>(type: "integer", nullable: false),
                    Amount = table.Column<double>(type: "double precision", nullable: false),
                    AmountUnit = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserVitaminSurveyResult", x => new { x.UserVitaminSurveyId, x.VitaminId });
                    table.ForeignKey(
                        name: "FK_UserVitaminSurveyResult_UserVitaminSurvey_UserVitaminSurvey~",
                        column: x => x.UserVitaminSurveyId,
                        principalTable: "UserVitaminSurvey",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserVitaminSurveyResult_Vitamins_VitaminId",
                        column: x => x.VitaminId,
                        principalTable: "Vitamins",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DietarySupplementImages_DietarySupplementId",
                table: "DietarySupplementImages",
                column: "DietarySupplementId");

            migrationBuilder.CreateIndex(
                name: "IX_Foods_Name",
                table: "Foods",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FoodVitamin_VitaminId",
                table: "FoodVitamin",
                column: "VitaminId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Gender",
                table: "Users",
                column: "Gender");

            migrationBuilder.CreateIndex(
                name: "IX_UserVitaminSurvey_UserId",
                table: "UserVitaminSurvey",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserVitaminSurveyResult_VitaminId",
                table: "UserVitaminSurveyResult",
                column: "VitaminId");

            migrationBuilder.CreateIndex(
                name: "IX_VitaminDietarySupplements_DietarySupplementId",
                table: "VitaminDietarySupplements",
                column: "DietarySupplementId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DietarySupplementImages");

            migrationBuilder.DropTable(
                name: "FoodVitamin");

            migrationBuilder.DropTable(
                name: "UserVitaminSurveyResult");

            migrationBuilder.DropTable(
                name: "VitaminDietarySupplements");

            migrationBuilder.DropTable(
                name: "Foods");

            migrationBuilder.DropTable(
                name: "UserVitaminSurvey");

            migrationBuilder.DropTable(
                name: "DietarySupplements");

            migrationBuilder.DropTable(
                name: "Vitamins");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
