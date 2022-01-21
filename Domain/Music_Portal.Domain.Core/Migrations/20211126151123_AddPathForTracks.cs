using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Music_Portal.Domain.Core.Migrations
{
    public partial class AddPathForTracks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PathToFile",
                table: "Tracks",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PathToFile",
                table: "Tracks");
        }
    }
}
