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
    public class ReferenciasConfiguration : IEntityTypeConfiguration<Referencias>
    {
        public void Configure(EntityTypeBuilder<Referencias> builder)
        {
            {
                builder.HasKey(x => new { x.IdReferencias });

                builder.HasOne(x => x.Documento)
                    .WithMany(x => x.Referencias)
                    .HasForeignKey(x => x.IdDocumento)
                    .OnDelete(DeleteBehavior.Cascade);
            }
        }

    }
}
