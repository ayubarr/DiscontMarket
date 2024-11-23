using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DiscontMarket.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Init3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Brends_ID",
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
                name: "IX_Products_BrendId",
                table: "Products",
                column: "BrendId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Brends_BrendId",
                table: "Products",
                column: "BrendId",
                principalTable: "Brends",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Brends_BrendId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_BrendId",
                table: "Products");

            migrationBuilder.AlterColumn<long>(
                name: "ID",
                table: "Products",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Brends_ID",
                table: "Products",
                column: "ID",
                principalTable: "Brends",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
