using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PuntoVenta.Migrations
{
    /// <inheritdoc />
    public partial class pathdocumentproduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "HistoryPriceProduct",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalCash",
                table: "CashRegisters",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "InitialCash",
                table: "CashRegisters",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "HistoryPriceProduct");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalCash",
                table: "CashRegisters",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "InitialCash",
                table: "CashRegisters",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }
    }
}
