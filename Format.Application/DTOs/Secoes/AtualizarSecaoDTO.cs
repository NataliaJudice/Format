using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Format.Application.DTOs.Secoes
{
    public class AtualizarSecaoDTO
    {
        public int? IdPai { get; set; }
        public int Nivel { get; set; }
        public string Nome { get; set; } = string.Empty;
        public bool Status { get; set; }
    }
}
