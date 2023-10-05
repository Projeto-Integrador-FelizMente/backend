using FelizMente.Model;
using FelizMente.Security;
using FelizMente.Service;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FelizMente.Controller
{

    [Route("~/usuarios")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;
        private readonly IValidator<User> _userValidator;
        private readonly IAuthService _authService;

        public UserController(
            IUserService userService,
            IValidator<User> userValidator,
            IAuthService authService)
        {
            _userService = userService;
            _userValidator = userValidator;
            _authService = authService;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _userService.GetAll());
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(long id)
        {
            var Resposta = await _userService.GetById(id);

            if (Resposta is null)
                return NotFound();

            return Ok(Resposta);
        }

        [Authorize]
        [HttpGet("nome/{nome}")]
        public async Task<ActionResult> GetByNome(string nome)
        {
            return Ok(await _userService.GetByNome(nome));
        }

        [Authorize]
        [HttpGet("tipo/{tipo}")]
        public async Task<ActionResult> GetByTipo(string tipo)
        {
            return Ok(await _userService.GetByTipo(tipo));
        }

        [AllowAnonymous]
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

        [Authorize]
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

        [AllowAnonymous]
        [HttpPost("logar")]
        public async Task<ActionResult> Autenticar([FromBody] UserLogin usuarioLogin)
        {
            var Resposta = await _authService.Autenticar(usuarioLogin);

            if (Resposta is null)
                return Unauthorized("Usuário e/ou Senha inválidos!");

            return Ok(Resposta);
        }


    }
}


