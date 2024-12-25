using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiscontMarket.DAL.Migrations
{
    /// <inheritdoc />
    public partial class OrderInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalCost",
                table: "Orders");

            migrationBuilder.AddColumn<string>(
                name: "ClientsPhoneNumber",
                table: "Orders",
                type: "varchar",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientsPhoneNumber",
                table: "Orders");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalCost",
                table: "Orders",
                type: "numeric(15,2)",
                precision: 15,
                scale: 2,
                nullable: false,
                defaultValue: 0m);
        }
    }
}
