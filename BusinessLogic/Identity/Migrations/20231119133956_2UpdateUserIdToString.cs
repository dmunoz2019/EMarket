using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessLogic.Identity.Migrations
{
    public partial class _2UpdateUserIdToString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "AddressId",
                table: "AspNetUsers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Address",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Address_UserId1",
                table: "Address",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Address_AspNetUsers_UserId1",
                table: "Address",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Address_AspNetUsers_UserId1",
                table: "Address");

            migrationBuilder.DropIndex(
                name: "IX_Address_UserId1",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Address");

            migrationBuilder.AlterColumn<int>(
                name: "AddressId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
