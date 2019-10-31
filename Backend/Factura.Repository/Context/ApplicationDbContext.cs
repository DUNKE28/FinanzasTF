using System;
using System.Collections.Generic;
using System.Text;
using Factura.Domain;
using Microsoft.EntityFrameworkCore;

namespace Factura.Repository.Context
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Factu> Facturas { get; set; }
        public DbSet<Costo> Costos { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        
        }
    }
}