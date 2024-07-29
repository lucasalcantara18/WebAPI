using Domain.Enumerations;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DataAccess.Mappings
{
    public class ClienteMap : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder
                .ToTable("cliente")
                .HasKey(x => x.Id);

            builder
               .Property(x => x.Id)
               .HasColumnName("id")
               .IsRequired();

            builder
               .Property(x => x.Cpf)
               .HasColumnName("cpf")
               .IsRequired();

            builder
               .Property(x => x.Usuario)
               .HasColumnName("usuario")
               .IsRequired();

            builder
               .Property(x => x.Senha)
               .HasColumnName("senha")
               .IsRequired();

            builder
               .Property(x => x.Nome)
               .HasColumnName("nome")
               .IsRequired();

            builder
               .Property(x => x.Celular)
               .HasColumnName("celular")
               .IsRequired();

            builder
                .Property(x => x.Email)
                .HasColumnName("email")
                .IsRequired();

            builder
                .Property(x => x.Endereco)
                .HasColumnName("endereco")
                .IsRequired();

            builder
                .Property(x => x.Sexo)
                .HasColumnName("sexo")
                .IsRequired();

        }
    }
}