using Microsoft.EntityFrameworkCore.Migrations;

namespace HouseRent.Data.Migrations
{
    public partial class AddModificationBooking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetailOfDuplexs_Bookings_BookingId",
                table: "DetailOfDuplexs");

            migrationBuilder.DropForeignKey(
                name: "FK_DetailOfFlats_Bookings_BookingId",
                table: "DetailOfFlats");

            migrationBuilder.DropIndex(
                name: "IX_DetailOfFlats_BookingId",
                table: "DetailOfFlats");

            migrationBuilder.DropIndex(
                name: "IX_DetailOfDuplexs_BookingId",
                table: "DetailOfDuplexs");

            migrationBuilder.DropColumn(
                name: "BookingId",
                table: "DetailOfFlats");

            migrationBuilder.DropColumn(
                name: "BookingId",
                table: "DetailOfDuplexs");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BookingId",
                table: "DetailOfFlats",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BookingId",
                table: "DetailOfDuplexs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DetailOfFlats_BookingId",
                table: "DetailOfFlats",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_DetailOfDuplexs_BookingId",
                table: "DetailOfDuplexs",
                column: "BookingId");

            migrationBuilder.AddForeignKey(
                name: "FK_DetailOfDuplexs_Bookings_BookingId",
                table: "DetailOfDuplexs",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DetailOfFlats_Bookings_BookingId",
                table: "DetailOfFlats",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
