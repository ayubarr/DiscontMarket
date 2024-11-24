using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiscontMarket.DAL.Migrations
{
    /// <inheritdoc />
    public partial class BrandCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_AspNetUsers_UserID",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_UserID",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Categories");

            migrationBuilder.CreateTable(
                name: "BrandCategory",
                columns: table => new
                {
                    CategoryID = table.Column<long>(type: "bigint", nullable: false),
                    BrandID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrandCategory", x => new { x.BrandID, x.CategoryID });
                    table.ForeignKey(
                        name: "FK_BrandCategory_Brends_BrandID",
                        column: x => x.BrandID,
                        principalTable: "Brends",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BrandCategory_Categories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Categories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BrandCategory_CategoryID",
                table: "BrandCategory",
                column: "CategoryID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BrandCategory");

            migrationBuilder.AddColumn<long>(
                name: "UserID",
                table: "Categories",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_UserID",
                table: "Categories",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_AspNetUsers_UserID",
                table: "Categories",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
