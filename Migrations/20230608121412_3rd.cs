using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace printingsystem.Migrations
{
    /// <inheritdoc />
    public partial class _3rd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "prints",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false),
                    no_of_pages = table.Column<int>(type: "int", nullable: false),
                    no_of_copies = table.Column<int>(type: "int", nullable: false),
                    type_of_paper = table.Column<string>(type: "longtext", nullable: false),
                    printer_name = table.Column<string>(type: "longtext", nullable: false),
                    files = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_prints", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "prints");
        }
    }
}
