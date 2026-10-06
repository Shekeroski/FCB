using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FootballclubBarcelona.Migrations
{
    /// <inheritdoc />
    public partial class AddPlayerNameToProductPurchasing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PlayerName",
                table: "ProductPurchasings",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlayerName",
                table: "ProductPurchasings");
        }
    }
}
