using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Format.Application.DTOs.SecoesConteudo
{
    public class CriarSecoesConteudoDTO
    {
        public int IdSecoes { get; set; }
        public string Conteudo { get; set; } = string.Empty;
    }
}
