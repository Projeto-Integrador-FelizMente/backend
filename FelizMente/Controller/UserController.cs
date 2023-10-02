using FelizMente.Model;
using FelizMente.Service;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace FelizMente.Controller
{

    [Route("~/usuarios")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;
        private readonly IValidator<User> _userValidator;

        public UserController(
            IUserService userService,
            IValidator<User> userValidator
            )
        {
            _userService = userService;
            _userValidator = userValidator;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _userService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(long id)
        {
            var Resposta = await _userService.GetById(id);

            if (Resposta is null)
                return NotFound();

            return Ok(Resposta);
        }

        [HttpGet("nome/{nome}")]
        public async Task<ActionResult> GetByNome(string nome)
        {
            return Ok(await _userService.GetByNome(nome));
        }


        [HttpGet("tipo/{tipo}")]
        public async Task<ActionResult> GetByTipo(bool tipo)
        {
            return Ok(await _userService.GetByTipo(tipo));
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] User user)
        {

            var validarUser = await _userValidator.ValidateAsync(user);

            if (!validarUser.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, validarUser);
            }

            var Resposta = await _userService.Create(user);

            if (Resposta is null)
                return BadRequest("Usuário não encontrado!");

            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);

        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] User user)
        {
            if (user.Id == 0)
                return BadRequest("Id do User é inválido!");

            var validarUser = await _userValidator.ValidateAsync(user);

            if (!validarUser.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, validarUser);
            }

            var Resposta = await _userService.Update(user);

            if (Resposta is null)
                return NotFound("Usuário não encontrado!");

            return Ok(Resposta);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var BuscaUsuario = await _userService.GetById(id);

            if (BuscaUsuario is null)
                return NotFound("User não foi encontrada!");

            await _userService.Delete(BuscaUsuario);

            return NoContent();

        }
    }
}


