using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Roamer.Migrations
{
    /// <inheritdoc />
    public partial class MigPosts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PostContent2",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PostContent3",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PostHeading",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PostHeading2",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PostContent2",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "PostContent3",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "PostHeading",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "PostHeading2",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Comments");
        }
    }
}
