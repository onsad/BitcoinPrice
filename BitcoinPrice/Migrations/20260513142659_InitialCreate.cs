using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BitcoinPrice.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BitcoinRates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PriceInEur = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EurToCzkRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PriceCzk = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DownloadedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BitcoinRates", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BitcoinRates");
        }
    }
}
