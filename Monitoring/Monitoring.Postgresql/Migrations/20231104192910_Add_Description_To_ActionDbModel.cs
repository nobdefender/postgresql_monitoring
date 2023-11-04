using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Monitoring.Postgresql.Migrations
{
    /// <inheritdoc />
    public partial class Add_Description_To_ActionDbModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Actions",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Actions");
        }
    }
}
