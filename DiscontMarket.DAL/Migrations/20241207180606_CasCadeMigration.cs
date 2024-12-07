using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiscontMarket.DAL.Migrations
{
    /// <inheritdoc />
    public partial class CasCadeMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductAttribute_Attributes_AttributeID",
                table: "ProductAttribute");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAttribute_Attributes_AttributeID",
                table: "ProductAttribute",
                column: "AttributeID",
                principalTable: "Attributes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductAttribute_Attributes_AttributeID",
                table: "ProductAttribute");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAttribute_Attributes_AttributeID",
                table: "ProductAttribute",
                column: "AttributeID",
                principalTable: "Attributes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
