using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AnnouncementImplementation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cards_Events_EventId",
                table: "Cards");

            migrationBuilder.DropForeignKey(
                name: "FK_Cards_Users_RequesterId",
                table: "Cards");

            migrationBuilder.DropIndex(
                name: "IX_Cards_EventId",
                table: "Cards");

            migrationBuilder.DropIndex(
                name: "IX_Cards_RequesterId",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "RequestedDate",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "RequesterId",
                table: "Cards");

            migrationBuilder.RenameColumn(
                name: "Link",
                table: "Cards",
                newName: "TcgPlayerLink");

            migrationBuilder.AddColumn<string>(
                name: "CardHoarderLink",
                table: "Cards",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CardMarketLink",
                table: "Cards",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImageLink",
                table: "Cards",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Cards",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Announcements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Count = table.Column<int>(type: "integer", nullable: false),
                    RequestedDate = table.Column<DateOnly>(type: "date", nullable: false),
                    RequesterId = table.Column<Guid>(type: "uuid", nullable: false),
                    CardId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Announcements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Announcements_Cards_CardId",
                        column: x => x.CardId,
                        principalTable: "Cards",
                        principalColumn: "TelegramId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Announcements_Users_RequesterId",
                        column: x => x.RequesterId,
                        principalTable: "Users",
                        principalColumn: "TelegramId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Announcements_CardId",
                table: "Announcements",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_Announcements_RequesterId",
                table: "Announcements",
                column: "RequesterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Announcements");

            migrationBuilder.DropColumn(
                name: "CardHoarderLink",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "CardMarketLink",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "ImageLink",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Cards");

            migrationBuilder.RenameColumn(
                name: "TcgPlayerLink",
                table: "Cards",
                newName: "Link");

            migrationBuilder.AddColumn<Guid>(
                name: "EventId",
                table: "Cards",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "Number",
                table: "Cards",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateOnly>(
                name: "RequestedDate",
                table: "Cards",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<Guid>(
                name: "RequesterId",
                table: "Cards",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Cards_EventId",
                table: "Cards",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_RequesterId",
                table: "Cards",
                column: "RequesterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_Events_EventId",
                table: "Cards",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "TelegramId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_Users_RequesterId",
                table: "Cards",
                column: "RequesterId",
                principalTable: "Users",
                principalColumn: "TelegramId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
