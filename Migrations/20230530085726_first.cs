using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace printingsystem.Migrations
{
    /// <inheritdoc />
    public partial class first : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    user_id = table.Column<string>(type: "varchar(36)", nullable: false),
                    fullname = table.Column<string>(type: "varchar(255)", nullable: false),
                    id_number = table.Column<string>(type: "varchar(100)", nullable: false),
                    user_type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.user_id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
