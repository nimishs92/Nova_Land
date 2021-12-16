using Microsoft.EntityFrameworkCore.Migrations;

namespace Nova_Land.Migrations
{
    public partial class changestopayments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PaymentType",
                table: "Payments",
                newName: "paymenttype");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreditCardHolderName",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Exp_dt",
                table: "Payments",
                type: "nvarchar(7)",
                maxLength: 7,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Number",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "St_Address",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Zip",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "cvv",
                table: "Payments",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "CreditCardHolderName",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "Exp_dt",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "St_Address",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "Zip",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "cvv",
                table: "Payments");

            migrationBuilder.RenameColumn(
                name: "paymenttype",
                table: "Payments",
                newName: "PaymentType");
        }
    }
}
