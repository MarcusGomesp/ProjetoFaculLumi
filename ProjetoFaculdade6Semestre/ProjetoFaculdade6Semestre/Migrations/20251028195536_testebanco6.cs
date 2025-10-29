using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoFaculdade6Semestre.Migrations
{
    /// <inheritdoc />
    public partial class testebanco6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cvs_Users_UserId",
                table: "Cvs");

            migrationBuilder.DropForeignKey(
                name: "FK_Roles_Cvs_CvId",
                table: "Roles");

            migrationBuilder.AddForeignKey(
                name: "FK_Cvs_Users_UserId",
                table: "Cvs",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_Cvs_CvId",
                table: "Roles",
                column: "CvId",
                principalTable: "Cvs",
                principalColumn: "CvId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cvs_Users_UserId",
                table: "Cvs");

            migrationBuilder.DropForeignKey(
                name: "FK_Roles_Cvs_CvId",
                table: "Roles");

            migrationBuilder.AddForeignKey(
                name: "FK_Cvs_Users_UserId",
                table: "Cvs",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_Cvs_CvId",
                table: "Roles",
                column: "CvId",
                principalTable: "Cvs",
                principalColumn: "CvId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
