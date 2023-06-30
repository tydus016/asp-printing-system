using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace printingsystem.Migrations
{
    /// <inheritdoc />
    public partial class paperstable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "papers",
                columns: table => new
                {
                    paper_id = table.Column<Guid>(type: "char(36)", nullable: false),
                    paper_name = table.Column<string>(type: "longtext", nullable: false),
                    date = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_papers", x => x.paper_id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "papers");
        }
    }
}
