using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VendistaProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DropColumnHistoryDtoName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "name",
                table: "Histories");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "Histories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
