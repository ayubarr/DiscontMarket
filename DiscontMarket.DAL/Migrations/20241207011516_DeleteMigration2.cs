using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiscontMarket.DAL.Migrations
{
    /// <inheritdoc />
    public partial class DeleteMigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductAttribute_Products_ProductID",
                table: "ProductAttribute");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTag_Products_ProductID",
                table: "ProductTag");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAttribute_Products_ProductID",
                table: "ProductAttribute",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTag_Products_ProductID",
                table: "ProductTag",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductAttribute_Products_ProductID",
                table: "ProductAttribute");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTag_Products_ProductID",
                table: "ProductTag");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAttribute_Products_ProductID",
                table: "ProductAttribute",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTag_Products_ProductID",
                table: "ProductTag",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
