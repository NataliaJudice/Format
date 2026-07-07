using Format.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Format.Application.Interfaces
{
    public interface IReferenciaService
    {
        Task<IEnumerable<Referencias>> ObterPorDocumento(int idDocumento);
        Task<Referencias?> ObterPorId(int id);
        Task<Referencias> CriarReferencia(string descricao, int idDocumento);
        Task<bool> Atualizar(int id, string descricao, bool status);
        Task<bool> Deletar(int id);
    }
}
