using Microsoft.EntityFrameworkCore.Migrations;

namespace TeamLegend.Data.Migrations
{
    public partial class AddedFixturesToLeagues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Fixtures_FixtureId",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Teams_HomeTeamId",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Fixtures_FixtureRound",
                table: "Fixtures");

            migrationBuilder.AddColumn<string>(
                name: "LeagueId",
                table: "Fixtures",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Fixtures_LeagueId",
                table: "Fixtures",
                column: "LeagueId");

            migrationBuilder.CreateIndex(
                name: "IX_Fixtures_FixtureRound_LeagueId",
                table: "Fixtures",
                columns: new[] { "FixtureRound", "LeagueId" },
                unique: true,
                filter: "[LeagueId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Fixtures_Leagues_LeagueId",
                table: "Fixtures",
                column: "LeagueId",
                principalTable: "Leagues",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Fixtures_FixtureId",
                table: "Matches",
                column: "FixtureId",
                principalTable: "Fixtures",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Teams_HomeTeamId",
                table: "Matches",
                column: "HomeTeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fixtures_Leagues_LeagueId",
                table: "Fixtures");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Fixtures_FixtureId",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Teams_HomeTeamId",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Fixtures_LeagueId",
                table: "Fixtures");

            migrationBuilder.DropIndex(
                name: "IX_Fixtures_FixtureRound_LeagueId",
                table: "Fixtures");

            migrationBuilder.DropColumn(
                name: "LeagueId",
                table: "Fixtures");

            migrationBuilder.CreateIndex(
                name: "IX_Fixtures_FixtureRound",
                table: "Fixtures",
                column: "FixtureRound",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Fixtures_FixtureId",
                table: "Matches",
                column: "FixtureId",
                principalTable: "Fixtures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Teams_HomeTeamId",
                table: "Matches",
                column: "HomeTeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
