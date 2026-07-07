using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Format.Application.DTOs.Secoes
{
    public class CriarSecaoDTO
    {
        public int IdDocumento { get; set; }
        public int? IdPai { get; set; } // Nullable, pois a seção raiz não tem pai
        public int Nivel { get; set; }
        public string Nome { get; set; } = string.Empty;
    }
}
