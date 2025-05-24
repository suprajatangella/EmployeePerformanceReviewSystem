using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeePerformanceReview.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixFeedbackTblCascadeIssue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_Employees_EmployeeId",
                table: "Feedbacks");

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_Employees_EmployeeId",
                table: "Feedbacks",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_Employees_EmployeeId",
                table: "Feedbacks");

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_Employees_EmployeeId",
                table: "Feedbacks",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
