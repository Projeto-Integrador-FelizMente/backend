using FelizMente.Model;
using FelizMente.Service;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace FelizMente.Controller
{
    [Route("~/postagens")]
    [ApiController]
    public class PostagemController : ControllerBase
    {

        private readonly IPostagemService _postagemService;
        private readonly IValidator<Postagem> _postagemValidator;

        public PostagemController(
            IPostagemService postagemService,
            IValidator<Postagem> postagemValidator
            )
        {
            _postagemService = postagemService;
            _postagemValidator = postagemValidator;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _postagemService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(long id)
        {
            var Resposta = await _postagemService.GetById(id);

            if (Resposta is null)
                return NotFound();

            return Ok(Resposta);
        }

        [HttpGet("titulo/{titulo}")]
        public async Task<ActionResult> GetByTitulo(string titulo)
        {
            return Ok(await _postagemService.GetByTitulo(titulo));
        }


        [HttpGet("estado/{estado}")]
        public async Task<ActionResult> GetByEstado(bool estado)
        {
            return Ok(await _postagemService.GetByEstado(estado));
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Postagem postagem)
        {

            var validarPostagem = await _postagemValidator.ValidateAsync(postagem);

            if (!validarPostagem.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, validarPostagem);
            }

            var Resposta = await _postagemService.Create(postagem);

            if (Resposta is null)
                return BadRequest("Postagem não encontrada!");

            return CreatedAtAction(nameof(GetById), new { id = postagem.Id }, postagem);

        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] Postagem postagem)
        {
            if (postagem.Id == 0)
                return BadRequest("Id da postagem é inválido!");

            var validarPostagem = await _postagemValidator.ValidateAsync(postagem);

            if (!validarPostagem.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, validarPostagem);
            }

            var Resposta = await _postagemService.Update(postagem);

            if (Resposta is null)
                return NotFound("Postagem não encontrada!");

            return Ok(Resposta);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var BuscaUsuario = await _postagemService.GetById(id);

            if (BuscaUsuario is null)
                return NotFound("Postagem não foi encontrada!");

            await _postagemService.Delete(BuscaUsuario);

            return NoContent();

        }
    }
}
