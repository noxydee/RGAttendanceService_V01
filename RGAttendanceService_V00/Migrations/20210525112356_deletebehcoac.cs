using Microsoft.EntityFrameworkCore.Migrations;

namespace RGAttendanceService_V00.Migrations
{
    public partial class deletebehcoac : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Group_Coach_CoachId",
                table: "Group");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Group_Coach_CoachId",
                table: "Group",
                column: "CoachId",
                principalTable: "Coach",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Group_Coach_CoachId",
                table: "Group");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Group_Coach_CoachId",
                table: "Group",
                column: "CoachId",
                principalTable: "Coach",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
