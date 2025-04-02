using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixUserPropertiesNaming : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Photo_url",
                table: "Users",
                newName: "PhotoUrl");

            migrationBuilder.RenameColumn(
                name: "Last_name",
                table: "Users",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "First_name",
                table: "Users",
                newName: "FirstName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhotoUrl",
                table: "Users",
                newName: "Photo_url");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Users",
                newName: "Last_name");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Users",
                newName: "First_name");
        }
    }
}
