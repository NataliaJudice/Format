using Format.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Format.Infra.Data.Mappings
{
    public class SecoesConfiguration : IEntityTypeConfiguration<Secoes>
    {
        public void Configure(EntityTypeBuilder<Secoes> builder)
        {
            {
                builder.HasKey(x => new { x.IdSecoes });

                builder.HasOne(x => x.Documento)
                    .WithMany(x => x.Secoes)
                    .HasForeignKey(x => x.IdDocumento)
                    .OnDelete(DeleteBehavior.Cascade);

                builder.HasOne(s => s.SecaoPai)
                    .WithMany(s => s.SecoesFilhas) // se não criar a coleção, use .WithMany() vazio
                    .HasForeignKey(s => s.IdPai) 
                    .OnDelete(DeleteBehavior.Restrict); // Evita problemas de exclusão em cascata cíclica

            }
        }
    }
}
