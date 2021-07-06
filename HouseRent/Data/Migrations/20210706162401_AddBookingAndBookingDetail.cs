using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HouseRent.Data.Migrations
{
    public partial class AddBookingAndBookingDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "bookingDetail2s",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingId = table.Column<int>(type: "int", nullable: false),
                    DetailOfDuplexId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bookingDetail2s", x => x.Id);
                    table.ForeignKey(
                        name: "FK_bookingDetail2s_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_bookingDetail2s_DetailOfDuplexs_DetailOfDuplexId",
                        column: x => x.DetailOfDuplexId,
                        principalTable: "DetailOfDuplexs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookingDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingId = table.Column<int>(type: "int", nullable: false),
                    DetailOfFlatId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookingDetails_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookingDetails_DetailOfFlats_DetailOfFlatId",
                        column: x => x.DetailOfFlatId,
                        principalTable: "DetailOfFlats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DetailOfFlats_BookingId",
                table: "DetailOfFlats",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_DetailOfDuplexs_BookingId",
                table: "DetailOfDuplexs",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_bookingDetail2s_BookingId",
                table: "bookingDetail2s",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_bookingDetail2s_DetailOfDuplexId",
                table: "bookingDetail2s",
                column: "DetailOfDuplexId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingDetails_BookingId",
                table: "BookingDetails",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingDetails_DetailOfFlatId",
                table: "BookingDetails",
                column: "DetailOfFlatId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetailOfDuplexs_Bookings_BookingId",
                table: "DetailOfDuplexs");

            migrationBuilder.DropForeignKey(
                name: "FK_DetailOfFlats_Bookings_BookingId",
                table: "DetailOfFlats");

            migrationBuilder.DropTable(
                name: "bookingDetail2s");

            migrationBuilder.DropTable(
                name: "BookingDetails");

            migrationBuilder.DropTable(
                name: "Bookings");

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
    }
}
