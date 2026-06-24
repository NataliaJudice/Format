using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Format.Domain.Models
{
    public class Secoes
    {
        public int IdSecoes { get; set; }
        public int IdDocumento { get; set; }
        public Documento Documento { get; set; }
        public int IdPai { get; set; }
        public Secoes? SecaoPai { get; set; }
        public int Nivel {  get; set; }
        public string Nome { get; set; }
        public bool Status { get; set; }
        public DateTime DataCadastro { get; set; }
        public ICollection<SecoesConteudo> SecoesConteudos { get; set; }
    }
}
