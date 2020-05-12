using Microsoft.EntityFrameworkCore.Migrations;

namespace DateMate.API.Migrations
{
    public partial class addedpublicid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PublicID",
                table: "Photos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PublicID",
                table: "Photos");
        }
    }
}
