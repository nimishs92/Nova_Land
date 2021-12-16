using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Nova_Land.Migrations
{
    public partial class categorydescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "OrderLineItems");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "OrderLineItems");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "OrderLineItems");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Categories");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "OrderLineItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "OrderLineItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "OrderLineItems",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
