using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiscontMarket.DAL.Migrations
{
    /// <inheritdoc />
    public partial class BrandFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Brends_BrendId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "BrendId",
                table: "Products",
                newName: "BrandId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_BrendId",
                table: "Products",
                newName: "IX_Products_BrandId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Brends_BrandId",
                table: "Products",
                column: "BrandId",
                principalTable: "Brends",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Brends_BrandId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "BrandId",
                table: "Products",
                newName: "BrendId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_BrandId",
                table: "Products",
                newName: "IX_Products_BrendId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Brends_BrendId",
                table: "Products",
                column: "BrendId",
                principalTable: "Brends",
                principalColumn: "ID");
        }
    }
}
