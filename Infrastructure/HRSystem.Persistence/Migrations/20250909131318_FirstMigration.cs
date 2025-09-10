using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRSystem.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    firstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tckimlik = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dogumTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    telNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    departmentId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    departmentName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    totalLeave = table.Column<int>(type: "int", nullable: true),
                    usedLeave = table.Column<int>(type: "int", nullable: true),
                    position = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    workingStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    personnelPhoto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    startDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordSalt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    adres = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    salary = table.Column<int>(type: "int", nullable: true),
                    mealCost = table.Column<int>(type: "int", nullable: true),
                    transportCost = table.Column<int>(type: "int", nullable: true),
                    otherCost = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Departments_Name",
                table: "Departments",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
