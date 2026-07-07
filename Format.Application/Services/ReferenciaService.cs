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
    internal class ReferenciaService : IReferenciaService
    {
        private readonly IFormatDbContext _context;

        public ReferenciaService(IFormatDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Referencias>> ObterPorDocumento(int idDocumento)
        {
            try
            {
                return await _context.Referencias
                    .Where(r => r.IdDocumento == idDocumento)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao recuperar as referências do documento {idDocumento}.", ex);
            }
        }

        public async Task<Referencias?> ObterPorId(int id)
        {
            try
            {
                return await _context.Referencias.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao buscar a referência com ID {id}.", ex);
            }
        }

        public async Task<Referencias> CriarReferencia(string descricao, int idDocumento)
        {
            try
            {
                var documentoExiste = await _context.Documento.AnyAsync(d => d.IdDocumento == idDocumento);
                if (!documentoExiste)
                {
                    throw new Exception($"O documento com ID {idDocumento} não existe.");
                }

                var novaReferencia = new Referencias
                {
                    Descricao = descricao,
                    IdDocumento = idDocumento,
                    Status = true,
                    DataCadastro = DateTime.UtcNow
                };

                await _context.Referencias.AddAsync(novaReferencia);
                await _context.SaveChangesAsync();

                return novaReferencia;
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception("Erro de integridade ao salvar a referência no banco.", dbEx);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro inesperado ao criar a referência.", ex);
            }
        }

        public async Task<bool> Atualizar(int id, string descricao, bool status)
        {
            try
            {
                var referenciaExistente = await _context.Referencias.FindAsync(id);
                if (referenciaExistente == null)
                {
                    return false;
                }

                referenciaExistente.Descricao = descricao;
                referenciaExistente.Status = status;

                _context.Referencias.Update(referenciaExistente);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (DbUpdateConcurrencyException dbEx)
            {
                throw new Exception("A referência foi alterada por outro usuário concorrente.", dbEx);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao atualizar a referência com ID {id}.", ex);
            }
        }

        public async Task<bool> Deletar(int id)
        {
            try
            {
                var referencia = await _context.Referencias.FindAsync(id);
                if (referencia == null)
                {
                    return false;
                }

                _context.Referencias.Remove(referencia);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao excluir a referência com ID {id}.", ex);
            }
        }
    }
}
