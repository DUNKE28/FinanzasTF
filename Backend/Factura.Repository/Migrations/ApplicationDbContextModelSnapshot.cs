﻿// <auto-generated />
using System;
using Factura.Repository.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Factura.Repository.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Factura.Domain.Costo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("FacturaId");

                    b.Property<string>("Motivo");

                    b.Property<string>("PorcEfect");

                    b.Property<string>("Tipo");

                    b.Property<double>("Valor");

                    b.HasKey("Id");

                    b.HasIndex("FacturaId");

                    b.ToTable("Costos");
                });

            modelBuilder.Entity("Factura.Domain.Factu", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("CostesFinales");

                    b.Property<double>("CostesIniciales");

                    b.Property<double>("DescuentoPeriodo");

                    b.Property<int>("DiasPorAño");

                    b.Property<int>("DiasTranscurridos");

                    b.Property<DateTime>("FechaConsulta");

                    b.Property<DateTime>("FechaDescuento");

                    b.Property<DateTime>("FechaEmision");

                    b.Property<DateTime>("FechaPago");

                    b.Property<bool>("IsNominal");

                    b.Property<int>("PeriodoCapital");

                    b.Property<int>("PlazoDeTasa");

                    b.Property<double>("Retencion");

                    b.Property<double>("TCEA");

                    b.Property<double>("TEA");

                    b.Property<double>("TEPeriodo");

                    b.Property<int>("TasaEfectiva");

                    b.Property<int>("TasaNominal");

                    b.Property<double>("TotalFacturado");

                    b.Property<int>("UsuarioId");

                    b.Property<double>("ValorEntregado");

                    b.Property<double>("ValorNeto");

                    b.Property<double>("ValorRecibido");

                    b.Property<double>("dPeriodo");

                    b.Property<int>("m");

                    b.Property<int>("n");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Facturas");
                });

            modelBuilder.Entity("Factura.Domain.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Contraseña")
                        .IsRequired();

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<long>("RUC");

                    b.Property<string>("RazonSocial")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("Factura.Domain.Costo", b =>
                {
                    b.HasOne("Factura.Domain.Factu", "Factura")
                        .WithMany("Costos")
                        .HasForeignKey("FacturaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Factura.Domain.Factu", b =>
                {
                    b.HasOne("Factura.Domain.Usuario", "Usuario")
                        .WithMany("Facturas")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
