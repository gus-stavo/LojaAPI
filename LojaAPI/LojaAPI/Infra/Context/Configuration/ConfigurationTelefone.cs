using LojaAPI.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LojaAPI.Infra.Context.Configuration
{
    public class ConfigurationTelefone : IEntityTypeConfiguration<Telefone>
    {
        public void Configure(EntityTypeBuilder<Telefone> builder) 
        {
            builder.HasKey(x => x.cdTelefone);

            builder
                .HasOne(t => t.cliente)
                .WithMany(c => c.telefones)
                .HasForeignKey(t => t.cdCliente);

            builder
                .Property(nameof(Telefone.cdTelefone))
                .IsRequired()
                .HasColumnName("CD_TELEFONE")
                .HasColumnType("bigint");

            builder
                .Property(nameof(Telefone.nrTelefone))
                .IsRequired()
                .HasColumnName("NR_TELEFONE")
                .HasColumnType("varchar(100)");

            builder
                .Property(nameof(Telefone.cdCliente))
                .IsRequired()
                .HasColumnName("CD_CLIENTE")
                .HasColumnType("bigint");
        }
    }
}
