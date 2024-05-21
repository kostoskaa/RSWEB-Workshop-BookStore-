using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication3.Migrations
{
    /// <inheritdoc />
    public partial class userbooks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BookStoreUserId",
                table: "UserBooks",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserBooks_BookStoreUserId",
                table: "UserBooks",
                column: "BookStoreUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserBooks_AspNetUsers_BookStoreUserId",
                table: "UserBooks",
                column: "BookStoreUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserBooks_AspNetUsers_BookStoreUserId",
                table: "UserBooks");

            migrationBuilder.DropIndex(
                name: "IX_UserBooks_BookStoreUserId",
                table: "UserBooks");

            migrationBuilder.DropColumn(
                name: "BookStoreUserId",
                table: "UserBooks");
        }
    }
}
