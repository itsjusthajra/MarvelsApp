using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarvelsApp.Migrations
{
    /// <inheritdoc />
    public partial class addedmoremodels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Affiliations",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "Powers",
                table: "Characters");

            migrationBuilder.RenameColumn(
                name: "Category",
                table: "Characters",
                newName: "CategoryId");

            migrationBuilder.CreateTable(
                name: "Affiliations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Affiliations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Powers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Powers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CharacterAffiliations",
                columns: table => new
                {
                    CharacterId = table.Column<int>(type: "int", nullable: false),
                    AffiliationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterAffiliations", x => new { x.CharacterId, x.AffiliationId });
                    table.ForeignKey(
                        name: "FK_CharacterAffiliations_Affiliations_AffiliationId",
                        column: x => x.AffiliationId,
                        principalTable: "Affiliations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterAffiliations_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CharacterPowers",
                columns: table => new
                {
                    CharacterId = table.Column<int>(type: "int", nullable: false),
                    PowerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterPowers", x => new { x.CharacterId, x.PowerId });
                    table.ForeignKey(
                        name: "FK_CharacterPowers_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterPowers_Powers_PowerId",
                        column: x => x.PowerId,
                        principalTable: "Powers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Characters_CategoryId",
                table: "Characters",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterAffiliations_AffiliationId",
                table: "CharacterAffiliations",
                column: "AffiliationId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterPowers_PowerId",
                table: "CharacterPowers",
                column: "PowerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Categories_CategoryId",
                table: "Characters",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Categories_CategoryId",
                table: "Characters");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "CharacterAffiliations");

            migrationBuilder.DropTable(
                name: "CharacterPowers");

            migrationBuilder.DropTable(
                name: "Affiliations");

            migrationBuilder.DropTable(
                name: "Powers");

            migrationBuilder.DropIndex(
                name: "IX_Characters_CategoryId",
                table: "Characters");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Characters",
                newName: "Category");

            migrationBuilder.AddColumn<string>(
                name: "Affiliations",
                table: "Characters",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Powers",
                table: "Characters",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
