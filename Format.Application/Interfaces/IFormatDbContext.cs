using Format.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Format.Application.Interfaces
{
    public interface IFormatDbContext
    {
        public DbSet<Documento> Documento { get; set; }
        public DbSet<Referencias> Referencias { get; set; }
        public DbSet<Secoes> Secoes { get; set; }
        public DbSet<SecoesConteudo> SecoesConteudo { get; set; }
        public DbSet<TipoDocumento> TipoDocumento { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        int SaveChanges();
    }
}
