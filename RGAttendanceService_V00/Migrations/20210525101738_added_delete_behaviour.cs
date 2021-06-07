using Microsoft.EntityFrameworkCore.Migrations;

namespace RGAttendanceService_V00.Migrations
{
    public partial class added_delete_behaviour : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Participant_Group_GroupId",
                table: "Participant");

            migrationBuilder.AddForeignKey(
                name: "FK_Participant_Group_GroupId",
                table: "Participant",
                column: "GroupId",
                principalTable: "Group",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Participant_Group_GroupId",
                table: "Participant");

            migrationBuilder.AddForeignKey(
                name: "FK_Participant_Group_GroupId",
                table: "Participant",
                column: "GroupId",
                principalTable: "Group",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
