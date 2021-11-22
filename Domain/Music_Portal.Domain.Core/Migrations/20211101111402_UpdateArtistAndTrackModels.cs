using Microsoft.EntityFrameworkCore.Migrations;

namespace Music_Portal.Domain.Core.Migrations
{
    public partial class UpdateArtistAndTrackModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Url",
                table: "Tracks");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "Artists");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Tracks",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Artists",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);
        }
    }
}
