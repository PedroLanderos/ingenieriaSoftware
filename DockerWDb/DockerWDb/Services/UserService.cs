using DockerWDb.Data;
using DockerWDb.Interfaces;
using DockerWDb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DockerWDb.Services
{
    public class UserService : IUser
    {
        private readonly IConfiguration _config;
        private readonly AppDbContext _context;

        public UserService(IConfiguration config, AppDbContext context)
        {
            _config = config;
            _context = context;
        }

        // Método para registrar un usuario (con cifrado de la contraseña)
        public async Task<UserModel> RegisterUser(UserModel user)
        {
            // Encriptar la contraseña
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            // Agregar el usuario a la base de datos
            _context.Usuarios.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        // Método para hacer login (validar las credenciales y generar JWT)
        public async Task<UserModel> LoginUser(string email, string password)
        {
            var user = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
                return null; // Usuario no encontrado

            // Verificar la contraseña (comparar hash)
            if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
                return null; // Contraseña incorrecta

            // Generar JWT para el usuario
            string token = GenerateToken(user);

            // Asignar el token al usuario
            user.Password = token; // O podrías devolverlo por separado si solo quieres el token

            return user;  // Devolvemos el usuario con el token generado
        }

        // Obtener todos los usuarios (solo accesible por administradores)
        public async Task<IEnumerable<UserModel>> GetAllUsers()
        {
            return await _context.Usuarios.ToListAsync();
        }

        // Obtener un usuario por ID
        public async Task<UserModel> GetUserById(long id)
        {
            return await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        // Editar usuario
        public async Task<bool> EditUser(UserModel user)
        {
            var existingUser = await _context.Usuarios.FindAsync(user.Id);

            if (existingUser == null)
                return false;

            // Actualizar propiedades
            existingUser.Nombre = user.Nombre;
            existingUser.Email = user.Email;
            existingUser.Password = string.IsNullOrEmpty(user.Password) ? existingUser.Password : BCrypt.Net.BCrypt.HashPassword(user.Password);
            existingUser.UsuarioRoles = user.UsuarioRoles; // Actualizar roles si es necesario

            _context.Usuarios.Update(existingUser);
            await _context.SaveChangesAsync();

            return true;
        }

        // Generar JWT para el usuario
        private string GenerateToken(UserModel user)
        {
            var key = Encoding.UTF8.GetBytes(_config["Authentication:Key"]);
            var securityKey = new SymmetricSecurityKey(key);
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, user.Nombre),
                new(ClaimTypes.Email, user.Email),
                new("UserId", user.Id.ToString()) // Id del usuario como claim
            };

            if (user.UsuarioRoles != null && user.UsuarioRoles.Any())
            {
                // Asignar roles
                foreach (var usuarioRol in user.UsuarioRoles)
                {
                    if (usuarioRol.Rol != null)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, usuarioRol.Rol.Nombre));
                    }
                }
            }

            var token = new JwtSecurityToken(
                issuer: _config["Authentication:Issuer"],
                audience: _config["Authentication:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
