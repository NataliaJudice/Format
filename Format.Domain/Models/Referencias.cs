using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Format.Domain.Models
{
    public class Referencias
    {
        public int IdReferencias { get; set; }
        public string Descricao { get; set; }
        public int IdDocumento { get; set; }
        public Documento Documento { get; set; }
        public bool Status { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
