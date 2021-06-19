using Microsoft.EntityFrameworkCore.Migrations;

namespace RGAttendanceService_V00.Migrations
{
    public partial class onemanycoachgroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Group_CoachId",
                table: "Group");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Coach");

            migrationBuilder.CreateIndex(
                name: "IX_Group_CoachId",
                table: "Group",
                column: "CoachId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Group_CoachId",
                table: "Group");

            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "Coach",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Group_CoachId",
                table: "Group",
                column: "CoachId",
                unique: true,
                filter: "[CoachId] IS NOT NULL");
        }
    }
}
