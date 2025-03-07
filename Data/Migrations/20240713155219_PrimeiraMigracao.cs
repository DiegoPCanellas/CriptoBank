using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class PrimeiraMigracao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pessoa",
                columns: table => new
                {
                    PessoaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Senha = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Endereco = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CPFCNPJ = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContaCorrenteID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pessoa", x => x.PessoaID);
                });

            migrationBuilder.CreateTable(
                name: "TransacaoCripto",
                columns: table => new
                {
                    TransacaoCriptoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataReferencia = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ValorReal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ValorCripto = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransacaoCripto", x => x.TransacaoCriptoID);
                });

            migrationBuilder.CreateTable(
                name: "ContaCorrente",
                columns: table => new
                {
                    ContaCorrenteID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Saldo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LimiteTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LimiteUtilizado = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ValorAtraso = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PessoaID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContaCorrente", x => x.ContaCorrenteID);
                    table.ForeignKey(
                        name: "FK_ContaCorrente_Pessoa_PessoaID",
                        column: x => x.PessoaID,
                        principalTable: "Pessoa",
                        principalColumn: "PessoaID");
                });

            migrationBuilder.CreateTable(
                name: "Cartao",
                columns: table => new
                {
                    CartaoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Senha = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Numero = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContaCorrenteID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cartao", x => x.CartaoID);
                    table.ForeignKey(
                        name: "FK_Cartao_ContaCorrente_ContaCorrenteID",
                        column: x => x.ContaCorrenteID,
                        principalTable: "ContaCorrente",
                        principalColumn: "ContaCorrenteID");
                });

            migrationBuilder.CreateTable(
                name: "Deposito",
                columns: table => new
                {
                    DepositoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransacaoID = table.Column<int>(type: "int", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransacaoCriptoID = table.Column<int>(type: "int", nullable: false),
                    TipoID = table.Column<byte>(type: "tinyint", nullable: false),
                    ContaCorrenteID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deposito", x => x.DepositoID);
                    table.ForeignKey(
                        name: "FK_Deposito_ContaCorrente_ContaCorrenteID",
                        column: x => x.ContaCorrenteID,
                        principalTable: "ContaCorrente",
                        principalColumn: "ContaCorrenteID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Deposito_TransacaoCripto_TransacaoCriptoID",
                        column: x => x.TransacaoCriptoID,
                        principalTable: "TransacaoCripto",
                        principalColumn: "TransacaoCriptoID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Emprestimo",
                columns: table => new
                {
                    EmprestimoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ValorContratado = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DataLiberacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    QuantidadeParcelas = table.Column<int>(type: "int", nullable: false),
                    ValorPagamentoParcela = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PercentualJurosMensal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PercentualJurosAtraso = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ContaCorrenteID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emprestimo", x => x.EmprestimoID);
                    table.ForeignKey(
                        name: "FK_Emprestimo_ContaCorrente_ContaCorrenteID",
                        column: x => x.ContaCorrenteID,
                        principalTable: "ContaCorrente",
                        principalColumn: "ContaCorrenteID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transferencia",
                columns: table => new
                {
                    TransferenciaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeroCarteiraDestino = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransacaoID = table.Column<int>(type: "int", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransacaoCriptoID = table.Column<int>(type: "int", nullable: false),
                    TipoID = table.Column<byte>(type: "tinyint", nullable: false),
                    ContaCorrenteID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transferencia", x => x.TransferenciaID);
                    table.ForeignKey(
                        name: "FK_Transferencia_ContaCorrente_ContaCorrenteID",
                        column: x => x.ContaCorrenteID,
                        principalTable: "ContaCorrente",
                        principalColumn: "ContaCorrenteID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transferencia_TransacaoCripto_TransacaoCriptoID",
                        column: x => x.TransacaoCriptoID,
                        principalTable: "TransacaoCripto",
                        principalColumn: "TransacaoCriptoID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Parcela",
                columns: table => new
                {
                    ParcelaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StatusID = table.Column<byte>(type: "tinyint", nullable: false),
                    EmprestimoID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parcela", x => x.ParcelaID);
                    table.ForeignKey(
                        name: "FK_Parcela_Emprestimo_EmprestimoID",
                        column: x => x.EmprestimoID,
                        principalTable: "Emprestimo",
                        principalColumn: "EmprestimoID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cartao_ContaCorrenteID",
                table: "Cartao",
                column: "ContaCorrenteID");

            migrationBuilder.CreateIndex(
                name: "IX_ContaCorrente_PessoaID",
                table: "ContaCorrente",
                column: "PessoaID");

            migrationBuilder.CreateIndex(
                name: "IX_Deposito_ContaCorrenteID",
                table: "Deposito",
                column: "ContaCorrenteID");

            migrationBuilder.CreateIndex(
                name: "IX_Deposito_TransacaoCriptoID",
                table: "Deposito",
                column: "TransacaoCriptoID");

            migrationBuilder.CreateIndex(
                name: "IX_Emprestimo_ContaCorrenteID",
                table: "Emprestimo",
                column: "ContaCorrenteID");

            migrationBuilder.CreateIndex(
                name: "IX_Parcela_EmprestimoID",
                table: "Parcela",
                column: "EmprestimoID");

            migrationBuilder.CreateIndex(
                name: "IX_Transferencia_ContaCorrenteID",
                table: "Transferencia",
                column: "ContaCorrenteID");

            migrationBuilder.CreateIndex(
                name: "IX_Transferencia_TransacaoCriptoID",
                table: "Transferencia",
                column: "TransacaoCriptoID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cartao");

            migrationBuilder.DropTable(
                name: "Deposito");

            migrationBuilder.DropTable(
                name: "Parcela");

            migrationBuilder.DropTable(
                name: "Transferencia");

            migrationBuilder.DropTable(
                name: "Emprestimo");

            migrationBuilder.DropTable(
                name: "TransacaoCripto");

            migrationBuilder.DropTable(
                name: "ContaCorrente");

            migrationBuilder.DropTable(
                name: "Pessoa");
        }
    }
}
