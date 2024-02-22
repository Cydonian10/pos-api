using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PuntoVenta.Migrations
{
    /// <inheritdoc />
    public partial class AddCashregisteridSale : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CashRegisterId",
                table: "Sales",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sales_CashRegisterId",
                table: "Sales",
                column: "CashRegisterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_CashRegisters_CashRegisterId",
                table: "Sales",
                column: "CashRegisterId",
                principalTable: "CashRegisters",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sales_CashRegisters_CashRegisterId",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Sales_CashRegisterId",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "CashRegisterId",
                table: "Sales");
        }
    }
}
