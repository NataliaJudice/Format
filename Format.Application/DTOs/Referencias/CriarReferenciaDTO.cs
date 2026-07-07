using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Format.Application.DTOs.Referencias
{
    public class CriarReferenciaDTO
    {
        public string Descricao { get; set; } = string.Empty;
        public int IdDocumento { get; set; }
    }
}
