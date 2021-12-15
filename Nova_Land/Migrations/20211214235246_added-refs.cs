using Microsoft.EntityFrameworkCore.Migrations;

namespace Nova_Land.Migrations
{
    public partial class addedrefs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderLineItems_Orders_OrderID",
                table: "OrderLineItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderLineItems_OrderID",
                table: "OrderLineItems");

            migrationBuilder.DropColumn(
                name: "OrderID",
                table: "OrderLineItems");

            migrationBuilder.AddColumn<int>(
                name: "OrderRef",
                table: "OrderLineItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OrderLineItems_OrderRef",
                table: "OrderLineItems",
                column: "OrderRef");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderLineItems_Orders_OrderRef",
                table: "OrderLineItems",
                column: "OrderRef",
                principalTable: "Orders",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderLineItems_Orders_OrderRef",
                table: "OrderLineItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderLineItems_OrderRef",
                table: "OrderLineItems");

            migrationBuilder.DropColumn(
                name: "OrderRef",
                table: "OrderLineItems");

            migrationBuilder.AddColumn<int>(
                name: "OrderID",
                table: "OrderLineItems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderLineItems_OrderID",
                table: "OrderLineItems",
                column: "OrderID");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderLineItems_Orders_OrderID",
                table: "OrderLineItems",
                column: "OrderID",
                principalTable: "Orders",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
