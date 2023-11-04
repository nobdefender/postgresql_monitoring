using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Monitoring.Postgresql.Migrations
{
    /// <inheritdoc />
    public partial class AddForeignKeyToUserToAction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ActiondId",
                table: "UserToAction",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<long>(
                name: "TelegramChatId",
                table: "Users",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "TelegramChatId",
                value: 0L);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "TelegramChatId",
                value: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_UserToAction_ActiondId",
                table: "UserToAction",
                column: "ActiondId");

            migrationBuilder.CreateIndex(
                name: "IX_UserToAction_UserId",
                table: "UserToAction",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserToAction_Actions_ActiondId",
                table: "UserToAction",
                column: "ActiondId",
                principalTable: "Actions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserToAction_Users_UserId",
                table: "UserToAction",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserToAction_Actions_ActiondId",
                table: "UserToAction");

            migrationBuilder.DropForeignKey(
                name: "FK_UserToAction_Users_UserId",
                table: "UserToAction");

            migrationBuilder.DropIndex(
                name: "IX_UserToAction_ActiondId",
                table: "UserToAction");

            migrationBuilder.DropIndex(
                name: "IX_UserToAction_UserId",
                table: "UserToAction");

            migrationBuilder.DropColumn(
                name: "TelegramChatId",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "ActiondId",
                table: "UserToAction",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }
    }
}
