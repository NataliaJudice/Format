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
    public class SecoesConteudoService : ISecoesConteudoService
    {
        private readonly IFormatDbContext _context;

        public SecoesConteudoService(IFormatDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SecoesConteudo>> ObterPorSecao(int idSecoes)
        {
            try
            {
                return await _context.SecoesConteudo
                    .Where(c => c.IdSecoes == idSecoes)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao recuperar os conteúdos da seção {idSecoes}.", ex);
            }
        }

        public async Task<SecoesConteudo?> ObterPorId(int id)
        {
            try
            {
                return await _context.SecoesConteudo.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao buscar o conteúdo com ID {id}.", ex);
            }
        }

        public async Task<SecoesConteudo> CriarConteudo(int idSecoes, string conteudo)
        {
            try
            {
                // Valida se a seção de destino realmente existe antes de inserir o conteúdo
                var secaoExiste = await _context.Secoes.AnyAsync(s => s.IdSecoes == idSecoes);
                if (!secaoExiste)
                {
                    throw new Exception($"A seção com ID {idSecoes} não existe.");
                }

                var novoConteudo = new SecoesConteudo
                {
                    IdSecoes = idSecoes,
                    Conteudo = conteudo,
                    Status = true,
                    DataCadastro = DateTime.UtcNow
                };

                await _context.SecoesConteudo.AddAsync(novoConteudo);
                await _context.SaveChangesAsync();

                return novoConteudo;
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception("Erro de integridade ao salvar o conteúdo no banco. Verifique o ID da seção.", dbEx);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro inesperado ao criar o conteúdo.", ex);
            }
        }

        public async Task<bool> Atualizar(int id, string conteudo, bool status)
        {
            try
            {
                var conteudoExistente = await _context.SecoesConteudo.FindAsync(id);
                if (conteudoExistente == null)
                {
                    return false;
                }

                conteudoExistente.Conteudo = conteudo;
                conteudoExistente.Status = status;

                _context.SecoesConteudo.Update(conteudoExistente);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (DbUpdateConcurrencyException dbEx)
            {
                throw new Exception("O conteúdo foi modificado por outro processo em segundo plano.", dbEx);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao atualizar o conteúdo com ID {id}.", ex);
            }
        }

        public async Task<bool> Deletar(int id)
        {
            try
            {
                var conteudo = await _context.SecoesConteudo.FindAsync(id);
                if (conteudo == null)
                {
                    return false;
                }

                _context.SecoesConteudo.Remove(conteudo);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao deletar o conteúdo com ID {id}.", ex);
            }
        }
    }
}
