using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Vendinha.Models;

namespace Vendinha.Contexts
{
    public class VendinhaDbContext : DbContext
    {
        private string connectionString;

        public VendinhaDbContext(string connectionString = null)
        {
            this.connectionString = connectionString;
        }

        public DbSet<Cliente> Clientes => Set<Cliente>();
        public DbSet<Divida> Dividas => Set<Divida>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var entityCliente = modelBuilder.Entity<Cliente>();

            entityCliente.ToTable("clientes");

            entityCliente.Property(c => c.Id).HasColumnName("id");
            entityCliente.Property(c => c.NomeCompleto).HasColumnName("nomecompleto");
            entityCliente.Property(c => c.Cpf).HasColumnName("cpf");
            entityCliente.Property(c => c.DataNascimento).HasColumnName("datanascimento");
            entityCliente.Property(c => c.Email).HasColumnName("email");
            entityCliente.HasKey(c => c.Id);

            
            entityCliente.HasIndex(c => c.Cpf).IsUnique();

            var entityDivida = modelBuilder.Entity<Divida>();

            entityDivida.ToTable("dividas");

            entityDivida.Property(d => d.Id).HasColumnName("id");
            entityDivida.Property(d => d.ClienteId).HasColumnName("clienteid");
            entityDivida.Property(d => d.Valor).HasColumnName("valor").HasPrecision(10, 2);
            entityDivida.Property(d => d.Paga).HasColumnName("paga");
            entityDivida.Property(d => d.DataCriacao).HasColumnName("datacriacao");
            entityDivida.Property(d => d.DataPagamento).HasColumnName("datapagamento");
            entityDivida.HasKey(d => d.Id);

            
            entityDivida.HasOne(d => d.Cliente)
                .WithMany(c => c.Dividas)
                .HasForeignKey(d => d.ClienteId);

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!string.IsNullOrEmpty(connectionString))
            {
                optionsBuilder.UseNpgsql(connectionString)
                    .EnableSensitiveDataLogging()
                    .LogTo(m => Debug.WriteLine(m));
            }
            base.OnConfiguring(optionsBuilder);
        }
    }
}