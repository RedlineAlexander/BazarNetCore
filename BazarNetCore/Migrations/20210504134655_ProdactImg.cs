using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BazarNetCore.Migrations
{
    public partial class ProdactImg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Prodacts",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageMimeTypeOfData",
                table: "Prodacts",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Prodacts");

            migrationBuilder.DropColumn(
                name: "ImageMimeTypeOfData",
                table: "Prodacts");
        }
    }
}
