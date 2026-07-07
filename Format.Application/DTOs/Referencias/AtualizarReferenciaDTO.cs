using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Format.Application.DTOs.Referencias
{
    public class AtualizarReferenciaDTO
    {
        public string Descricao { get; set; } = string.Empty;
        public bool Status { get; set; }
    }
}
