using Microsoft.EntityFrameworkCore.Migrations;

namespace EventBee.Migrations
{
    public partial class place : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Events",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Events",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "Events",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Events");
        }
    }
}
