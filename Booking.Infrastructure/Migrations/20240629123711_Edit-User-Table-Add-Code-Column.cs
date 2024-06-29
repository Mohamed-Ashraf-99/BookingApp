using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Booking.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EditUserTableAddCodeColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_images_Hotels_HotelID",
                table: "images");

            migrationBuilder.DropPrimaryKey(
                name: "PK_images",
                table: "images");

            migrationBuilder.RenameTable(
                name: "images",
                newName: "Images");

            migrationBuilder.RenameIndex(
                name: "IX_images_HotelID",
                table: "Images",
                newName: "IX_Images_HotelID");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Images",
                table: "Images",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Hotels_HotelID",
                table: "Images",
                column: "HotelID",
                principalTable: "Hotels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Hotels_HotelID",
                table: "Images");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Images",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "Images",
                newName: "images");

            migrationBuilder.RenameIndex(
                name: "IX_Images_HotelID",
                table: "images",
                newName: "IX_images_HotelID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_images",
                table: "images",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_images_Hotels_HotelID",
                table: "images",
                column: "HotelID",
                principalTable: "Hotels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
