using DockerWDb.Interfaces;
using DockerWDb.Models;
using DockerWDb.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DockerWDb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser _userService;

        public UserController(IUser userService)
        {
            _userService = userService;
        }

        // Registro de un nuevo usuario
        [HttpPost("register")]
        public async Task<ActionResult<ApiResponse>> RegisterUser([FromBody] UserModel user)
        {
            if (user == null || !ModelState.IsValid)
                return BadRequest("Invalid user data");

            var response = await _userService.Register(user);
            if (response.Flag)
                return Ok(response.Message);

            return BadRequest(response.Message);
        }

        // Login de un usuario
        [HttpPost("login")]
        public async Task<ActionResult<ApiResponse>> LoginUser([FromBody] LoginModel login)
        {
            if (login == null || !ModelState.IsValid)
                return BadRequest("Invalid login data");

            var response = await _userService.Login(login);
            if (response.Flag)
                return Ok(new { Token = response.Message }); // Devuelve el token

            return Unauthorized(response.Message);
        }

        // Obtener todos los usuarios (solo accesible por administradores)
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse>> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();
            if (users != null)
                return Ok(users);

            return NotFound("No users found");
        }

        // Obtener un usuario por ID
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<ApiResponse>> GetUserById(int id)
        {
            var user = await _userService.GetUser(id);
            if (user != null)
                return Ok(user);

            return NotFound($"User with ID {id} not found");
        }

        // Editar un usuario por ID
        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<ApiResponse>> EditUserById(long id, [FromBody] UserModel user)
        {
            if (user == null || user.Id != id)
                return BadRequest("User ID mismatch");

            var response = await _userService.EditUserById(user);
            if (response.Flag)
                return Ok(response.Message);

            return BadRequest(response.Message);
        }
    }
}
