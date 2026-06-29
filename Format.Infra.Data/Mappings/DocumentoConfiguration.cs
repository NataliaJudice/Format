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
    public class DocumentoConfiguration : IEntityTypeConfiguration<Documento>
    {
        public void Configure(EntityTypeBuilder<Documento> builder)
        {
            {
                builder.HasKey(x => new { x.IdDocumento });

                builder.HasOne(x => x.TipoDocumento)
                    .WithMany(x => x.Documentos)
                    .HasForeignKey( x=> x.IdTipoDocumento)
                    .OnDelete(DeleteBehavior.SetNull);

                builder.HasOne(x => x.Usuario)
                    .WithMany(x => x.Documentos)
                    .HasForeignKey(x => x.IdUsuario)
                    .OnDelete(DeleteBehavior.Restrict);
            }
        }
    }
}
