using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiscontMarket.DAL.Migrations
{
    /// <inheritdoc />
    public partial class BrandsRename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BrandCategory_Brends_BrandID",
                table: "BrandCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Brends_BrandId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Brends",
                table: "Brends");

            migrationBuilder.RenameTable(
                name: "Brends",
                newName: "Brands");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Brands",
                table: "Brands",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BrandCategory_Brands_BrandID",
                table: "BrandCategory",
                column: "BrandID",
                principalTable: "Brands",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Brands_BrandId",
                table: "Products",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BrandCategory_Brands_BrandID",
                table: "BrandCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Brands_BrandId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Brands",
                table: "Brands");

            migrationBuilder.RenameTable(
                name: "Brands",
                newName: "Brends");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Brends",
                table: "Brends",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BrandCategory_Brends_BrandID",
                table: "BrandCategory",
                column: "BrandID",
                principalTable: "Brends",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Brends_BrandId",
                table: "Products",
                column: "BrandId",
                principalTable: "Brends",
                principalColumn: "ID");
        }
    }
}
