using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Format.Domain.Models
{
    public class Documento
    {
        public int IdDocumento { get; set; }
        public string Nome {  get; set; }
        public int? IdTipoDocumento { get; set; }
        public TipoDocumento? TipoDocumento { get; set; }
        public bool Status { get; set; }
        public DateTime DataCadastro { get; set; }
        public int IdUsuario { get; set; }
        public Usuario Usuario { get; set; }
        public ICollection<Secoes> Secoes { get; set; }
        public ICollection<Referencias> Referencias { get; set; }
    }
}
