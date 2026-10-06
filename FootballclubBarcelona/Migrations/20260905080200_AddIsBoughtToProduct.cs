using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FootballclubBarcelona.Migrations
{
    /// <inheritdoc />
    public partial class AddIsBoughtToProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsBought",
                table: "Products",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBought",
                table: "Products");
        }
    }
}
