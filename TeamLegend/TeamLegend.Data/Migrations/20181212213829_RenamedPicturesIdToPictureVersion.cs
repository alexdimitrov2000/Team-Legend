using Microsoft.EntityFrameworkCore.Migrations;

namespace TeamLegend.Data.Migrations
{
    public partial class RenamedPicturesIdToPictureVersion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BadgeId",
                table: "Teams",
                newName: "BadgeVersion");

            migrationBuilder.RenameColumn(
                name: "StadiumPictureId",
                table: "Stadiums",
                newName: "StadiumPictureVersion");

            migrationBuilder.RenameColumn(
                name: "PlayerPictureId",
                table: "Players",
                newName: "PlayerPictureVersion");

            migrationBuilder.RenameColumn(
                name: "ProfilePictureId",
                table: "AspNetUsers",
                newName: "ProfilePictureVersion");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BadgeVersion",
                table: "Teams",
                newName: "BadgeId");

            migrationBuilder.RenameColumn(
                name: "StadiumPictureVersion",
                table: "Stadiums",
                newName: "StadiumPictureId");

            migrationBuilder.RenameColumn(
                name: "PlayerPictureVersion",
                table: "Players",
                newName: "PlayerPictureId");

            migrationBuilder.RenameColumn(
                name: "ProfilePictureVersion",
                table: "AspNetUsers",
                newName: "ProfilePictureId");
        }
    }
}
