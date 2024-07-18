using Microsoft.EntityFrameworkCore;
using MiniProyectoBanking.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProyectoBanking.Infrastructure.Persistence.Contexts
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Transaccion> Transacciones { get; set; }
        public DbSet<Beneficiario> Beneficiarios { get; set; }
        public DbSet<Producto> Productos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Fluent API

            #region Tablas
            modelBuilder.Entity<Usuario>().ToTable("Usuarios");
            modelBuilder.Entity<Transaccion>().ToTable("Transacciones");
            modelBuilder.Entity<Beneficiario>().ToTable("Beneficiarios");
            modelBuilder.Entity<Producto>().ToTable("Productos");
            #endregion

            #region Primary Keys
            modelBuilder.Entity<Usuario>().HasKey(u => u.Id);
            modelBuilder.Entity<Transaccion>().HasKey(t => t.Id);
            modelBuilder.Entity<Beneficiario>().HasKey(b => b.Id);
            modelBuilder.Entity<Producto>().HasKey(p => p.Id);
            #endregion

            #region Relationships
            modelBuilder.Entity<Usuario>()
                .HasMany(u => u.Beneficiarios)
                .WithOne(b => b.Cliente)
                .HasForeignKey(b => b.ClienteId);

            modelBuilder.Entity<Usuario>()
                .HasMany(u => u.Productos)
                .WithOne(p => p.Cliente)
                .HasForeignKey(p => p.ClienteId);

            modelBuilder.Entity<Producto>()
                .HasMany(p => p.TransaccionesOrigen)
                .WithOne(t => t.CuentaOrigen)
                .HasForeignKey(t => t.CuentaOrigenId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Producto>()
                .HasMany(p => p.TransaccionesDestino)
                .WithOne(t => t.CuentaDestino)
                .HasForeignKey(t => t.CuentaDestinoId)
                .OnDelete(DeleteBehavior.Restrict);
            #endregion

            #region Property Configurations

            modelBuilder.Entity<Producto>()
                .Property(p => p.Limite)
                .IsRequired(false);

            modelBuilder.Entity<Producto>()
                .Property(p => p.Monto)
                .IsRequired(false);

            modelBuilder.Entity<Producto>()
                .Property(p => p.Deuda)
                .IsRequired(false);

            #endregion
        }
    }
}