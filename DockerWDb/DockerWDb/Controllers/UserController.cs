using DockerWDb.Interfaces;
using DockerWDb.Models;
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
        public async Task<ActionResult<UserModel>> RegisterUser([FromBody] UserModel user)
        {
            if (user == null || !ModelState.IsValid)
                return BadRequest("Invalid user data");

            var registeredUser = await _userService.RegisterUser(user);

            if (registeredUser == null)
                return BadRequest("User registration failed");

            return CreatedAtAction(nameof(GetUserById), new { id = registeredUser.Id }, registeredUser);
        }

        // Login de usuario
        [HttpPost("login")]
        public async Task<ActionResult<string>> LoginUser([FromBody] LoginModel login)
        {
            if (login == null || !ModelState.IsValid)
                return BadRequest("Invalid login data");

            var user = await _userService.LoginUser(login.Email, login.Password);

            if (user == null)
                return Unauthorized("Invalid credentials");

            // Devolvemos el token JWT
            return Ok(new { Token = user.Password });  // El token se almacena en user.Password
        }

        // Obtener todos los usuarios (solo accesible por admin)
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<UserModel>>> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();
            if (users == null)
                return NotFound("No users found");

            return Ok(users);
        }

        // Obtener un usuario por ID
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<UserModel>> GetUserById(long id)
        {
            var user = await _userService.GetUserById(id);

            if (user == null)
                return NotFound($"User with ID {id} not found");

            return Ok(user);
        }

        // Editar un usuario
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> EditUser(long id, [FromBody] UserModel user)
        {
            if (user == null || user.Id != id)
                return BadRequest("Invalid user data");

            var result = await _userService.EditUser(user);

            if (!result)
                return NotFound("User not found or update failed");

            return Ok("User updated successfully");
        }
    }
}
