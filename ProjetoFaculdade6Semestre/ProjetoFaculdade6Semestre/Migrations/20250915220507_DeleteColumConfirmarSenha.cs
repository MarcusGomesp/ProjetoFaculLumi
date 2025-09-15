using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoFaculdade6Semestre.Migrations
{
    /// <inheritdoc />
    public partial class DeleteColumConfirmarSenha : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConfirmarSenha",
                table: "Cadastros");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ConfirmarSenha",
                table: "Cadastros",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
