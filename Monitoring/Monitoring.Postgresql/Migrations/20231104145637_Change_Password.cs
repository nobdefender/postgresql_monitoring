using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Monitoring.Postgresql.Migrations
{
    /// <inheritdoc />
    public partial class Change_Password : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TelegramBotUserToAction_TelegramBotUsers_UserId",
                table: "TelegramBotUserToAction");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "TelegramBotUserToAction",
                newName: "TelegramBotUserId");

            migrationBuilder.RenameIndex(
                name: "IX_TelegramBotUserToAction_UserId",
                table: "TelegramBotUserToAction",
                newName: "IX_TelegramBotUserToAction_TelegramBotUserId");

            migrationBuilder.InsertData(
                table: "WebUsers",
                columns: new[] { "Id", "EmailAddress", "LastName", "Name", "Password", "RefreshToken", "RefreshTokenExpiryTime", "Role", "Username" },
                values: new object[] { 1, "admin@gmail.ru", "Admin_lastname", "Admin", "oKPLwtnO+/yAnIiNhuDVDtOUwo67CERyInTV3MV66r0DJBFFcUdMnoLCoPj0LpClIHHeCCs9169KJisL6o7VfQ==", null, null, "Admin", "Admin_username" });

            migrationBuilder.AddForeignKey(
                name: "FK_TelegramBotUserToAction_TelegramBotUsers_TelegramBotUserId",
                table: "TelegramBotUserToAction",
                column: "TelegramBotUserId",
                principalTable: "TelegramBotUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TelegramBotUserToAction_TelegramBotUsers_TelegramBotUserId",
                table: "TelegramBotUserToAction");

            migrationBuilder.DeleteData(
                table: "WebUsers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.RenameColumn(
                name: "TelegramBotUserId",
                table: "TelegramBotUserToAction",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_TelegramBotUserToAction_TelegramBotUserId",
                table: "TelegramBotUserToAction",
                newName: "IX_TelegramBotUserToAction_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TelegramBotUserToAction_TelegramBotUsers_UserId",
                table: "TelegramBotUserToAction",
                column: "UserId",
                principalTable: "TelegramBotUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
