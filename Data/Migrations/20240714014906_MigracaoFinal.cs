using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class MigracaoFinal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContaCorrente_Pessoa_PessoaID",
                table: "ContaCorrente");

            migrationBuilder.DropColumn(
                name: "LimiteTotal",
                table: "ContaCorrente");

            migrationBuilder.DropColumn(
                name: "LimiteUtilizado",
                table: "ContaCorrente");

            migrationBuilder.DropColumn(
                name: "ValorAtraso",
                table: "ContaCorrente");

            migrationBuilder.RenameColumn(
                name: "NumeroCarteiraDestino",
                table: "Transferencia",
                newName: "ContaCorrenteDestinoID");

            migrationBuilder.AlterColumn<int>(
                name: "PessoaID",
                table: "ContaCorrente",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ContaCorrente_Pessoa_PessoaID",
                table: "ContaCorrente",
                column: "PessoaID",
                principalTable: "Pessoa",
                principalColumn: "PessoaID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContaCorrente_Pessoa_PessoaID",
                table: "ContaCorrente");

            migrationBuilder.RenameColumn(
                name: "ContaCorrenteDestinoID",
                table: "Transferencia",
                newName: "NumeroCarteiraDestino");

            migrationBuilder.AlterColumn<int>(
                name: "PessoaID",
                table: "ContaCorrente",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<decimal>(
                name: "LimiteTotal",
                table: "ContaCorrente",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "LimiteUtilizado",
                table: "ContaCorrente",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ValorAtraso",
                table: "ContaCorrente",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddForeignKey(
                name: "FK_ContaCorrente_Pessoa_PessoaID",
                table: "ContaCorrente",
                column: "PessoaID",
                principalTable: "Pessoa",
                principalColumn: "PessoaID");
        }
    }
}
