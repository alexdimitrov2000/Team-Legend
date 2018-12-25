using Microsoft.EntityFrameworkCore.Migrations;

namespace TeamLegend.Data.Migrations
{
    public partial class AddedMatchIsPlayedAndUniqueFixtureRounds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPlayed",
                table: "Matches",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Fixtures_FixtureRound",
                table: "Fixtures",
                column: "FixtureRound",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Fixtures_FixtureRound",
                table: "Fixtures");

            migrationBuilder.DropColumn(
                name: "IsPlayed",
                table: "Matches");
        }
    }
}
