using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MT.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EditTypoInFieldNameInItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Usefull",
                table: "items",
                newName: "Useful");

            migrationBuilder.RenameColumn(
                name: "PossibleUsefull",
                table: "items",
                newName: "PossibleUseful");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "items",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "items",
                type: "character varying(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Useful",
                table: "items",
                newName: "Usefull");

            migrationBuilder.RenameColumn(
                name: "PossibleUseful",
                table: "items",
                newName: "PossibleUsefull");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "items",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "items",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(30)",
                oldMaxLength: 30,
                oldNullable: true);
        }
    }
}
