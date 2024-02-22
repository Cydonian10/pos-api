﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PuntoVenta.Migrations
{
    /// <inheritdoc />
    public partial class UnitPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "UnitPrice",
                table: "SaleDetails",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnitPrice",
                table: "SaleDetails");
        }
    }
}