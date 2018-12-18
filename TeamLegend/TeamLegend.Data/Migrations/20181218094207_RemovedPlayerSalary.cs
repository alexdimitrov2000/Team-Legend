using Microsoft.EntityFrameworkCore.Migrations;

namespace TeamLegend.Data.Migrations
{
    public partial class RemovedPlayerSalary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Salary",
                table: "Players");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Salary",
                table: "Players",
                type: "decimal(18, 2)",
                nullable: true);
        }
    }
}
