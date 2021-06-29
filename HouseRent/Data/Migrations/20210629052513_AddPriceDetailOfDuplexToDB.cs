using Microsoft.EntityFrameworkCore.Migrations;

namespace HouseRent.Data.Migrations
{
    public partial class AddPriceDetailOfDuplexToDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "DetailOfDuplexs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "DetailOfDuplexs");
        }
    }
}
