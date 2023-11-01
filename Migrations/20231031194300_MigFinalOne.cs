using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Roamer.Migrations
{
    /// <inheritdoc />
    public partial class MigFinalOne : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubCategories");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.CreateTable(
                name: "Places",
                columns: table => new
                {
                    PlaceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlaceName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlaceDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlaceImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlaceHeading1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlaceHeading2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlaceContent1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlaceContent2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlaceContent3 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Places", x => x.PlaceId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Places");

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "SubCategories",
                columns: table => new
                {
                    SubCategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    SubCategoryContent1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubCategoryContent2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubCategoryContent3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubCategoryHeading1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubCategoryHeading2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubCategoryImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubCategoryName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCategories", x => x.SubCategoryId);
                    table.ForeignKey(
                        name: "FK_SubCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubCategories_CategoryId",
                table: "SubCategories",
                column: "CategoryId");
        }
    }
}
