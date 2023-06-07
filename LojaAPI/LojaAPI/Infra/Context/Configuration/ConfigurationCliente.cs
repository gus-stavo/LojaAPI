using LojaAPI.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LojaAPI.Infra.Context.Configuration
{
    public class ConfigurationCliente : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder) 
        {
            builder.HasKey(x => x.cdCliente);

            builder
                .Property(nameof(Cliente.cdCliente))
                .IsRequired()
                .HasColumnName("CD_CLIENTE")
                .HasColumnType("bigint");

            builder
                .Property(nameof(Cliente.cdCpf))
                .IsRequired(false)
                .HasColumnName("CD_CPF")
                .HasColumnType("varchar(11)");

            builder
                .Property(nameof(Cliente.cdCnpj))
                .IsRequired(false)
                .HasColumnName("CD_CNPJ")
                .HasColumnType("varchar(13)");

            builder
                .Property(nameof(Cliente.nmCliente))
                .IsRequired()
                .HasColumnName("NM_CLIENTE")
                .HasColumnType("varchar(100)");

            builder
                .Property(nameof(Cliente.nmRazaoSocial))
                .IsRequired(false)
                .HasColumnName("NM_RAZAO_SOCIAL")
                .HasColumnType("varchar(100)");

            builder
                .Property(nameof(Cliente.cdCep))
                .IsRequired()
                .HasColumnName("CD_CEP")
                .HasColumnType("varchar(8)");

            builder
                .Property(nameof(Cliente.nmLogradouro))
                .IsRequired(false)
                .HasColumnName("NM_LOGRADOURO")
                .HasColumnType("varchar(100)");

            builder
                .Property(nameof(Cliente.nrLogradouro))
                .IsRequired(false)
                .HasColumnName("NR_LOGRADOURO")
                .HasColumnType("numeric(5,0)");

            builder
                .Property(nameof(Cliente.dsComplemento))
                .IsRequired(false)
                .HasColumnName("DS_COMPLEMENTO")
                .HasColumnType("varchar(100)");

            builder
                .Property(nameof(Cliente.nmBairro))
                .IsRequired(false)
                .HasColumnName("NM_BAIRRO")
                .HasColumnType("varchar(100)");

            builder
                .Property(nameof(Cliente.nmCidade))
                .IsRequired(false)
                .HasColumnName("NM_CIDADE")
                .HasColumnType("varchar(100)");

            builder
                .Property(nameof(Cliente.cdEstado))
                .IsRequired(false)
                .HasColumnName("CD_ESTADO")
                .HasColumnType("varchar(2)");

            builder
                .Property(nameof(Cliente.dsEmail))
                .IsRequired()
                .HasColumnName("DS_EMAIL")
                .HasColumnType("varchar(100)");

            builder
                .Property(nameof(Cliente.dsClassificacao))
                .IsRequired()
                .HasColumnName("DS_CLASSIFICACAO")
                .HasColumnType("varchar(12)");
        }
    }
}
