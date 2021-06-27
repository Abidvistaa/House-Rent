using Microsoft.EntityFrameworkCore.Migrations;

namespace HouseRent.Data.Migrations
{
    public partial class AddDetailOfFlatToDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DetailOfFlats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlatName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FlatId = table.Column<int>(type: "int", nullable: false),
                    RoomNumber = table.Column<int>(type: "int", nullable: false),
                    BathNumber = table.Column<int>(type: "int", nullable: false),
                    CarParking = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetailOfFlats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetailOfFlats_Flats_FlatId",
                        column: x => x.FlatId,
                        principalTable: "Flats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DetailOfFlats_FlatId",
                table: "DetailOfFlats",
                column: "FlatId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetailOfFlats");
        }
    }
}
