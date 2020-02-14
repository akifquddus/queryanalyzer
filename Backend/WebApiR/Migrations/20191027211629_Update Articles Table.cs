using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApiR.Migrations
{
    public partial class UpdateArticlesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Article",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "Image",
                table: "Article",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
