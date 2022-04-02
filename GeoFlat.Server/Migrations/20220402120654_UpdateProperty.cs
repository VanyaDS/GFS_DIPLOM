using Microsoft.EntityFrameworkCore.Migrations;

namespace GeoFlat.Server.Migrations
{
    public partial class UpdateProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "room_number",
                table: "Record");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "room_number",
                table: "Record",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
