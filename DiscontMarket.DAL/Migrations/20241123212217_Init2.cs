using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DiscontMarket.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Brends_Products_ProductID",
                table: "Brends");

            migrationBuilder.DropIndex(
                name: "IX_Brends_ProductID",
                table: "Brends");

            migrationBuilder.AlterColumn<long>(
                name: "ID",
                table: "Products",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<long>(
                name: "BrendId",
                table: "Products",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Brends_ID",
                table: "Products",
                column: "ID",
                principalTable: "Brends",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Brends_ID",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "BrendId",
                table: "Products");

            migrationBuilder.AlterColumn<long>(
                name: "ID",
                table: "Products",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.CreateIndex(
                name: "IX_Brends_ProductID",
                table: "Brends",
                column: "ProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_Brends_Products_ProductID",
                table: "Brends",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ID");
        }
    }
}
