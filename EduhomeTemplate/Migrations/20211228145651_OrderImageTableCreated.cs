using Microsoft.EntityFrameworkCore.Migrations;

namespace EduhomeTemplate.Migrations
{
    public partial class OrderImageTableCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Teachers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "Teachers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "Teachers");
        }
    }
}
