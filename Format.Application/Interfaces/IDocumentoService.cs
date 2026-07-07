using Format.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Format.Application.Interfaces
{
    public interface IDocumentoService
    {
        Task<IEnumerable<Documento>> ObterTodos();
        Task<Documento?> ObterPorId(int id);
        Task<Documento> CriarDocumento(string? nome, int idTipoDocumento, int idUsuario);
        Task<bool> Atualizar(int id, string? nome, int idTipoDocumento, int idUsuario);
        Task<bool> Deletar(int id);
    }
}
