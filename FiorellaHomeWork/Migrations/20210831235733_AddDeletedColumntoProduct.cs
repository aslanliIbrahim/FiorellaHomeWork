using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FiorellaHomeWork.Migrations
{
    public partial class AddDeletedColumntoProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedTime",
                table: "Products",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletedTime",
                table: "categories",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedTime",
                table: "Products");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletedTime",
                table: "categories",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }
    }
}
