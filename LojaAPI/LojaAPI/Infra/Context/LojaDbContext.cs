using LojaAPI.Domain.Models;
using LojaAPI.Infra.Context.Configuration;
using Microsoft.EntityFrameworkCore;

namespace LojaAPI.Infra.Context
{
    public class LojaDbContext : DbContext
    {
        public LojaDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ConfigurationCliente());
            modelBuilder.ApplyConfiguration(new ConfigurationTelefone());
            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<Cliente> Clientes { get; set; }

        public virtual DbSet<Telefone> Telefones { get; set; }
    }
}
