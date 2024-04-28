using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VendistaProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddNewColumnToHistoryDtoName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "Histories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "name",
                table: "Histories");
        }
    }
}
