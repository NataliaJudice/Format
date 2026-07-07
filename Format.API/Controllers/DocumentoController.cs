using Format.Application.DTOs.Documento;
using Format.Application.Interfaces;
using Format.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Format.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentoController : ControllerBase
    {
        private readonly IDocumentoService _documentoService;

        public DocumentoController(IDocumentoService documentoService)
        {
            _documentoService = documentoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var documentos = await _documentoService.ObterTodos();
                return Ok(documentos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensagem = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var documento = await _documentoService.ObterPorId(id);
                if (documento == null)
                    return NotFound(new { mensagem = $"Documento com ID {id} não encontrado." });

                return Ok(documento);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensagem = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CriarDocumentoDTO dto)
        {
            if (dto == null)
                return BadRequest(new { mensagem = "Dados inválidos." });

            try
            {
                var novoDocumento = await _documentoService.CriarDocumento(dto.Nome, dto.IdTipoDocumento, dto.IdUsuario);

                return CreatedAtAction(nameof(GetById), new { id = novoDocumento.IdDocumento }, novoDocumento);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AtualizarDocumentoDTO dto)
        {
            if (dto == null)
                return BadRequest(new { mensagem = "Dados inválidos." });

            try
            {
                var atualizado = await _documentoService.Atualizar(id, dto.Nome, dto.IdTipoDocumento, dto.IdUsuario);

                if (!atualizado)
                    return NotFound(new { mensagem = $"Documento com ID {id} não encontrado para atualização." });

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deletado = await _documentoService.Deletar(id);
                if (!deletado)
                    return NotFound(new { mensagem = $"Documento com ID {id} não encontrado." });

                return Ok(new { mensagem = "Documento deletado com sucesso!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }
    }
}

