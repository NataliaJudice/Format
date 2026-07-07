using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Format.Application.DTOs.Documento
{
    public class AtualizarDocumentoDTO
    {
        public string? Nome { get; set; }
        public int IdTipoDocumento { get; set; }
        public int IdUsuario { get; set; }
    }
}
