using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Format.Domain.Models
{
    public class Usuario
    {
        public int IdUsuario {  get; set; }
        public string Nome { get; set; }
        public ICollection<Documento> Documentos { get; set; }
    }
}
