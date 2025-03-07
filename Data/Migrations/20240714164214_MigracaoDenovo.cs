using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class MigracaoDenovo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TipoID",
                table: "Transferencia");

            migrationBuilder.DropColumn(
                name: "Valor",
                table: "Transferencia");

            migrationBuilder.DropColumn(
                name: "TipoID",
                table: "Deposito");

            migrationBuilder.DropColumn(
                name: "Valor",
                table: "Deposito");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "TipoID",
                table: "Transferencia",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<decimal>(
                name: "Valor",
                table: "Transferencia",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<byte>(
                name: "TipoID",
                table: "Deposito",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<decimal>(
                name: "Valor",
                table: "Deposito",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
