using Format.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Format.Application.Interfaces
{
    public interface ISecoesService
    {
        Task<IEnumerable<Secoes>> ObterTodasPorDocumento(int idDocumento);
        Task<Secoes?> ObterPorId(int id);
        Task<Secoes> CriarSecao(int idDocumento, int? idPai, int nivel, string nome);
        Task<bool> Atualizar(int id, int? idPai, int nivel, string nome, bool status);
        Task<bool> Deletar(int id);
    }
}
