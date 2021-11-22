using Microsoft.EntityFrameworkCore.Migrations;

namespace Music_Portal.Domain.Core.Migrations
{
    public partial class AddSimilarArtistsRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SimilarArtistsToArtist",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SimilarArtistId = table.Column<int>(type: "int", nullable: false),
                    SimilarToId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SimilarArtistsToArtist", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SimilarArtistsToArtist");
        }
    }
}
