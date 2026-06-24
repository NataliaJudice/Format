using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Format.Domain.Models
{
    public class SecoesConteudo
    {
        public int IdSecoesConteudo { get; set; }
        public int IdSecoes { get; set; }
        public Secoes Secoes { get; set; }
        public string Conteudo { get; set; }
        public bool Status { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
