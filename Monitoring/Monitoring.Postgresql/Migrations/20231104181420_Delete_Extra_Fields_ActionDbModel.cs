using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Monitoring.Postgresql.Migrations
{
    /// <inheritdoc />
    public partial class Delete_Extra_Fields_ActionDbModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Actionid",
                table: "Actions");

            migrationBuilder.DropColumn(
                name: "Esc_period",
                table: "Actions");

            migrationBuilder.DropColumn(
                name: "Eventsource",
                table: "Actions");

            migrationBuilder.DropColumn(
                name: "Notify_if_canceled",
                table: "Actions");

            migrationBuilder.DropColumn(
                name: "Pause_suppressed",
                table: "Actions");

            migrationBuilder.DropColumn(
                name: "Pause_symptoms",
                table: "Actions");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Actions");

            migrationBuilder.UpdateData(
                table: "WebUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "oKPLwtnO+/yAnIiNhuDVDtOUwo67CERyInTV3MV66r0DJBFFcUdMnoLCoPj0LpClIHHeCCs9169KJisL6o7VfQ==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Actionid",
                table: "Actions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Esc_period",
                table: "Actions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Eventsource",
                table: "Actions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Notify_if_canceled",
                table: "Actions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Pause_suppressed",
                table: "Actions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Pause_symptoms",
                table: "Actions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Actions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "WebUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "FRy0cmdAIeEgGM0h8LZxBZEy7DqwQnrnCPypjg8ia5WJ9+WFt3niUqDHMcYl/0YyTwsD5GV8eqbJQBjy1biTrg==");
        }
    }
}
