using Format.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Format.Application.Interfaces
{
    public interface ISecoesConteudoService
    {
        Task<IEnumerable<SecoesConteudo>> ObterPorSecao(int idSecoes);
        Task<SecoesConteudo?> ObterPorId(int id);
        Task<SecoesConteudo> CriarConteudo(int idSecoes, string conteudo);
        Task<bool> Atualizar(int id, string conteudo, bool status);
        Task<bool> Deletar(int id);
    }
}
