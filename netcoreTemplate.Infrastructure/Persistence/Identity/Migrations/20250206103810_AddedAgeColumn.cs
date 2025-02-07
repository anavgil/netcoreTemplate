using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace netcoreTemplate.Infrastructure.Persistence.Identity.Migrations
{
    /// <inheritdoc />
    public partial class AddedAgeColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "age",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "age",
                table: "AspNetUsers");
        }
    }
}
