using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApiR.Migrations
{
    public partial class UpdateinCategoryImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Categories",
                nullable: false,
                oldClrType: typeof(byte[]));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "Image",
                table: "Categories",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
