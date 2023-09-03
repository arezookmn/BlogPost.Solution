using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogPost.Infrustructure.Migrations
{
    /// <inheritdoc />
    public partial class AddFulentApiEditForUserLikeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserLikes_AspNetUsers_UserId",
                table: "UserLikes");

            migrationBuilder.AddForeignKey(
                name: "FK_UserLikes_AspNetUsers_UserId",
                table: "UserLikes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserLikes_AspNetUsers_UserId",
                table: "UserLikes");

            migrationBuilder.AddForeignKey(
                name: "FK_UserLikes_AspNetUsers_UserId",
                table: "UserLikes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
