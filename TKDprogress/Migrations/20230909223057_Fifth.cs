using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TKDprogress.Migrations
{
    /// <inheritdoc />
    public partial class Fifth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserTuls_TulDto_TulId",
                table: "UserTuls");

            migrationBuilder.DropTable(
                name: "MovementDto");

            migrationBuilder.DropTable(
                name: "TulDto");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTuls_Tuls_TulId",
                table: "UserTuls",
                column: "TulId",
                principalTable: "Tuls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserTuls_Tuls_TulId",
                table: "UserTuls");

            migrationBuilder.CreateTable(
                name: "TulDto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TulDto", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MovementDto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ImageUrl = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TulDtoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovementDto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovementDto_TulDto_TulDtoId",
                        column: x => x.TulDtoId,
                        principalTable: "TulDto",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_MovementDto_TulDtoId",
                table: "MovementDto",
                column: "TulDtoId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTuls_TulDto_TulId",
                table: "UserTuls",
                column: "TulId",
                principalTable: "TulDto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
