using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Format.Domain.Models
{
    public class TipoDocumento
    {
        public int IdTipoDocumento { get; set; }
        public string Nome { get; set; }
        public bool Status { get; set; }
        public DateTime DataCadastro { get; set; }
        public ICollection<Documento> Documentos { get; set; }
    }
}
