using Microsoft.EntityFrameworkCore.Migrations;

namespace BazarNetCore.Migrations
{
    public partial class ProdactType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "Prodacts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "Prodacts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Weight",
                table: "Prodacts",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Count",
                table: "Prodacts");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Prodacts");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "Prodacts");
        }
    }
}
