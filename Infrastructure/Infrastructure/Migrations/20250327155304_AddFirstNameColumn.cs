using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddFirstNameColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HashPassword",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PhotoURL",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TelegramFirstName",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "TelegramUserName",
                table: "Users",
                newName: "Photo_url");

            migrationBuilder.AddColumn<string>(
                name: "First_name",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Last_name",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Users",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "First_name",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Last_name",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Photo_url",
                table: "Users",
                newName: "TelegramUserName");

            migrationBuilder.AddColumn<string>(
                name: "HashPassword",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhotoURL",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TelegramFirstName",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
