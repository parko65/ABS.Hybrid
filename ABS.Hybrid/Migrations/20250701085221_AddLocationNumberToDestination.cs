using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ABS.Hybrid.Migrations
{
    /// <inheritdoc />
    public partial class AddLocationNumberToDestination : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LocationNumber",
                table: "Destinations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Destinations",
                keyColumn: "DestinationId",
                keyValue: 1,
                column: "LocationNumber",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Destinations",
                keyColumn: "DestinationId",
                keyValue: 2,
                column: "LocationNumber",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Destinations",
                keyColumn: "DestinationId",
                keyValue: 3,
                column: "LocationNumber",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Destinations",
                keyColumn: "DestinationId",
                keyValue: 4,
                column: "LocationNumber",
                value: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LocationNumber",
                table: "Destinations");
        }
    }
}
