using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TKDprogress.Migrations
{
    /// <inheritdoc />
    public partial class Third : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserCategories_AspNetUsers_UserId1",
                table: "UserCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTuls_AspNetUsers_UserId1",
                table: "UserTuls");

            migrationBuilder.DropIndex(
                name: "IX_UserTuls_UserId1",
                table: "UserTuls");

            migrationBuilder.DropIndex(
                name: "IX_UserCategories_UserId1",
                table: "UserCategories");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "UserTuls");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "UserCategories");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserTuls",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserCategories",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_UserTuls_UserId",
                table: "UserTuls",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCategories_UserId",
                table: "UserCategories",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserCategories_AspNetUsers_UserId",
                table: "UserCategories",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserTuls_AspNetUsers_UserId",
                table: "UserTuls",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserCategories_AspNetUsers_UserId",
                table: "UserCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTuls_AspNetUsers_UserId",
                table: "UserTuls");

            migrationBuilder.DropIndex(
                name: "IX_UserTuls_UserId",
                table: "UserTuls");

            migrationBuilder.DropIndex(
                name: "IX_UserCategories_UserId",
                table: "UserCategories");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "UserTuls",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "UserTuls",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "UserCategories",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "UserCategories",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_UserTuls_UserId1",
                table: "UserTuls",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_UserCategories_UserId1",
                table: "UserCategories",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_UserCategories_AspNetUsers_UserId1",
                table: "UserCategories",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserTuls_AspNetUsers_UserId1",
                table: "UserTuls",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
