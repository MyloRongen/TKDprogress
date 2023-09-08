using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TKDprogress.Migrations
{
    /// <inheritdoc />
    public partial class Second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Terminologies_Categories_CategoryId",
                table: "Terminologies");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "UserCategories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Terminologies",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Terminologies_Categories_CategoryId",
                table: "Terminologies",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Terminologies_Categories_CategoryId",
                table: "Terminologies");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "UserCategories");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Terminologies",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Terminologies_Categories_CategoryId",
                table: "Terminologies",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }
    }
}
