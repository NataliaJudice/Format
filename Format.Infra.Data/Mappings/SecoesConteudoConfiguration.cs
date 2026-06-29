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
    public class SecoesConteudoConfiguration : IEntityTypeConfiguration<SecoesConteudo>
    {
        public void Configure(EntityTypeBuilder<SecoesConteudo> builder)
        {
            {
                builder.HasKey(x => new { x.IdSecoesConteudo });

                builder.HasOne(x => x.Secoes)
                    .WithMany(x => x.SecoesConteudos)
                    .HasForeignKey(x => x.IdSecoes)
                    .OnDelete(DeleteBehavior.Cascade);
            }
        }
    
    }
}
