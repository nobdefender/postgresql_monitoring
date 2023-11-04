using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Monitoring.Postgresql.Migrations
{
    /// <inheritdoc />
    public partial class AddUserDataInTelegramBotUserDbModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "TelegramBotUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "TelegramBotUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "TelegramBotUsers",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "TelegramBotUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "TelegramBotUsers");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "TelegramBotUsers");
        }
    }
}
