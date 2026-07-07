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
    public class SecoesService : ISecoesService
    {
        private readonly IFormatDbContext _context;

        public SecoesService(IFormatDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Secoes>> ObterTodasPorDocumento(int idDocumento)
        {
            try
            {
                // Traz apenas as seções do documento específico.
                // Usamos o Include para trazer as seções filhas, caso o React precise renderizar uma árvore/menu.
                return await _context.Secoes
                    .Where(s => s.IdDocumento == idDocumento)
                    .Include(s => s.SecoesFilhas)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao recuperar as seções do documento {idDocumento}.", ex);
            }
        }

        public async Task<Secoes?> ObterPorId(int id)
        {
            try
            {
                return await _context.Secoes
                    .Include(s => s.SecoesFilhas)
                    .Include(s => s.SecoesConteudos) // Caso o React precise renderizar o conteúdo da seção
                    .FirstOrDefaultAsync(s => s.IdSecoes == id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao buscar a seção com ID {id}.", ex);
            }
        }

        public async Task<Secoes> CriarSecao(int idDocumento, int? idPai, int nivel, string nome)
        {
            try
            {
                // Se um idPai foi informado, valida se essa seção pai realmente existe no banco
                if (idPai.HasValue)
                {
                    var paiExiste = await _context.Secoes.AnyAsync(s => s.IdSecoes == idPai.Value);
                    if (!paiExiste)
                    {
                        throw new Exception($"A seção pai com ID {idPai} informada não existe.");
                    }
                }

                var novaSecao = new Secoes
                {
                    IdDocumento = idDocumento,
                    IdPai = idPai,
                    Nivel = nivel,
                    Nome = nome,
                    Status = true, // Ativa por padrão ao criar
                    DataCadastro = DateTime.UtcNow
                };

                await _context.Secoes.AddAsync(novaSecao);
                await _context.SaveChangesAsync();

                return novaSecao;
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception("Erro de integridade no banco de dados ao criar a seção. Verifique os IDs de documento ou pai.", dbEx);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro inesperado ao criar a seção.", ex);
            }
        }

        public async Task<bool> Atualizar(int id, int? idPai, int nivel, string nome, bool status)
        {
            try
            {
                var secaoExistente = await _context.Secoes.FindAsync(id);
                if (secaoExistente == null)
                {
                    return false;
                }

                // Evita que uma seção seja pai dela mesma (o que quebraria a estrutura em árvore)
                if (idPai.HasValue && idPai.Value == id)
                {
                    throw new Exception("Uma seção não pode ser pai de si mesma.");
                }

                secaoExistente.IdPai = idPai;
                secaoExistente.Nivel = nivel;
                secaoExistente.Nome = nome;
                secaoExistente.Status = status;

                _context.Secoes.Update(secaoExistente);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (DbUpdateConcurrencyException dbEx)
            {
                throw new Exception("A seção foi modificada por outro processo concorrente.", dbEx);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao atualizar a seção com ID {id}: {ex.Message}", ex);
            }
        }

        public async Task<bool> Deletar(int id)
        {
            try
            {
                var secao = await _context.Secoes
                    .Include(s => s.SecoesFilhas)
                    .FirstOrDefaultAsync(s => s.IdSecoes == id);

                if (secao == null)
                {
                    return false;
                }

                // Regra de negócio importante para árvore: se deletar a seção pai, 
                // precisamos decidir o que fazer com as filhas. Aqui vamos impedir a deleção se houver filhas vinculadas.
                if (secao.SecoesFilhas.Any())
                {
                    throw new Exception("Não é possível deletar esta seção porque ela possui subseções vinculadas. Delete as subseções primeiro.");
                }

                _context.Secoes.Remove(secao);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao deletar a seção com ID {id}: {ex.Message}", ex);
            }
        }
    }
}

