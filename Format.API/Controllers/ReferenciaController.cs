using Format.Application.DTOs.Referencias;
using Format.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Format.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReferenciaController : ControllerBase
    {
        private readonly IReferenciaService _referenciasService;

        public ReferenciaController(IReferenciaService referenciasService)
        {
            _referenciasService = referenciasService;
        }

        // GET: api/referencias/documento/5
        [HttpGet("documento/{idDocumento}")]
        public async Task<IActionResult> GetByDocumento(int idDocumento)
        {
            try
            {
                var referencias = await _referenciasService.ObterPorDocumento(idDocumento);
                return Ok(referencias);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensagem = ex.Message });
            }
        }

        // GET: api/referencias/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var referencia = await _referenciasService.ObterPorId(id);
                if (referencia == null)
                    return NotFound(new { mensagem = $"Referência com ID {id} não encontrada." });

                return Ok(referencia);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensagem = ex.Message });
            }
        }

        // POST: api/referencias
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CriarReferenciaDTO dto)
        {
            if (dto == null)
                return BadRequest(new { mensagem = "Dados inválidos." });

            try
            {
                var novaReferencia = await _referenciasService.CriarReferencia(dto.Descricao, dto.IdDocumento);
                return CreatedAtAction(nameof(GetById), new { id = novaReferencia.IdReferencias }, novaReferencia);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        // PUT: api/referencias/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AtualizarReferenciaDTO dto)
        {
            if (dto == null)
                return BadRequest(new { Clarification = "Dados inválidos." });

            try
            {
                var atualizado = await _referenciasService.Atualizar(id, dto.Descricao, dto.Status);
                if (!atualizado)
                    return NotFound(new { mensagem = $"Referência com ID {id} não encontrada para atualização." });

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        // DELETE: api/referencias/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deletado = await _referenciasService.Deletar(id);
                if (!deletado)
                    return NotFound(new { mensagem = $"Referência com ID {id} não encontrada." });

                return Ok(new { mensagem = "Referência deletada com sucesso!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }
    }
}

