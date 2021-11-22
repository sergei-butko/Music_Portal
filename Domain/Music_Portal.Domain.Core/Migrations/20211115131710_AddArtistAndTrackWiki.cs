using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Music_Portal.Domain.Core.Migrations
{
    public partial class AddArtistAndTrackWiki : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Summary",
                table: "Tracks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Wiki",
                table: "Tracks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Summary",
                table: "Artists",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Summary",
                table: "Tracks");

            migrationBuilder.DropColumn(
                name: "Wiki",
                table: "Tracks");

            migrationBuilder.DropColumn(
                name: "Summary",
                table: "Artists");
        }
    }
}
