using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Booking.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateComplainsandOwnerTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Complains_Admins_AdminId",
                table: "Complains");

            migrationBuilder.DropIndex(
                name: "IX_Complains_AdminId",
                table: "Complains");

            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "Complains");

            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "Complains",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Complains_OwnerId",
                table: "Complains",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Complains_Owner_OwnerId",
                table: "Complains",
                column: "OwnerId",
                principalTable: "Owner",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Complains_Owner_OwnerId",
                table: "Complains");

            migrationBuilder.DropIndex(
                name: "IX_Complains_OwnerId",
                table: "Complains");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Complains");

            migrationBuilder.AddColumn<int>(
                name: "AdminId",
                table: "Complains",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Complains_AdminId",
                table: "Complains",
                column: "AdminId");

            migrationBuilder.AddForeignKey(
                name: "FK_Complains_Admins_AdminId",
                table: "Complains",
                column: "AdminId",
                principalTable: "Admins",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
