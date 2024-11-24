using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiscontMarket.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ProductFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductStatus",
                table: "Products",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "ProductAvailability",
                table: "Products",
                newName: "Availability");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Products",
                newName: "ProductStatus");

            migrationBuilder.RenameColumn(
                name: "Availability",
                table: "Products",
                newName: "ProductAvailability");
        }
    }
}
