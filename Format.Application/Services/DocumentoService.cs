using Format.Application.Interfaces;
using Format.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Format.Application.Services
{
    public class DocumentoService : IDocumentoService
    {
        private readonly IFormatDbContext _context;
        public DocumentoService(IFormatDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Documento>> ObterTodos()
        {
            try
            {
                return await _context.Documento.Where(x => x.Status).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao recuperar a lista de documentos do banco de dados.", ex);
            }
        }

        public async Task<Documento?> ObterPorId(int id)
        {
            try
            {
                return await _context.Documento.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Ocorreu um erro ao buscar o documento com ID {id}.", ex);
            }
        }

        public async Task<Documento> CriarDocumento(string? nome, int idTipoDocumento, int idUsuario)
        {
            try
            {
                var documento = new Documento()
                {
                    Nome = nome, 
                    IdTipoDocumento = idTipoDocumento,
                    IdUsuario = idUsuario,
                    Status = true,
                    DataCadastro = DateTime.Now,
                };

                await _context.Documento.AddAsync(documento);
                await _context.SaveChangesAsync();

                return documento;
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception("Erro de integridade ao salvar o documento. Verifique se as chaves estrangeiras são válidas.", dbEx);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro inesperado ao criar o documento.", ex);
            }
        }

        public async Task<bool> Atualizar(int id, string? nome, int idTipoDocumento, int idUsuario)
        {
            try
            {
                var documentoExistente = await _context.Documento.FindAsync(id);
                if (documentoExistente == null)
                {
                    return false; 
                }
                documentoExistente.Nome = nome;
                documentoExistente.IdTipoDocumento = idTipoDocumento;
                documentoExistente.IdUsuario = idUsuario;
                _context.Documento.Update(documentoExistente);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (DbUpdateConcurrencyException dbEx)
            {
                throw new Exception("O registro foi modificado ou deletado por outro usuário concorrente.", dbEx);
            }
            catch (Exception ex)
            {
                throw new Exception($"Ocorreu um erro ao atualizar o documento com ID {id}.", ex);
            }
        }

        public async Task<bool> Deletar(int id)
        {
            try
            {
                var documento = await _context.Documento.FindAsync(id);
                if (documento == null)
                {
                    return false; 
                }
                documento.Status = false;
                _context.Documento.Update(documento);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Ocorreu um erro ao excluir o documento com ID {id}.", ex);
            }
        }
    }
}
