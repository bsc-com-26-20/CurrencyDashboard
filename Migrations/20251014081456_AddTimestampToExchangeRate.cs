using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CurrencyDashboard.Migrations
{
    /// <inheritdoc />
    public partial class AddTimestampToExchangeRate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Date",
                table: "ExchangeRates",
                newName: "Timestamp");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Timestamp",
                table: "ExchangeRates",
                newName: "Date");
        }
    }
}
