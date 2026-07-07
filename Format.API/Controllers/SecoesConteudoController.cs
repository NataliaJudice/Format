using Format.Application.DTOs.SecoesConteudo;
using Format.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Format.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecoesConteudoController : ControllerBase
    {
        private readonly ISecoesConteudoService _conteudoService;

        public SecoesConteudoController(ISecoesConteudoService conteudoService)
        {
            _conteudoService = conteudoService;
        }

        // GET: api/secoesconteudo/secao/12
        [HttpGet("secao/{idSecoes}")]
        public async Task<IActionResult> GetBySecao(int idSecoes)
        {
            try
            {
                var conteudos = await _conteudoService.ObterPorSecao(idSecoes);
                return Ok(conteudos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensagem = ex.Message });
            }
        }

        // GET: api/secoesconteudo/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var conteudo = await _conteudoService.ObterPorId(id);
                if (conteudo == null)
                    return NotFound(new { mensagem = $"Conteúdo com ID {id} não encontrado." });

                return Ok(conteudo);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensagem = ex.Message });
            }
        }

        // POST: api/secoesconteudo
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CriarSecoesConteudoDTO dto)
        {
            if (dto == null)
                return BadRequest(new { mensagem = "Dados inválidos." });

            try
            {
                var novoConteudo = await _conteudoService.CriarConteudo(dto.IdSecoes, dto.Conteudo);
                return CreatedAtAction(nameof(GetById), new { id = novoConteudo.IdSecoesConteudo }, novoConteudo);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        // PUT: api/secoesconteudo/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AtualizarSecoesConteudoDTO dto)
        {
            if (dto == null)
                return BadRequest(new { mensagem = "Dados inválidos." });

            try
            {
                var atualizado = await _conteudoService.Atualizar(id, dto.Conteudo, dto.Status);
                if (!atualizado)
                    return NotFound(new { mensagem = $"Conteúdo com ID {id} não encontrado para atualização." });

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        // DELETE: api/secoesconteudo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deletado = await _conteudoService.Deletar(id);
                if (!deletado)
                    return NotFound(new { mensagem = $"Conteúdo com ID {id} não encontrado." });

                return Ok(new { mensagem = "Conteúdo removido com sucesso!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }
    }
}
