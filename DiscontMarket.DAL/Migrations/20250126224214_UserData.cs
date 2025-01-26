using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiscontMarket.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UserData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "AspNetUsers",
                type: "varchar",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClientsTelegram",
                table: "AspNetUsers",
                type: "varchar",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClientsVk",
                table: "AspNetUsers",
                type: "varchar",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClientsWhatsapp",
                table: "AspNetUsers",
                type: "varchar",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactInfoText",
                table: "AspNetUsers",
                type: "varchar",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HrefAdress",
                table: "AspNetUsers",
                type: "varchar",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HrefmapAdress",
                table: "AspNetUsers",
                type: "varchar",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReturnsText",
                table: "AspNetUsers",
                type: "varchar",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SupportContacts",
                table: "AspNetUsers",
                type: "varchar",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TextAdress",
                table: "AspNetUsers",
                type: "varchar",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkTimeInfo",
                table: "AspNetUsers",
                type: "varchar",
                maxLength: 50,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientsTelegram",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ClientsVk",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ClientsWhatsapp",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ContactInfoText",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "HrefAdress",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "HrefmapAdress",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ReturnsText",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SupportContacts",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TextAdress",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "WorkTimeInfo",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "AspNetUsers",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldMaxLength: 50,
                oldNullable: true);
        }
    }
}
