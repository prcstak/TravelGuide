using Microsoft.EntityFrameworkCore.Migrations;

namespace DataBase.Migrations
{
    public partial class FixAdvertisement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Advertisement",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Duration",
                table: "Advertisement",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Advertisement");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Advertisement");
        }
    }
}
