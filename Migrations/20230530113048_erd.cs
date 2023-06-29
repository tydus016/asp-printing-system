using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace printingsystem.Migrations
{
    /// <inheritdoc />
    public partial class erd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "email",
                table: "users",
                type: "longtext",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "password",
                table: "users",
                type: "longtext",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "email",
                table: "users");

            migrationBuilder.DropColumn(
                name: "password",
                table: "users");
        }
    }
}
