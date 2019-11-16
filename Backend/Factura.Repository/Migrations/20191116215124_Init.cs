using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Factura.Repository.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    RazonSocial = table.Column<string>(nullable: false),
                    RUC = table.Column<long>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Contraseña = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Facturas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    FechaConsulta = table.Column<DateTime>(nullable: false),
                    FechaEmision = table.Column<DateTime>(nullable: false),
                    FechaPago = table.Column<DateTime>(nullable: false),
                    TotalFacturado = table.Column<double>(nullable: false),
                    Retencion = table.Column<double>(nullable: false),
                    DiasPorAño = table.Column<double>(nullable: false),
                    PlazoDeTasa = table.Column<double>(nullable: false),
                    IsNominal = table.Column<bool>(nullable: false),
                    TasaEfectiva = table.Column<double>(nullable: false),
                    TasaNominal = table.Column<double>(nullable: false),
                    PeriodoCapital = table.Column<double>(nullable: false),
                    m = table.Column<double>(nullable: false),
                    n = table.Column<double>(nullable: false),
                    FechaDescuento = table.Column<DateTime>(nullable: false),
                    TEA = table.Column<double>(nullable: false),
                    DiasTranscurridos = table.Column<double>(nullable: false),
                    TEPeriodo = table.Column<double>(nullable: false),
                    dPeriodo = table.Column<double>(nullable: false),
                    DescuentoPeriodo = table.Column<double>(nullable: false),
                    CostesIniciales = table.Column<double>(nullable: false),
                    ValorNeto = table.Column<double>(nullable: false),
                    ValorRecibido = table.Column<double>(nullable: false),
                    CostesFinales = table.Column<double>(nullable: false),
                    ValorEntregado = table.Column<double>(nullable: false),
                    TCEA = table.Column<double>(nullable: false),
                    UsuarioId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facturas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Facturas_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Costos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Motivo = table.Column<string>(nullable: true),
                    Valor = table.Column<double>(nullable: false),
                    IsEfectivo = table.Column<bool>(nullable: false),
                    IsInit = table.Column<bool>(nullable: false),
                    FacturaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Costos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Costos_Facturas_FacturaId",
                        column: x => x.FacturaId,
                        principalTable: "Facturas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Costos_FacturaId",
                table: "Costos",
                column: "FacturaId");

            migrationBuilder.CreateIndex(
                name: "IX_Facturas_UsuarioId",
                table: "Facturas",
                column: "UsuarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Costos");

            migrationBuilder.DropTable(
                name: "Facturas");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
