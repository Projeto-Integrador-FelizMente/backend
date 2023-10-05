using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using FelizMente.Validator;
using FelizMente.Service;
using FelizMente.Model;
using Microsoft.AspNetCore.Authorization;

namespace FelizMente.Controllers
{
    [Authorize]
    [Route("~/tema")]
    [ApiController]
    public class TemaControllers : ControllerBase
    {
        private readonly ITemaService _temaService;
        private readonly IValidator<Tema> _temaValidator;
        public TemaControllers(ITemaService temaService, IValidator<Tema> temaValidator)
        {
            _temaService = temaService;
            _temaValidator = temaValidator;
        }
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _temaService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetByid(long id)
        {
            var Resposta = await _temaService.GetById(id);
            if (Resposta is null)
                return NotFound();
            return Ok(Resposta);
        }

        [HttpGet("nome/{nome}")]
        public async Task<ActionResult> GetByNome (string nome)
        {
            return Ok(await _temaService.GetByNome(nome));
        }
        [HttpGet("descricao/{descricao}")]
        public async Task<ActionResult> GetByDescricao (string descricao)
        {
            return Ok(await _temaService.GetByDescricao(descricao));
        }
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Tema tema)
        {
            var validarTema = await _temaValidator.ValidateAsync(tema);

            if (!validarTema.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, validarTema);

            var Resposta = await _temaService.Create(tema);

            if (Resposta is null)
                return BadRequest("Tema não encontrado");

            return CreatedAtAction(nameof(GetByid), new { id = tema.Id }, tema);
        }
        [HttpPut]
        public async Task<ActionResult> Update([FromBody] Tema tema)
        {
            if (tema.Id == 0)
                return BadRequest("id da Tema é invalida");

            var validarTema = await _temaValidator.ValidateAsync(tema);

            if (!validarTema.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, validarTema);

            var Resposta = await _temaService.Update(tema);

            if (Resposta is null)
                return NotFound("Tema e/ou Postagem não encontrada!");
            return Ok(Resposta);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id)
        {
            var BuscarTema = await _temaService.GetById(id);
            if (BuscarTema is null)
                return NotFound("Tema não encontrada!");
            await _temaService.Delete(BuscarTema);
            return NoContent();
        }
    }
}
