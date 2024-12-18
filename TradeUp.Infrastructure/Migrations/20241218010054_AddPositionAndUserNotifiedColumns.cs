using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TradeUp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPositionAndUserNotifiedColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Position",
                table: "Subscriptions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "UserNotified",
                table: "Subscriptions",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Position",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "UserNotified",
                table: "Subscriptions");
        }
    }
}
