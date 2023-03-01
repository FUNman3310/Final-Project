using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_commerce.Migrations
{
    public partial class MyAccountEdited : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "myAccounts",
                newName: "Image");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "myAccounts",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_myAccounts_AppUserId",
                table: "myAccounts",
                column: "AppUserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_myAccounts_AspNetUsers_AppUserId",
                table: "myAccounts",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_myAccounts_AspNetUsers_AppUserId",
                table: "myAccounts");

            migrationBuilder.DropIndex(
                name: "IX_myAccounts_AppUserId",
                table: "myAccounts");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "myAccounts");

            migrationBuilder.RenameColumn(
                name: "Image",
                table: "myAccounts",
                newName: "UserName");
        }
    }
}
