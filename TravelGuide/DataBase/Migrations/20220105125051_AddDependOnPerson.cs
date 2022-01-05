using Microsoft.EntityFrameworkCore.Migrations;

namespace DataBase.Migrations
{
    public partial class AddDependOnPerson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PersonId",
                table: "Advertisement",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Advertisement_PersonId",
                table: "Advertisement",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Advertisement_Person_PersonId",
                table: "Advertisement",
                column: "PersonId",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Advertisement_Person_PersonId",
                table: "Advertisement");

            migrationBuilder.DropIndex(
                name: "IX_Advertisement_PersonId",
                table: "Advertisement");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "Advertisement");
        }
    }
}
