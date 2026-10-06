using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FootballclubBarcelona.Migrations
{
    /// <inheritdoc />
    public partial class AddSizeToProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "TeamAndPlayers",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Size",
                table: "Products",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_TeamAndPlayers_ProductId",
                table: "TeamAndPlayers",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamAndPlayers_Products_ProductId",
                table: "TeamAndPlayers",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamAndPlayers_Products_ProductId",
                table: "TeamAndPlayers");

            migrationBuilder.DropIndex(
                name: "IX_TeamAndPlayers_ProductId",
                table: "TeamAndPlayers");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "TeamAndPlayers");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "Products");
        }
    }
}
