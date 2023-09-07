using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogPost.Infrustructure.Migrations
{
    /// <inheritdoc />
    public partial class setOnDeleteOnCommentAndArticle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Authors_AuthorId1",
                table: "Articles");

            migrationBuilder.DropForeignKey(
                name: "FK_Authors_AspNetUsers_ApplicationUserId",
                table: "Authors");

            migrationBuilder.DropForeignKey(
                name: "FK_UserLikes_Articles_ArticleId",
                table: "UserLikes");

            migrationBuilder.DropTable(
                name: "AuthorSkills");

            migrationBuilder.DropTable(
                name: "Skill");

            migrationBuilder.DropIndex(
                name: "IX_Articles_AuthorId1",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "AuthorId1",
                table: "Articles");

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_AspNetUsers_ApplicationUserId",
                table: "Authors",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserLikes_Articles_ArticleId",
                table: "UserLikes",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "ArticleID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Authors_AspNetUsers_ApplicationUserId",
                table: "Authors");

            migrationBuilder.DropForeignKey(
                name: "FK_UserLikes_Articles_ArticleId",
                table: "UserLikes");

            migrationBuilder.AddColumn<Guid>(
                name: "AuthorId1",
                table: "Articles",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Skill",
                columns: table => new
                {
                    SkillId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SkillName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skill", x => x.SkillId);
                });

            migrationBuilder.CreateTable(
                name: "AuthorSkills",
                columns: table => new
                {
                    AuthorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SkillId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorSkills", x => new { x.AuthorId, x.SkillId });
                    table.ForeignKey(
                        name: "FK_AuthorSkills_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "AuthorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuthorSkills_Skill_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skill",
                        principalColumn: "SkillId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Articles_AuthorId1",
                table: "Articles",
                column: "AuthorId1");

            migrationBuilder.CreateIndex(
                name: "IX_AuthorSkills_SkillId",
                table: "AuthorSkills",
                column: "SkillId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Authors_AuthorId1",
                table: "Articles",
                column: "AuthorId1",
                principalTable: "Authors",
                principalColumn: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_AspNetUsers_ApplicationUserId",
                table: "Authors",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserLikes_Articles_ArticleId",
                table: "UserLikes",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "ArticleID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
