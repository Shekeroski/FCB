using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FootballclubBarcelona.Migrations
{
    /// <inheritdoc />
    public partial class ProductPurchasingToUserProfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_UserProfiles_UserProfileId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_UserProfileId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "UserProfileId",
                table: "Products");

            migrationBuilder.AlterColumn<string>(
                name: "price",
                table: "Tickets",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "price",
                table: "Tickets",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<int>(
                name: "UserProfileId",
                table: "Products",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_UserProfileId",
                table: "Products",
                column: "UserProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_UserProfiles_UserProfileId",
                table: "Products",
                column: "UserProfileId",
                principalTable: "UserProfiles",
                principalColumn: "Id");
        }
    }
}
