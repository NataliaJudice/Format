using Format.Application.Interfaces;
using Format.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Format.Infra.Data.Data
{
    public class FormatDbContext : DbContext, IFormatDbContext
    {
        public FormatDbContext(DbContextOptions<FormatDbContext> options)
            : base(options)
        { }

        public DbSet<Documento> Documento { get; set; }
        public DbSet<Referencias> Referencias { get; set; }
        public DbSet<Secoes> Secoes { get; set; }
        public DbSet<SecoesConteudo> SecoesConteudo { get; set; }
        public DbSet<TipoDocumento> TipoDocumento { get; set; }
        public DbSet<Usuario> Usuario { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FormatDbContext).Assembly);

        }
    }
}
