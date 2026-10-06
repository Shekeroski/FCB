using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FootballclubBarcelona.Migrations
{
    /// <inheritdoc />
    public partial class FixProductPurchasingUserRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ProductPurchasings");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "ProductPurchasings",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
