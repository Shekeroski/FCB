using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FootballclubBarcelona.Migrations
{
    /// <inheritdoc />
    public partial class AddUserToTicketPurchasing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_UserProfiles_UserProfileId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_UserProfileId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "UserProfileId",
                table: "Tickets");

            migrationBuilder.AddColumn<int>(
                name: "UserProfileId",
                table: "TicketPurchasings",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TicketPurchasings_UserProfileId",
                table: "TicketPurchasings",
                column: "UserProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketPurchasings_UserProfiles_UserProfileId",
                table: "TicketPurchasings",
                column: "UserProfileId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketPurchasings_UserProfiles_UserProfileId",
                table: "TicketPurchasings");

            migrationBuilder.DropIndex(
                name: "IX_TicketPurchasings_UserProfileId",
                table: "TicketPurchasings");

            migrationBuilder.DropColumn(
                name: "UserProfileId",
                table: "TicketPurchasings");

            migrationBuilder.AddColumn<int>(
                name: "UserProfileId",
                table: "Tickets",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_UserProfileId",
                table: "Tickets",
                column: "UserProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_UserProfiles_UserProfileId",
                table: "Tickets",
                column: "UserProfileId",
                principalTable: "UserProfiles",
                principalColumn: "Id");
        }
    }
}
