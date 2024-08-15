using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MT.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addUserFieldForTags : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "tags",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "tags",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_tags_UserId",
                table: "tags",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_tags_users_UserId",
                table: "tags",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tags_users_UserId",
                table: "tags");

            migrationBuilder.DropIndex(
                name: "IX_tags_UserId",
                table: "tags");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "tags");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "tags",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);
        }
    }
}
