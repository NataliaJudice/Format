using Format.Application.DTOs.Secoes;
using Format.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Format.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecoesController : ControllerBase
    {
        private readonly ISecoesService _secoesService;

        public SecoesController(ISecoesService secoesService)
        {
            _secoesService = secoesService;
        }

        // GET: api/secoes/documento/5 (Traz todas as seções de um documento específico)
        [HttpGet("documento/{idDocumento}")]
        public async Task<IActionResult> GetByDocumento(int idDocumento)
        {
            try
            {
                var secoes = await _secoesService.ObterTodasPorDocumento(idDocumento);
                return Ok(secoes);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensagem = ex.Message });
            }
        }

        // GET: api/secoes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var secao = await _secoesService.ObterPorId(id);
                if (secao == null)
                    return NotFound(new { mensagem = $"Seção com ID {id} não encontrada." });

                return Ok(secao);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensagem = ex.Message });
            }
        }

        // POST: api/secoes
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CriarSecaoDTO dto)
        {
            if (dto == null)
                return BadRequest(new { mensagem = "Dados inválidos." });

            try
            {
                var novaSecao = await _secoesService.CriarSecao(dto.IdDocumento, dto.IdPai, dto.Nivel, dto.Nome);
                return CreatedAtAction(nameof(GetById), new { id = novaSecao.IdSecoes }, novaSecao);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        // PUT: api/secoes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AtualizarSecaoDTO dto)
        {
            if (dto == null)
                return BadRequest(new { mensagem = "Dados inválidos." });

            try
            {
                var atualizado = await _secoesService.Atualizar(id, dto.IdPai, dto.Nivel, dto.Nome, dto.Status);
                if (!atualizado)
                    return NotFound(new { mensagem = $"Seção com ID {id} não encontrada para atualização." });

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        // DELETE: api/secoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deletado = await _secoesService.Deletar(id);
                if (!deletado)
                    return NotFound(new { mensagem = $"Seção com ID {id} não encontrada." });

                return Ok(new { mensagem = "Seção deletada com sucesso!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }
    }
}

