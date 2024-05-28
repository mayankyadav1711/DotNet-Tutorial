using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Books.Migrations
{
    /// <inheritdoc />
    public partial class @new : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Users",
                newName: "UserType");

            migrationBuilder.RenameColumn(
                name: "Surname",
                table: "Users",
                newName: "UserFullName");

            migrationBuilder.RenameColumn(
                name: "Role",
                table: "Users",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "GivenName",
                table: "Users",
                newName: "LastName");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ForgotPasswords",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    TempId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    RequestDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForgotPasswords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ForgotPasswords_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ForgotPasswords_UserId",
                table: "ForgotPasswords",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ForgotPasswords");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "UserType",
                table: "Users",
                newName: "Username");

            migrationBuilder.RenameColumn(
                name: "UserFullName",
                table: "Users",
                newName: "Surname");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Users",
                newName: "Role");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Users",
                newName: "GivenName");
        }
    }
}
