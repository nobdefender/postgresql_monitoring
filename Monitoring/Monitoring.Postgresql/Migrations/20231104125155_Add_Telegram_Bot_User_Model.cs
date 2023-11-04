using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Monitoring.Postgresql.Migrations
{
    /// <inheritdoc />
    public partial class Add_Telegram_Bot_User_Model : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserToAction");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.CreateTable(
                name: "TelegramBotUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TelegramChatId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramBotUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WebUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: true),
                    EmailAddress = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<string>(type: "text", nullable: false),
                    RefreshToken = table.Column<string>(type: "text", nullable: true),
                    RefreshTokenExpiryTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TelegramBotUserToAction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    ActiondId = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramBotUserToAction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TelegramBotUserToAction_Actions_ActiondId",
                        column: x => x.ActiondId,
                        principalTable: "Actions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TelegramBotUserToAction_TelegramBotUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "TelegramBotUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TelegramBotUserToAction_ActiondId",
                table: "TelegramBotUserToAction",
                column: "ActiondId");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramBotUserToAction_UserId",
                table: "TelegramBotUserToAction",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TelegramBotUserToAction");

            migrationBuilder.DropTable(
                name: "WebUsers");

            migrationBuilder.DropTable(
                name: "TelegramBotUsers");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EmailAddress = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: true),
                    RefreshToken = table.Column<string>(type: "text", nullable: true),
                    RefreshTokenExpiryTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Role = table.Column<string>(type: "text", nullable: false),
                    TelegramChatId = table.Column<long>(type: "bigint", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserToAction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ActiondId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserToAction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserToAction_Actions_ActiondId",
                        column: x => x.ActiondId,
                        principalTable: "Actions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserToAction_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "EmailAddress", "LastName", "Name", "Password", "RefreshToken", "RefreshTokenExpiryTime", "Role", "TelegramChatId", "Username" },
                values: new object[,]
                {
                    { 1, "user@user.ru", "User", "User", "FRy0cmdAIeEgGM0h8LZxBZEy7DqwQnrnCPypjg8ia5WJ9+WFt3niUqDHMcYl/0YyTwsD5GV8eqbJQBjy1biTrg==", null, null, "User", 0L, "User" },
                    { 2, "reviewer@reviewer.ru", "Reviewer", "Reviewer", "lAwsp713uLD9oPruO35jS4zRvH6vXDXi/kJefpm2H7tJNvb/2ugxQh/+90I+olIKH+ifVTd/ZOXH7O4azKp/Wg==", null, null, "Reviewer", 0L, "Reviewer" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserToAction_ActiondId",
                table: "UserToAction",
                column: "ActiondId");

            migrationBuilder.CreateIndex(
                name: "IX_UserToAction_UserId",
                table: "UserToAction",
                column: "UserId");
        }
    }
}
