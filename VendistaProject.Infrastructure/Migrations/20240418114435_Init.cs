using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VendistaProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Histories",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    dataTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    command = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    param1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    param2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    param3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Histories", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Histories");
        }
    }
}
