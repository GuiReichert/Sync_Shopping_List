using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lista_De_Compras.Migrations
{
    /// <inheritdoc />
    public partial class adicionando_selecionado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Selecionado",
                table: "Produtos",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Selecionado",
                table: "Produtos");
        }
    }
}
